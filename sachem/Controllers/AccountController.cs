﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using sachem.Models;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using sachem.Methodes_Communes;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private const int Portcourriel = 587;
        private readonly IDataRepository _dataRepository;

        public AccountController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public AccountController()
        {
            _dataRepository = new BdRepository();
        }

        private void CreerCookieConnexion(string nomUsager, string motDePasse)
        {
            var mdpEncrypte = Crypto.Encrypt(motDePasse,
                System.Configuration.ConfigurationManager.AppSettings.Get("CryptoKey"));
            var maintenir = new HttpCookie("SACHEMConnexion");
            maintenir.Values.Add("NomUsager", nomUsager);
            maintenir.Values.Add("MP", mdpEncrypte);

            maintenir.Expires = DateTime.Now.AddDays(1d);

            Response.Cookies.Add(maintenir);
        }

        private void SupprimerCookieConnexion()
        {
            var biscuitPerime = new HttpCookie("SACHEMConnexion") {Expires = DateTime.Now.AddDays(-1d)};
            Response.Cookies.Add(biscuitPerime);
        }

        private bool CookieConnexionExiste()
        {
            var collectionsCookiesSachem = Request.Cookies;
            var cookieMaintenirConnexion = collectionsCookiesSachem.Get("SACHEMConnexion");

            return cookieMaintenirConnexion != null;
        }

        private bool EnvoyerCourriel(string email, string nouveauMdp, string nomEtudiant)
        {
            var client = new SmtpClient();
            var reqBdCourrielReinitialisation = _dataRepository.WhereCourriel(x => x.id_TypeCourriel == 2);
            var contenuCourriel = reqBdCourrielReinitialisation.Select(x => x.Courriel1).FirstOrDefault();
            contenuCourriel = contenuCourriel?.Replace("$PrenomNomEtudiant", nomEtudiant).Replace("$NouveauMotDePasse",nouveauMdp);
            var message = new MailMessage("sachemcllmail@gmail.com", email, 
                reqBdCourrielReinitialisation.Select(x => x.Titre).FirstOrDefault(),
                contenuCourriel);
            client.Port = Portcourriel;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("sachemcllmail@gmail.com",
                System.Configuration.ConfigurationManager.AppSettings.Get("EmailSachemMDP"));
            message.BodyEncoding = Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                client.Send(message);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (SachemIdentite.ObtenirTypeUsager(Session) != TypeUsagers.Aucun && SachemIdentite.ObtenirTypeUsager(Session) != TypeUsagers.Etudiant)
            {
                return RedirectToAction("Index", "DossierEtudiant", null);
            }

            if (CookieConnexionExiste())
            {
                var personneCookie = new Personne();
                var cookieMaintenirConnexion = Request.Cookies.Get("SACHEMConnexion");
                personneCookie.NomUsager = cookieMaintenirConnexion?["NomUsager"];
                personneCookie.MP = Crypto.Decrypt(cookieMaintenirConnexion?["MP"], 
                    System.Configuration.ConfigurationManager.AppSettings.Get("CryptoKey"));
                personneCookie.SouvenirConnexion = true;

                return View(personneCookie);
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string nomUsager, string mp, bool souvenirConnexion)
        {
            var mdpPlain = mp;
            var personneBd = new Personne();
            var reqBdPersonne =
                _dataRepository.WherePersonne(x=> x.Matricule.Substring(2) == nomUsager || x.NomUsager == nomUsager,true);
            const int statutAccepte = 3;

            if (mdpPlain == "")
                ModelState.AddModelError("MP", Messages.ChampRequis);
            else if (nomUsager == null)
            {
                ModelState.AddModelError("NomUsager", Messages.ChampRequis);
            }
            else if (Regex.IsMatch(nomUsager, @"^\d+$") && nomUsager.Length == 7)
            {
                if (reqBdPersonne.Any(x => x.Matricule.Substring(2) == nomUsager))
                {
                    personneBd = reqBdPersonne.FirstOrDefault(x => x.Matricule.Substring(2) == nomUsager);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Messages.AccountConnexionErreur);
                }
                    
            }
            else if (reqBdPersonne.Any(x => x.NomUsager == nomUsager))
            {
                personneBd = reqBdPersonne.FirstOrDefault(x => x.NomUsager == nomUsager);
            }
            else
            {
                ModelState.AddModelError(string.Empty, Messages.AccountConnexionErreur);
            }

            mp = SachemIdentite.EncrypterChaine(mp);

            if (personneBd != null && personneBd.MP != mp)
                ModelState.AddModelError(string.Empty, Messages.AccountConnexionErreur);

            if (personneBd == null) return View();

            if (!ModelState.IsValid)
            {
                personneBd.MP = "";
                return View(personneBd);
            }

            var reqBdInscription = _dataRepository.WhereInscription(i => i.id_Pers == personneBd.id_Pers,true);

            var typeinscr = reqBdInscription.Select(i => i.id_TypeInscription).FirstOrDefault();
            var idinscr = reqBdInscription.Select(i => i.id_Inscription).FirstOrDefault();

            SessionBag.Current.id_Inscription = idinscr != 0 ? idinscr : 0;

            if (personneBd.id_TypeUsag == 1)
            {
                if (typeinscr > 1)
                {
                    SessionBag.Current.id_TypeUsag = TypeUsagers.Tuteur;
                }
                else
                {
                    SessionBag.Current.id_TypeUsag = typeinscr == 1 ? TypeUsagers.Eleve : TypeUsagers.Etudiant;
                }
            }
            else
            {
                SessionBag.Current.id_TypeUsag = personneBd.id_TypeUsag;
            }

            var idSuperviseur =
                _dataRepository.WhereJumelage(j => j.id_Enseignant == personneBd.id_Pers, true)
                    .Select(j => j.id_Enseignant)
                    .FirstOrDefault();

            SessionBag.Current.idSuperviseur = idSuperviseur != 0 ? idSuperviseur : 0;


            AjoutInfoConnection(personneBd);

            SessionBag.Current.id_Pers = personneBd.id_Pers;
            if (souvenirConnexion)
                CreerCookieConnexion(nomUsager, mdpPlain);
            else
                SupprimerCookieConnexion();

            if (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Eleve)
            {
                return RedirectToAction("Details", "DossierEtudiant", new {id = SessionBag.Current.id_Inscription});
            }

            if (SachemIdentite.TypeListeProf.Contains(SachemIdentite.ObtenirTypeUsager(Session)) ||
                SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Tuteur)
            {
                if (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Super)
                {
                    return RedirectToAction("Index", "Enseignant");
                }

                return RedirectToAction("Index", "DossierEtudiant");
            }

            var inscription = reqBdInscription.FirstOrDefault();

            if (inscription != null && inscription.id_Inscription == statutAccepte &&
                inscription.ContratEngagement == false)
            {
                return RedirectToAction("Index", "ContratEngagement");
            }
            return RedirectToAction("Index", "Inscription");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (SachemIdentite.ObtenirTypeUsager(Session) != TypeUsagers.Aucun)
                return RedirectToAction("Error", "Home", null);
            ViewBag.id_Sexe = _dataRepository.ListeSexe();
            ViewBag.Autorisation = false;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Personne personne)
        {
            ViewBag.id_Sexe = _dataRepository.ListeSexe();
            var reqBdPersonne = _dataRepository.WherePersonne(x => x.Matricule == personne.Matricule, true);
            var validation = ConfirmeMdp(personne.MP, personne.ConfirmPassword);

            if (!validation)
                return View(personne);

            if (personne.Matricule7 == null)
                ModelState.AddModelError("Matricule7", Messages.ChampRequis);
            else if (personne.Matricule7.Length != 7 || !personne.Matricule.All(char.IsDigit))
                ModelState.AddModelError("Matricule7", Messages.LongueurDeSeptCaracteres);
            else if (reqBdPersonne.Any(x => x.Matricule == personne.Matricule && x.MP != null))
                ModelState.AddModelError(string.Empty, Messages.AccountExisteDeja);
            else if (reqBdPersonne.Any(x => x.Matricule != personne.Matricule))
                ModelState.AddModelError(string.Empty, Messages.AccountCreerErreurEtudiantNonInscrit);
            else
            {
                var etudiantBd =
                    reqBdPersonne.FirstOrDefault(x => x.Matricule == personne.Matricule);

                if (etudiantBd != null && (personne.DateNais != etudiantBd.DateNais || personne.id_Sexe != etudiantBd.id_Sexe))
                    ModelState.AddModelError(string.Empty, Messages.AccountCreerErreurEtudiantNonInscrit);
                else
                {
                    if (etudiantBd != null)
                    {
                        etudiantBd.Courriel = personne.Courriel;
                        etudiantBd.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
                        etudiantBd.MP = personne.MP;
                        etudiantBd.ConfirmPassword = personne.ConfirmPassword;
                        SachemIdentite.EncrypterMpPersonne(ref etudiantBd);

                        _dataRepository.EditPersonne(etudiantBd);

                        AjoutInfoConnection(etudiantBd);
                    }
                    SessionBag.Current.id_TypeUsag = TypeUsagers.Etudiant;
                    TempData["Success"] = Messages.AccountCree;
                    return RedirectToAction("Index", "Inscription");
                }
            }
            return View(personne);
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            if (SachemIdentite.ObtenirTypeUsager(Session) != TypeUsagers.Aucun)
                return RedirectToAction("Error", "Home", null);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string courriel)
        {
            var reqBdPersonne = _dataRepository.WherePersonne(x => x.Courriel == courriel, true);
            if (reqBdPersonne.Any(y => y.Courriel == courriel && y.Actif))
            {
                const string caracterePossible = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?";
                var nouveauMdp = "";
                var r = new Random();

                for (var i = 0; i < 10; i++)
                    nouveauMdp = nouveauMdp + caracterePossible[r.Next(0, caracterePossible.Length)];

                if (EnvoyerCourriel(courriel, nouveauMdp, reqBdPersonne.Select(x => x.PrenomNom).FirstOrDefault()))
                {
                    var utilisateur =
                        reqBdPersonne.FirstOrDefault(x => x.Courriel == courriel);
                    if (utilisateur != null)
                    {
                        utilisateur.MP = nouveauMdp;
                        utilisateur.MP = SachemIdentite.EncrypterChaine(utilisateur.MP);
                        _dataRepository.EditPersonne(utilisateur);
                    }
                    ViewBag.Success = Messages.AccountEnvoieMotDePasseParCourriel(courriel);
                }
                else
                {
                    ModelState.AddModelError(string.Empty,
                        Messages.AccountEnvoiCourrielImpossiblePortBloque(Portcourriel.ToString()));
                }
            }
            else
            {
                ModelState.AddModelError("Courriel", Messages.AccountForgotPasswordErreurAucunUsager);
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult ModifierPassword()
        {
            if (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Aucun)
                return RedirectToAction("Error", "Home", null);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierPassword(Personne personne, string modifier, string annuler)
        {
            int idpersonne = SessionBag.Current.id_Pers;
            string ancienmdpbd = SessionBag.Current.MP;

            if (personne.AncienMotDePasse == null)
                ModelState.AddModelError("AncienMotDePasse", Messages.ChampRequis);

            if (!ConfirmeMdp(personne.MP, personne.ConfirmPassword))
                return View(personne);

            if (personne.AncienMotDePasse == null || personne.MP == null || personne.ConfirmPassword == null)
                return View(personne);

            if (SachemIdentite.EncrypterChaine(personne.AncienMotDePasse) != ancienmdpbd)
            {
                ModelState.AddModelError("AncienMotDePasse", Messages.AccountPasswordErreurAncienMotDePasseInvalide);
                return View(personne);
            }

            var utilisateur = _dataRepository.FindPersonne(idpersonne);

            if (utilisateur != null)
            {
                utilisateur.MP = personne.MP;
                utilisateur.ConfirmPassword = personne.ConfirmPassword;
                SachemIdentite.EncrypterMpPersonne(ref utilisateur);
                SessionBag.Current.MP = utilisateur.MP;
                SupprimerCookieConnexion();
                _dataRepository.EditPersonne(utilisateur);
            }
            ViewBag.Success = Messages.AccountMotDePasseModifie;

            return View(personne);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account", null);
        }

        private bool ConfirmeMdp(string s1, string s2)
        {
            if (s1 == null || s2 == null)
            {
                ModelState.AddModelError("MP", Messages.ChampRequis);
                ModelState.AddModelError("ConfirmPassword", Messages.ChampRequis);
                return false;
            }

            if (s1 == s2) return true;

            ModelState.AddModelError("ConfirmPassword", Messages.MotsDePasseDoiventEtreIdentiques);
            return false;
        }

        private static void AjoutInfoConnection(Personne personne)
        {
            SessionBag.Current.NomUsager = personne.NomUsager;
            SessionBag.Current.Matricule7 = personne.Matricule7;
            SessionBag.Current.NomComplet = personne.PrenomNom;
            SessionBag.Current.MP = personne.MP;
            SessionBag.Current.id_Pers = personne.id_Pers;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}