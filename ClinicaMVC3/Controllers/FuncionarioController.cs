using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaMVC3.Models;
using System.Data;
using System.Web.Security;
using ClinicaMVC3.Controllers;
//Funcionario
namespace ClinicaMVC3.Controllers
{

    [HandleError]
    public class FuncionarioController : Controller
    {
		String tituloCadastro = "Funcionários";
		
        //
        // GET: /Funcionario/

        public ActionResult Index()
        {
		    ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {                
                return View(db.Funcionario.ToList());    
            }
        }

        //
        // GET: /Funcionario/Details/5

        public ActionResult Details(int id)
        {

            ViewBag.Title = tituloCadastro;
            ViewBag.Method = "Detail";
            ClinicaEntities db = new ClinicaEntities();
            return View("Create", db.Funcionario.Find(id));

        }

        //
        // GET: /Funcionario/Create

        public ActionResult Create()
        {
            ViewBag.Title = tituloCadastro;
            ViewBag.Method = "Insert";

            using (var db = new ClinicaEntities())
            {
                return View();
            }
        } 

        //
        // POST: /Funcionario/Create

        [HttpPost]
        public JsonResult Create(Funcionario  funcionario, String UserName, String Password, String email)
        {

            try
            {


                ModelState.Remove("UserId");
                if (ModelState.IsValid)
                {
                    using (var db = new ClinicaEntities())
                    {

                        //Criar usuario e vincular o UserId
                        //MembershipUser myObject = Membership.GetUser();
                        //string UserID = myObject.ProviderUserKey.ToString();

                        // Attempt to register the user
                        MembershipCreateStatus createStatus;
                        Membership.CreateUser(UserName, Password, email, null, null, true, null, out createStatus);



                        if (createStatus == MembershipCreateStatus.Success)
                        {
                            FormsAuthentication.SetAuthCookie(UserName, false /* createPersistentCookie */);

                        }
                        else
                        {

                            ModelState.AddModelError("", AccountController.ErrorCodeToString(createStatus));
                        }



                        // Se o código do paciente é maior que zero, entendemos que existe registro para tal.
                        // Então nós "atualizaremos" ele.                    
                        if (funcionario.FuncionarioId > 0)
                        {

                            var selFuncionarioTelefone = db.FuncionarioTelefone.Where(p => p.FuncionarioId == funcionario.FuncionarioId);

                            foreach (FuncionarioTelefone pt in selFuncionarioTelefone)
                            {
                                db.FuncionarioTelefone.Remove(pt);
                            }

                            foreach (FuncionarioTelefone pt in funcionario.FuncionarioTelefone)
                            {
                                db.FuncionarioTelefone.Add(pt);
                            }

                            db.Entry(funcionario).State = EntityState.Modified;

                        }
                        //Perform Save
                        else
                        {
                            db.Funcionario.Add(funcionario);
                        }

                        db.SaveChanges();

                    }


                    // If Sucess== 1 then Save/Update Successfull else there it has Exception
                    return Json(new { Success = 1, SalesID = funcionario.FuncionarioId, ex = "" });
                }
                else
                {
                    ValidateModel(funcionario);
                }
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.Message.ToString() + '\n' +  ex.InnerException.ToString() });
            }

            return Json(new { Success = 0, ex = new Exception("Unable to save").Message.ToString() });
        }
        
        //
        // GET: /Funcionario/Edit/5
 
        public ActionResult Edit(int id)
        {
            ViewBag.Title = tituloCadastro;
            ViewBag.Method = "Edit";
            ClinicaEntities db = new ClinicaEntities();
            return View("Create", db.Paciente.Find(id));
        }

        //
        // POST: /Funcionario/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Funcionario funcionario)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                ClinicaEntities db = new ClinicaEntities();

                db.Entry(funcionario).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
                return View("Error");
            }
        }

        //
        // GET: /Funcionario/Delete/5
 
        public ActionResult Delete(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.Funcionario.Find(id));
            }
        }

        //
        // POST: /Funcionario/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Funcionario funcionario)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                using (var db = new ClinicaEntities())
                {
                    db.Entry(funcionario).State = System.Data.EntityState.Deleted;
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
