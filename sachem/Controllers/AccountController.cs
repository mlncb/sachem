﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;
using Microsoft.Owin.Security;
using sachem.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace sachem.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();


        
        public AccountController()
        {

        }
        
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Personne infos)
        {
            string mdpPlain = infos.MP;

            //Validations des champs et de la connection
            if (mdpPlain == "")
                ModelState.AddModelError("MP", Messages.U_001);
            else
                if (infos.NomUsager == null)
                ModelState.AddModelError("NomUsager", Messages.U_001);
            else
                    if (!db.Personne.Any(x => x.NomUsager == infos.NomUtilisateur || x.Matricule.Substring(2) == infos.NomUtilisateur))
                        ModelState.AddModelError(string.Empty, Messages.I_017());  //Erreur de connection
            else
            {
                //Encrypter le mdp et tester la connection
                SachemIdentite.encrypterMPPersonne(ref infos);
                Personne PersonneBD = db.Personne.AsNoTracking().Where(x => x.NomUsager == infos.NomUtilisateur || x.Matricule.Substring(2) == infos.NomUtilisateur).FirstOrDefault();
                //Vérifie si le mot de passe concorde 
                if (!db.Personne.Any(x => x.id_Pers == PersonneBD.id_Pers && x.MP == infos.MP))
                    ModelState.AddModelError(string.Empty, Messages.I_017()); //Erreur de connection

                if (!ModelState.IsValid)
                {
                    return View(infos); //Retourne le formulaire rempli avec l'erreur
                }
            }

            return View();
        }
        
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            //Modifié pour accommoder nos tables maison. Extrait du PAM adapté pour le SACHEM
            //Si connecté, on ne peux pas s'inscrire
            if (SachemIdentite.ObtenirTypeUsager(Session) != TypeUsagers.Aucun)
                return RedirectToAction("Error", "Home", null);
            //Récupère les sexes
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
            ViewBag.Autorisation = false;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Personne personne)
            //Fortement Extrait du PAM, approuvé par J, Lainesse
        {
            //Get le sexe du formulaire
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");

            if (personne.MP == null)
            {
                ModelState.AddModelError("MP", Messages.U_001); //requis
                ModelState.AddModelError("ConfirmPassword", Messages.U_001); //requis
            }
            if (personne.Matricule7 == null)
                ModelState.AddModelError("Matricule7", Messages.U_001); //requis
            else if (personne.Matricule7.Length != 7 || !personne.Matricule.All(char.IsDigit)) //vérifie le matricule
                ModelState.AddModelError("Matricule7", Messages.U_004); //longueur
            else if (db.Personne.Any(x => x.Matricule == personne.Matricule && x.MP != null))
                ModelState.AddModelError(string.Empty, Messages.I_025()); //Un compte existe déjà pour cet étudiant.
            else if (!db.Personne.Any(x => x.Matricule == personne.Matricule))
                ModelState.AddModelError(string.Empty, Messages.I_027()); //Aucun étudiant ne correspond aux données saisies. 
            else
            {
                //Sort la personne de la BD pour la compléter
                //Exemple du PAM grande inspiration
                Personne EtudiantBD = db.Personne.AsNoTracking().Where(x => x.Matricule == personne.Matricule).FirstOrDefault();

                //Erreur si les infos ne concordent pas
                if (personne.DateNais != EtudiantBD.DateNais || personne.id_Sexe != EtudiantBD.id_Sexe)
                    ModelState.AddModelError(string.Empty, Messages.I_027());
                else
                {
                    //Mise à jour des infos
                    EtudiantBD.Courriel = personne.Courriel;
                    EtudiantBD.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
                    EtudiantBD.MP = personne.MP;
                    SachemIdentite.encrypterMPPersonne(ref EtudiantBD);

                    db.Entry(EtudiantBD).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.Success = Messages.I_026();
                 
                    return View();
                }
            }
            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return View(personne);
        }


        // GET: /Account/Mot de passe oublié
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            //Un utilisateur connecté ne peut pas récupérer sont mot de passe.
            if (SachemIdentite.ObtenirTypeUsager(Session) != TypeUsagers.Aucun)
                return RedirectToAction("Error", "Home", null);

            return View();
        }

        //
        // POST: /Account/Mot de passe oublié
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string courriel)
        {
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage("sachemcllmail@gmail.com",courriel,"test","bonjour");//Test d'envoi de email
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod=SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials=new NetworkCredential("sachemcllmail@gmail.com", "sachemadmin#123");//information de connection au email d'envoi de message de SACHEM
            message.BodyEncoding = Encoding.UTF8;
            client.Send(message);

            return View();
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}