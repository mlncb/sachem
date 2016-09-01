﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class ConsulterCoursController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        // GET: ConsulterCours
        public ActionResult Index()
        {
            return View(AfficherCoursAssignes());
            //db.Cours.ToList()
        }


        //Fonction pour afficher lescours assignés à l'utilisateur connecté
        [NonAction]
        private IEnumerable<Cours> AfficherCoursAssignes()
        {
            var sess = 0;

            //Pour accéder à la valeur de cle envoyée en GET dans le formulaire
            //Request.QueryString["cle"]
            //Pour accéder à la valeur cle envoyée en POST dans le formulaire
            //Request.Form["cle"]
            //Cette méthode fonctionnera dans les 2 cas
            //Request["cle"]

            if (Request.RequestType == "GET" && Session["DernRechCours"] != null && (string)Session["DernRechCoursUrl"] == Request.Url?.LocalPath)
            {
                var anciennerech = (string)Session["DernRechCours"];
                var tanciennerech = anciennerech.Split(';');

                if (tanciennerech[0] != "")
                {
                    sess = int.Parse(tanciennerech[0]);
                }

            }
            else
            {
                //La méthode String.IsNullOrEmpty permet à la fois de vérifier si la chaine est NULL (lors du premier affichage de la page ou vide, lorsque le paramètre n'est pas appliquée 
                if (!string.IsNullOrEmpty(Request.Form["Session"]))
                    sess = Convert.ToInt32(Request.Form["Session"]);
                //si la variable est null c'est que la page est chargée pour la première fois, donc il faut assigner la session à la session en cours, la plus grande dans la base de données
                else if (Request.Form["Session"] == null)
                    sess = db.Session.Max(s => s.id_Sess);
            }
            
            ListeSession(sess);

            //idTypeUSager 2 = enseignant
            //idTypeUSager 3 = responsable sachem

            var cours = from c in db.Cours
                        where (db.Groupe.Any(r => r.id_Cours == c.id_Cours && r.id_Sess == sess) || sess == 0)
                        orderby c.Code //sont-ils toujours actifs?
                        select c;



            

            //on enregistre la recherche
            Session["DernRechCours"] = sess + ";";
            Session["DernRechCoursUrl"] = Request.Url?.LocalPath;

            return cours.ToList();
        }

        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeSession(int Session = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));

            ViewBag.Session = slSession;
        }
























        // GET: ConsulterCours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: ConsulterCours/Create
        public ActionResult Create()
        {
            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess");
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison");
            return View();
        }

        // POST: ConsulterCours/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Sess,id_Saison,Annee")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Session.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess", session.id_Sess);
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison", session.id_Saison);
            return View(session);
        }

        // GET: ConsulterCours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess", session.id_Sess);
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison", session.id_Saison);
            return View(session);
        }

        // POST: ConsulterCours/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Sess,id_Saison,Annee")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess", session.id_Sess);
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison", session.id_Saison);
            return View(session);
        }

        // GET: ConsulterCours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: ConsulterCours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Session.Find(id);
            db.Session.Remove(session);
            db.SaveChanges();
            return RedirectToAction("Index");
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
