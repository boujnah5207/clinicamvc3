using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaMVC3.Models;
using System.Data;


//Paciente
namespace ClinicaMVC3.Controllers
{

    [HandleError,Authorize(Roles = "Secretaria")]
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
            ViewBag.Method = "Detail";
            ClinicaEntities db = new ClinicaEntities();
            try
            {
                SelectListTelefones(db);
                return View("Create", db.Paciente.Find(id));

            }
            finally
            {
                db.Dispose();
            }
        }

        //
        // GET: /Paciente/Create

        public ActionResult Create()
        {
            ViewBag.Title = tituloCadastro;
            ViewBag.Method = "Insert";

            using (var db = new ClinicaEntities())
            {
                SelectListTelefones(db);
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

                    if (paciente.PacienteTelefone.Count == 0)
                    {
                        return Json(new { Success = 0, ex = "O paciente deve possuir ao menos um telefone cadastrado!" });
                    }

                    using (var db = new ClinicaEntities())
                    {
                        // Se o código do paciente é maior que zero, entendemos que existe registro para tal.
                        // Então nós "atualizaremos" ele.                    
                        if (paciente.PacienteId > 0)
                        {

                            var selPacienteTelefone = db.PacienteTelefone.Where(p => p.PacienteId == paciente.PacienteId);

                            foreach (PacienteTelefone pt in selPacienteTelefone)
                            {
                                db.PacienteTelefone.Remove(pt);
                            }

                            foreach (PacienteTelefone pt in paciente.PacienteTelefone)
                            {
                                db.PacienteTelefone.Add(pt);
                            }

                            db.Entry(paciente).State = EntityState.Modified;

                        }
                        //Perform Save
                        else
                        {
                            db.Paciente.Add(paciente);
                        }

                        db.SaveChanges();

                    }

                    return Json(new { Success = 1, SalesID = paciente.PacienteId, ex = "" });
                }
                else
                {
                    ValidateModel(paciente);
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {
                    return Json(new { Success = 0, ex = ex.InnerException.ToString() });
                }
                else
                {
                    return Json(new { Success = 0, ex = ex.Message.ToString() });
                }
            }

            return Json(new { Success = 0, ex = new Exception("Unable to save").Message.ToString() });
        }


        //
        // GET: /Paciente/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.Title = tituloCadastro;
            ViewBag.Method = "Edit";
            ClinicaEntities db = new ClinicaEntities();
            try
            {
                SelectListTelefones(db);
                return View("Create", db.Paciente.Find(id));
            }
            finally
            {
                db.Dispose();
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
                ClinicaEntities db = new ClinicaEntities();

                try
                {
                    db.Entry(paciente).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                finally
                {
                    db.Dispose();
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
                /*ViewBag.Error = e;*/
                TempData["Error"] = e;
                return RedirectToAction("Error", "Error");
                /*return View("Error");*/
            }
        }


        // Aux 

        public JsonResult SelectListPacientes()
        {
            using (var db = new ClinicaEntities())
            {
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (Paciente item in db.Paciente.ToList())
                {
                    list.Add(
                        new SelectListItem()
                        {
                            Value = item.PacienteId.ToString(),
                            Text = item.Nome,
                            Selected = false
                        });
                }

                return this.Json(list);
            }
        }


        public ActionResult PacienteDialog()
        {
            ViewBag.Title = tituloCadastro;
            ViewBag.Method = "Insert";

            using (var db = new ClinicaEntities())
            {
                SelectListTelefones(db);
                ViewBag.isDialog = true;
                return PartialView("PacientePartial");
            }

        }



        private void SelectListTelefones(ClinicaEntities db)
        {
            ViewBag.Telefones = ClinicaMVC3.Controllers.TelefoneController.SelectListTelefones(db);
        }

        public static List<SelectListItem> getPacientes()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using (var db = new ClinicaEntities())
            {
                foreach (var item in db.Paciente.ToList())
                {
                    SelectListItem li = new SelectListItem();
                    li.Value = item.PacienteId.ToString();
                    li.Text = item.Nome;
                    list.Add(li);
                }
            }

            return list;

        }

        public static Paciente getPaciente(int Id)
        {
            using (var db = new ClinicaEntities())
            {
                return db.Paciente.Find(Id);
            }
        }

    }
}
