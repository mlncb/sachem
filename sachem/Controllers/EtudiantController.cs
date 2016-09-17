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
        //fonction pour formatter le numéro de téléphone avant de mettre dans la bd
        public static string FormatTelephone(string s)
        {
            var charsToRemove = new string[] { ".", "-", "(", " ", ")" };
            foreach (var c in charsToRemove)
            {
                s = s.Replace(c, string.Empty);
            }
            return s;
        }
        //fonction qui remet le numéro de téléphone dans le bon format
        public static string RemettreTel(string a)

        {
            string modif;
            modif = a.Insert(0, "(");
            modif = modif.Insert(4, ")");
            modif = modif.Insert(5, " ");
            modif = modif.Insert(9,"-");
            return modif;
        }
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
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,Matricule,MP,ConfirmPassword,Courriel,Telephone,DateNais")] Personne personne,int? page)
        {
            personne.id_TypeUsag = 1;
            personne.Actif = true;
            personne.Telephone = FormatTelephone(personne.Telephone);

            Valider(personne);
            if (ModelState.IsValid)
            {
                personne.MP = encrypterChaine(personne.MP); // Encryption du mot de passe
                personne.ConfirmPassword = encrypterChaine(personne.ConfirmPassword); // Encryption du mot de passe 
                
                db.Personne.Add(personne);
                db.SaveChanges();
                personne.Telephone = RemettreTel(personne.Telephone);
                TempData["Success"] = Messages.I_010(personne.Matricule); // Message afficher sur la page d'index confirmant la création
                return RedirectToAction("Index");
            }

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
            //return View(Tuple.Create(personne, Prog.AsEnumerable()));
            PersonneEtuProgParent epep = new PersonneEtuProgParent();
            epep.personne = personne;
            epep.epe = Prog.ToList();
            return View(epep);
        }

        public void FillDropDownlist()
        {  
        }
        // POST: Etudiant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule7,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            PersonneEtuProgParent pepp = new PersonneEtuProgParent();
            Personne p = db.Personne.Find(personne.id_Pers);
            p.id_TypeUsag = 1;
            //p.Matricule = personne.Matricule;
            var idSexe = (from d in db.Personne
                          where d.id_Pers == p.id_Pers
                          select d).FirstOrDefault();
            p.id_Sexe = idSexe.id_Sexe;

            db.SaveChanges();
            pepp.personne = p;

            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", pepp.personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", pepp.personne.id_TypeUsag);
            ViewBag.id_Programme = new SelectList(db.ProgrammeEtude, "id_ProgEtu", "nomProg");
            ViewBag.id_Session = new SelectList(db.Session, "id_Sess", "NomSession");

            var etuprog = new EtuProgEtude();
            //Ajout du programme d'étude (Si l'étudiant rajoute les champs)
            if (Request.Form["id_Programme"] != "" && Request.Form["id_Session"] != "")
            {
                etuprog.id_ProgEtu = Int32.Parse(Request.Form["id_Programme"]);
                etuprog.id_Sess = Int32.Parse(Request.Form["id_Session"]);
                etuprog.id_Etu = personne.id_Pers;
                db.EtuProgEtude.Add(etuprog);
                db.SaveChanges();
            }
            
            var Prog = from d in db.EtuProgEtude
                       where d.id_Etu == pepp.personne.id_Pers
                       select d;

            pepp.epe = Prog.ToList();
            return View(pepp);
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
        //fonction qui supprime un programme d'étude à oartir de la page modifier
        public ActionResult deleteProgEtu(int id)
        {
            var etuProgEtu = db.EtuProgEtude.Where(x => x.id_EtuProgEtude == id);
            db.EtuProgEtude.RemoveRange(etuProgEtu);
            db.SaveChanges();
            //faire apparaitre le message
            TempData["Success"] = Messages.I_016("");
            //retourne à l'index
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
        //fonction de validation
        private void Valider([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,MP,ConfirmPassword,Courriel,DateNais,Actif")] Personne personne)
        {
            // Verifier si le matricule existe déja dans la BD
            if (db.Personne.Any(x => x.Matricule == personne.Matricule))
                ModelState.AddModelError(string.Empty, Messages.I_004(personne.Matricule));
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
