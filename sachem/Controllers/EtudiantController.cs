﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using System.Security.Cryptography;// pour encripter mdp
using System.Text;
using System.Web.Services;

namespace sachem.Controllers
{
    public class EtudiantController : RechercheEtudiantController
    {
       

        #region enCommentaire
        // GET: Etudiant
        //public ActionResult Index()
        //{
        //    List<object> lpersonne = new List<object>();

        //    //var test = from d in db.Personne
        //    //           where 

        //    //var test = from d in db.EtuProgEtude
        //    //    where d.id_Etu == d.Personne.id_Pers && d.id_EtuProgEtude == d.ProgrammeEtude.id_ProgEtu
        //    //    select d;

        //    var personne = from c in db.Personne
        //                   where c.Actif == true && c.id_TypeUsag == 1
        //                   select c;
        //    int nbpers = personne.Count();

        //    //afficher tous les étudiants
        //    //foreach (var pers in personne)
        //    //{
        //    //    var pEtu = (from p in db.EtuProgEtude
        //    //               where pers.id_Pers == p.id_Etu
        //    //                orderby p.id_Sess descending
        //    //                select p).FirstOrDefault();

        //    //}


        //    //pers.ProgEtu = ProgEtu.ToString();
        //    //// PROBLEME A REGLER, olivier filion a un programme qui n'existe pas dans BD programma etude





        //    //var id = db.EtuProgEtude.Find(pers.id_Pers);
        //    //    if (id != null)
        //    //        {
        //    //            pers.idpEtu = id.id_ProgEtu;
        //    //            var pEtu = db.ProgrammeEtude.Find(id.id_ProgEtu);
        //    //            string pe = pEtu.NomProg;
        //    //            pers.ProgEtu = pe;
        //    //            i = i + 1;
        //    //        }


        //    //i = i + 0;
        //    //    var pEtu = (from p in db.EtuProgEtude
        //    //                where
        //    //                orderby p.id_Sess descending//FirstOrDefault
        //    //                select p).FirstOrDefault();
        //    //string pe = pEtu.NomProg;
        //    //pers.ProgEtu = pe;
        //    //var pEtu = from d in db.ProgrammeEtude
        //    //           where d.id_ProgEtu == id.id_ProgEtu
        //    //           orderby d.Annee descending
        //    //           select d;
        //    //var pEtu = db.ProgrammeEtude.Find();
        //    //string pe = pEtu.FirstOrDefault().ToString();
        //    //pers.ProgEtu = pe;
        //    //(From p In context.Persons Select p Order By age Descending).FirstOrDefault
        //    //}

        //    ListeSession();
        //    ListeCours();
        //    ListeGroupe();


        //    return View(personne.ToList());
        //}

        #endregion

        public ActionResult Index(int? page)
        {
            noPage = (page ?? noPage);
           
            var personne = from c in db.Personne   
                           where c.Actif == true && c.id_TypeUsag == 1
                           select c;

            return View(Rechercher().ToPagedList(noPage, 20));
        }

        #region enCommentaire
        //private IEnumerable<Personne> Rechercher()
        //{
        //    var personne = from c in db.Personne
        //                   where c.Actif == true && c.id_TypeUsag == 1
        //                   select c;
        //           if (!String.IsNullOrEmpty(recherche))
        //            {
        //                personne = personne.Where(c => c.Matricule7.Contains() || c.NomProg.Contains(recherche)) as IOrderedQueryable<ProgrammeEtude>;
        //            }
        //    }

////        var personne = from c in db.Personne
////                       where c.Actif == true && c.id_TypeUsag == 1
////                       select c;
////                if (Request.RequestType == "POST")
////                {
////                    string m = ViewBag.Mat;
////                    if (!String.IsNullOrEmpty(m))
////                    {
////                        //personne = null;
////                        personne = from c in db.Personne
////                                   where c.Actif == true && c.id_TypeUsag == 1
////                                   && c.Matricule7.Contains(m)
////                                   select c;
////    }
////}

            ////foreach (var pers in personne)
            ////{
            ////    var pidEtu = (from p in db.EtuProgEtude
            ////                  where pers.id_Pers == p.id_Etu
            ////                  orderby p.id_Sess descending
            ////                  select p).FirstOrDefault();

            ////    var pEtu = db.ProgrammeEtude.Find(pidEtu.id_ProgEtu);
            ////    pers.ProgEtu = pEtu.NomProg.ToString();

