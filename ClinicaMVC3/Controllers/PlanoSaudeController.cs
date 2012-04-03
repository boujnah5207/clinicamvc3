using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaMVC3.Models;

namespace ClinicaMVC3.Controllers
{
    [HandleError, Authorize(Roles = "Administrador")]
    public class PlanoSaudeController : Controller
    {
        String tituloCadastro = "Planos de Saúde";

        //
        // GET: /PlanoSaude/
        
        public ActionResult Index()
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {                
                return View(db.PlanoSaude.ToList());    
            }
            
        }

        //
        // GET: /PlanoSaude/Details/5

        public ActionResult Details(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.PlanoSaude.Find(id));
            }
            
        }

        //
        // GET: /PlanoSaude/Create

        public ActionResult Create()
        {
            ViewBag.Title = tituloCadastro;
            return View();
        } 

        //
        // POST: /PlanoSaude/Create

        [HttpPost]
        public ActionResult Create(PlanoSaude planosaude)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                using (var db = new ClinicaEntities())
                {
                    db.PlanoSaude.Add(planosaude);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
                return View("Error");
            }
        }
        
        //
        // GET: /PlanoSaude/Edit/5
 
        public ActionResult Edit(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.PlanoSaude.Find(id));
            }
        }

        //
        // POST: /PlanoSaude/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PlanoSaude planosaude)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                using (var db = new ClinicaEntities())
                {
                    db.Entry(planosaude).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
                return View("Error");
            }
        }

        //
        // GET: /PlanoSaude/Delete/5
 
        public ActionResult Delete(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.PlanoSaude.Find(id));
            }
        }

        //
        // POST: /PlanoSaude/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, PlanoSaude planosaude)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                using (var db = new ClinicaEntities())
                {
                    db.Entry(planosaude).State = System.Data.EntityState.Deleted;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
                return View("Error");
            }
        }
    }
}
