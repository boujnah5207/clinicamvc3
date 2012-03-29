using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaMVC.Models;
using System.Data;


//Paciente
namespace ClinicaMVC.Controllers
{

    [HandleError]
    public class PacienteController : Controller
    {
		String tituloCadastro = "Pacientes";
		
        //
        // GET: /Paciente/

        public ActionResult Index()
        {
		    ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {                
                return View(db.Paciente.ToList());    
            }
        }

        //
        // GET: /Paciente/Details/5

        public ActionResult Details(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.Paciente.Find(id));
            }		
        }

        //
        // GET: /Paciente/Create

        public ActionResult Create()
        {
            ViewBag.Title = tituloCadastro;

            using (var db = new ClinicaEntities())
            {                
                return View();
            }

            
        } 

        //
        // POST: /Paciente/Create

        [HttpPost]
        public JsonResult Create(Paciente paciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new ClinicaEntities())
                    {
                        // Se o código do paciente é maior que zero, entendemos que existe registro para tal.
                        // Então nós "atualizaremos" ele.                    
                        if (paciente.PacienteId > 0)
                        {

                                var Telefones = db.PacienteTelefone.Where(p => p.PacienteId == paciente.PacienteId);
                                foreach (PacienteTelefone pt in Telefones)
                                    db.PacienteTelefone.Remove(pt);

                                foreach (PacienteTelefone pt in Telefones)
                                    db.PacienteTelefone.Add(pt);

                                db.Entry(paciente).State = EntityState.Modified;

                        }
                        //Perform Save
                        else
                        {
                            db.Paciente.Add(paciente);
                        }

                        db.SaveChanges();

                    }


                    // If Sucess== 1 then Save/Update Successfull else there it has Exception
                    return Json(new { Success = 1, SalesID = paciente.PacienteId , ex = "" });
                }
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }

            return Json(new { Success = 0, ex = new Exception("Unable to save").Message.ToString() });
        }

        
        //
        // GET: /Paciente/Edit/5
 
        public ActionResult Edit(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.Paciente.Find(id));
            }
        }

        //
        // POST: /Paciente/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Paciente paciente)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                using (var db = new ClinicaEntities())
                {
                    db.Entry(paciente).State = System.Data.EntityState.Modified;
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
        // GET: /Paciente/Delete/5
 
        public ActionResult Delete(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.Paciente.Find(id));
            }
        }

        //
        // POST: /Paciente/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Paciente paciente)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                using (var db = new ClinicaEntities())
                {
                    db.Entry(paciente).State = System.Data.EntityState.Deleted;
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