            ////}

            //    //on enregistre la recherche
            //    //Session["DernRechCours"] = sess + ";" + actif;
            //    //Session["DernRechCoursUrl"] = Request.Url?.LocalPath;


            //ListeSession();
            //ListeCours();
            //ListeGroupe();
        //    return personne.ToList();
        //}


        //private IEnumerable<Cours> Rechercher()
        //{
        //    var pers = 0;
        //    var actif = true;

        //    //Pour accéder à la valeur de cle envoyée en GET dans le formulaire
        //    //Request.QueryString["cle"]
        //    //Pour accéder à la valeur cle envoyée en POST dans le formulaire
        //    //Request.Form["cle"]
        //    //Cette méthode fonctionnera dans les 2 cas
        //    //Request["cle"]

        //    if (Request.RequestType == "GET" && personne["DernRechCours"] != null && (string)personne["DernRechCoursUrl"] == Request.Url?.LocalPath)
        //    {
        //        var anciennerech = (string)personne["DernRechCours"];
        //        var tanciennerech = anciennerech.Split(';');

        //        if (tanciennerech[0] != "")
        //        {
        //            pers = int.Parse(tanciennerech[0]);
        //        }
        //        if (tanciennerech[1] != "")
        //        {
        //            actif = bool.Parse(tanciennerech[1]);
        //        }

        //    }
        //    else
        //    {
        //        //La méthode String.IsNullOrEmpty permet à la fois de vérifier si la chaine est NULL (lors du premier affichage de la page ou vide, lorsque le paramètre n'est pas appliquée 
        //        if (!string.IsNullOrEmpty(Request.Form["personne"]))
        //            pers = Convert.ToInt32(Request.Form["personne"]);
        //        //si la variable est null c'est que la page est chargée pour la première fois, donc il faut assigner la session à la session en cours, la plus grande dans la base de données
        //        else if (Request.Form["Session"] == null)
        //            pers = db.Personne.Max(s => s.id_Sess);

        //        //la méthode Html.checkbox crée automatiquement un champ hidden du même nom que la case à cocher, lorsque la case n'est pas cochée une seule valeur sera soumise, par contre lorsqu'elle est cochée
        //        //2 valeurs sont soumises, il faut alors vérifier que l'une des valeurs est à true pour vérifier si elle est cochée
        //        if (!string.IsNullOrEmpty(Request.Form["Actif"]))
        //            actif = Request.Form["Actif"].Contains("true");
        //    }

        //    ViewBag.Actif = actif;

        //    ListeSession(pers);

        //    //var personne = from c in db.Cours
        //    //            where (db.Groupe.Any(r => r.id_Cours == c.id_Cours && r.id_Sess == sess) || sess == 0)
        //    //            && c.Actif == actif
        //    //            orderby c.Code
        //    //            select c;

        //    var personne = from c in db.Personne
        //                   where c.Actif == true && c.id_TypeUsag == 1
        //                   && c.id
        //                   select c;


        //    //on enregistre la recherche
        //    personne["DernRechCours"] = pers + ";" + actif;
        //    personne["DernRechCoursUrl"] = Request.Url?.LocalPath;

        //    return personne.ToList();
        //}
        #endregion

      
        // GET: Etudiant/Details/5

        // GET: Etudiant/Create
        public ActionResult Create()
        {

            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag");
            return View();
        }

        // POST: Etudiant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule,MP,Courriel,Telephone,DateNais,Actif")] Personne personne,int? page)
        {
            Valider(personne);
            if (ModelState.IsValid)
            {
                personne.MP = encrypterChaine(personne.MP); // Encryption du mot de passe
                db.Personne.Add(personne);
                db.SaveChanges();
                TempData["Success"] = Messages.I_010(personne.Matricule); // Message afficher sur la page d'index confirmant la création
                return RedirectToAction("Index");
            }
            //personne.ConfMP = personne.MP;

            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Etudiant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            //retroune la liste de programme qui relié à l'élève
            var Prog = from d in db.EtuProgEtude
                       where d.id_Etu == personne.id_Pers
                       select d;
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            ViewBag.id_Programme = new SelectList(db.ProgrammeEtude, "id_ProgEtu", "nomProg");
            ViewBag.id_Session = new SelectList(db.Session, "id_Sess", "NomSession");
            return View(Tuple.Create(personne, Prog.AsEnumerable()));
        }

