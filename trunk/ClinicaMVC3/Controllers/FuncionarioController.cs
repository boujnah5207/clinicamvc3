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

    [Authorize(Roles = "Administrador")]
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
            try
            {
                SelectListEspecialidades(db);
                SelectListTelefones(db);

                Funcionario func = db.Funcionario.Find(id);
                return View("Create", func);
            }
            finally
            {
                db.Dispose();
            }

            

        }

        //
        // GET: /Funcionario/Create

        public ActionResult Create()
        {


            ViewBag.Title = tituloCadastro;
            ViewBag.Method = "Insert";

            using (var db = new ClinicaEntities())
            {

                SelectListEspecialidades(db);
                SelectListTelefones(db);

                return View();
            }
        }

        //
        // POST: /Funcionario/Create

        [HttpPost]
        public JsonResult Create(Funcionario funcionario, String UserName, String Password, String email)
        {

            try
            {
                ModelState.Remove("UserId");
                if (ModelState.IsValid)
                {

                    // Tentativa de registrar o usuário
                    if (UserName != null)
                    {
                        /* Criamos a Roles para a função do usuario( caso ela já não exista) */

                        String strRole = Enum.GetValues(typeof(funcao)).GetValue(funcionario.funcao - 1).ToString();
                        if (!Roles.RoleExists(strRole))
                        {
                            Roles.CreateRole(strRole);
                        }


                        MembershipUser User = Membership.GetUser(UserName);
                        /* Se o usuario não existe, eu crio ele e vinculo a role. */
                        if (User == null)
                        {
                            MembershipCreateStatus createStatus;
                            MembershipUser membershipUserCreated = Membership.CreateUser(UserName, Password, email, null, null, true, null, out createStatus);

                            if (createStatus == MembershipCreateStatus.Success)
                            {
                                if (!Roles.IsUserInRole(UserName, strRole))
                                {
                                    Roles.AddUserToRole(UserName, strRole);
                                }
                                /*FormsAuthentication.SetAuthCookie(UserName, false );*/
                                funcionario.UserId = Guid.Parse(membershipUserCreated.ProviderUserKey.ToString());
                            }
                            else
                            {
                                return Json(new { Success = 0, ex = new Exception(AccountController.ErrorCodeToString(createStatus)).Message.ToString() });
                            }
                        }
                        else
                        {
                            MembershipUser currentUser = Membership.GetUser(UserName);

                            Array funcoes = Enum.GetValues(typeof(funcao));

                            for (int i = 0; i < funcoes.Length; i++)
                            {
                                if (Roles.IsUserInRole(UserName, funcoes.GetValue(i).ToString()))
                                {
                                    Roles.RemoveUserFromRole(UserName, funcoes.GetValue(i).ToString());
                                }
                            }

                            if (!Roles.IsUserInRole(UserName, strRole))
                            {
                                Roles.AddUserToRole(UserName, strRole);
                            }

                        }
                    }

                    using (var db = new ClinicaEntities())
                    {

                        // Se o código do Funcionario é maior que zero, entendemos que existe registro para tal.
                        // Então nós "atualizaremos" ele.                    
                        if (funcionario.FuncionarioId > 0)
                        {
                            #region Tratamento dos Telefones
                            var selFuncionarioTelefone = db.FuncionarioTelefone.Where(p => p.FuncionarioId == funcionario.FuncionarioId);

                            foreach (FuncionarioTelefone pt in selFuncionarioTelefone)
                            {
                                db.FuncionarioTelefone.Remove(pt);
                            }

                            foreach (FuncionarioTelefone pt in funcionario.FuncionarioTelefone)
                            {
                                db.FuncionarioTelefone.Add(pt);
                            }
                            #endregion

                            /* O funcionario somente possuirá funcionalidades quando for "Médico". */

                            var selFuncionarioEspecialidade = db.FuncionarioEspecialidade.Where(p => p.FuncionarioId == funcionario.FuncionarioId);

                            foreach (FuncionarioEspecialidade pt in selFuncionarioEspecialidade)
                            {
                                db.FuncionarioEspecialidade.Remove(pt);
                            }

                            if (funcionario.funcao == 3)
                            {
                                #region Tratamento das Especialidades
                                foreach (FuncionarioEspecialidade pt in funcionario.FuncionarioEspecialidade)
                                {

                                    db.FuncionarioEspecialidade.Add(pt);
                                }

                                #endregion
                            }
                            else
                            {
                                funcionario.FuncionarioEspecialidade = null;
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
                 
                    return Json(new { Success = 1, SalesID = funcionario.FuncionarioId, ex = "" });
                }
                else
                {
                    ValidateModel(funcionario);
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
        // GET: /Funcionario/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.Title = tituloCadastro;
            ViewBag.Method = "Edit";
            ClinicaEntities db = new ClinicaEntities();
            try
            {
                SelectListEspecialidades(db);
                SelectListTelefones(db);
                return View("Create", db.Funcionario.Find(id));

            }
            finally
            {
                db.Dispose();
            }
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
                try
                {
                    db.Entry(funcionario).State = System.Data.EntityState.Modified;
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
                    Funcionario funcionarioDeleted = db.Funcionario.Find(id);
                    db.Funcionario.Remove(funcionarioDeleted);

                    db.SaveChanges();


                    if (funcionarioDeleted.UserId != null)
                    {
                        MembershipUser msuAux = Membership.GetUser(((object)funcionarioDeleted.UserId));
                        if (msuAux != null)
                        {
                            Membership.DeleteUser(msuAux.UserName);
                        }

                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
                return View("Error");
            }
        }


        //Aux

        private void SelectListTelefones(ClinicaEntities db)
        {
            ViewBag.Telefones = ClinicaMVC3.Controllers.TelefoneController.SelectListTelefones(db);
        }

        private void SelectListEspecialidades(ClinicaEntities db)
        {

            var selectList = new List<SelectListItem>();
            foreach (var especialidade in db.Especialidade.ToList())
            {
                selectList.Add(new SelectListItem()
                {
                    Value = especialidade.EspecialidadeId.ToString(),
                    Text = especialidade.Descricao,
                    Selected = false
                });
            }
            ViewBag.Especialidades = selectList;

        }

        public enum funcao
        {
            Administrador = 1,
            Secretaria = 2,
            Medico = 3
        }

        public static List<SelectListItem> getMedicos()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using (var db = new ClinicaEntities())
            {
                var Medicos = db.Funcionario.Where(p => p.funcao == ((int)funcao.Medico) );
                foreach (var item in Medicos.ToList())
                {
                    SelectListItem li = new SelectListItem();
                    li.Value = item.FuncionarioId.ToString();
                    li.Text = item.Nome;
                    list.Add(li);
                }
            }

            return list;

        }

        public static Funcionario getFuncionario(int id)
        {
            using (var db = new ClinicaEntities())
            {
                return db.Funcionario.Find(id);
            }

        }

        public static String getFuncaoText(int funcaoCod)
        {
            Array funcaoArr = Enum.GetValues(typeof(funcao));
            String funcaoDesc = "";
            if (funcaoCod - 1 <= funcaoArr.Length - 1)
            {
                if (funcaoCod > 0)
                {
                    funcaoDesc = funcaoArr.GetValue(funcaoCod - 1).ToString();
                }
            }
            return funcaoDesc;

        }

    }


}