        public void FillDropDownlist()
        {  
        }
        // POST: Etudiant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,AncienMotDePasse,ConfirmPassword,Matricule,MP,Courriel,Telephone,DateNais,Actif,id_Programme,id_Session")] Personne personne)
        {
            //if (personne.AncienMotDePasse != null)
            //{
            //    personne.MP = personne.AncienMotDePasse;
            //    personne.MP = encrypterChaine(personne.AncienMotDePasse); // Appel de la méthode qui encrypte le mot de passe
            //    personne.ConfirmPassword = encrypterChaine(personne.ConfirmPassword); // Appel de la méthode qui encrypte la confirmation du mot de passe
            //}
            //else
            //{
            //    var Enseignant = from c in db.Personne
            //                     where (c.id_Pers == personne.id_Pers)
            //                     select c.MP;
            //    personne.MP = Enseignant.SingleOrDefault();
            //    personne.ConfirmPassword = personne.MP;
            //}
            Valider(personne);
            if (ModelState.IsValid)
            {
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Etudiant/Delete/5
        //exécuté lorsqu'un étudiant est supprimé
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            
            return View(personne);
        }

        // POST: Etudiant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,int? page)
        {
            var pageNumber = page ?? 1;
            // Verifie si l'étudiant est relié a un groupe
            /*if (db.Groupe.Any(a => a.Personne == id)) 
            {
                ModelState.AddModelError(string.Empty, Messages.I_014);

            }*/
            // Vérifier si l'enseignant est relié a un jumelage
            //if (db.Jumelage.Any(g => g.id_Enseignant == id)) 
            //{
            //    ModelState.AddModelError(string.Empty, Messages.I_000);
            //}
            //if (ModelState.IsValid)
            //{
                //trouve la personne à supprimer
                Personne personne = db.Personne.Find(id);
            //suppression de la personne dans tout les tables qu'on la retrouve
            var etuProgEtu = db.EtuProgEtude.Where(x => x.id_Etu == personne.id_Pers);
            db.EtuProgEtude.RemoveRange(etuProgEtu);
            var groupeEtu = db.GroupeEtudiant.Where(y => y.id_Etudiant == personne.id_Pers);
            db.GroupeEtudiant.RemoveRange(groupeEtu);
            var Jumul = db.Jumelage.Where(z => z.id_InscEleve == personne.id_Pers);
            db.Jumelage.RemoveRange(Jumul);
            var Inscri = db.Inscription.Where(a => a.id_Pers == personne.id_Pers);
            db.Inscription.RemoveRange(Inscri);
            var CoursSuiv = db.CoursSuivi.Where(b => b.id_Pers == personne.id_Pers);
            db.CoursSuivi.RemoveRange(CoursSuiv);


            //suppresion et sauvegarde dans la bd
            db.Personne.Remove(personne);
            db.SaveChanges();
            TempData["Success"] = Messages.I_028(personne.NomPrenom);
            //redirection à l'index après la suppression
            return RedirectToAction("Index");
        }
        public ActionResult deleteProgEtu([Bind(Include = "Matricule")] Personne personne, [Bind(Include = "id_EtuProgEtude")]EtuProgEtude ProgEtu)
        {
            //Personne personne = db.Personne.Find(id);

            //supprimer dans tous les tables 
            //faire apparaitre le message
            return RedirectToAction("Index");
        }

        //Méthode pour encrypter le de mot de passe.
        public static string encrypterChaine(string mdp)
        {
            byte[] Buffer;
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            Buffer = Encoding.UTF8.GetBytes(mdp);
            return BitConverter.ToString(provider.ComputeHash(Buffer)).Replace("-", "").ToLower();
        }

        private void Valider([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,MP,ConfirmPassword,Courriel,DateNais,Actif")] Personne personne)
        {
            // Verifier si le nom d'usager existe ou s'il a entré son ancien nom
            if (db.Personne.Any(x => x.Matricule == personne.Matricule))
                ModelState.AddModelError(string.Empty, Messages.I_004(personne.Matricule));

            //if (personne.MP != personne.ConfirmPassword) // Verifier si le premier mot de passe correspond au deuxieme mot de passe
            //    ModelState.AddModelError(string.Empty, Messages.C_001());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
