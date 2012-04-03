using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaMVC3.Models;


//Consulta
namespace ClinicaMVC3.Controllers
{
    [HandleError, Authorize(Roles = "Secretaria,Medico")]
    public class ConsultaController : Controller
    {

        /*Fluxo de Eventos: 
         * Agendar consulta = Create
         * Desmarcar consulta = Delete
         * Registrar não comparecimento = Edit
         **/

        String tituloCadastro = "Consultas";

        //
        // GET: /Consulta/

        public ActionResult Index()
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                int auxId = 0;
                var listConsultas = db.Consulta.ToList();
                for (int i = 0; i < listConsultas.Count; i++)
                {
                    auxId = listConsultas.ElementAt(i).PacienteId;
                    listConsultas.ElementAt(i).Paciente = PacienteController.getPaciente(auxId);

                    auxId = listConsultas.ElementAt(i).MedicoId;
                    listConsultas.ElementAt(i).Funcionario = FuncionarioController.getFuncionario(auxId);


                }

                return View(listConsultas);
            }
        }

        //
        // GET: /Consulta/Details/5

        public ActionResult Details(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.Consulta.Find(id));
            }
        }

        //
        // GET: /Consulta/Create
        [Authorize(Roles = "Secretaria")]
        public ActionResult Create()
        {
            ViewBag.Title = tituloCadastro;
            return View();
        }

        //
        // POST: /Consulta/Create

        [HttpPost, Authorize(Roles = "Secretaria")]
        public ActionResult Create(Consulta consulta)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                using (var db = new ClinicaEntities())
                {

                    var Consultas = db.Consulta.Where(i => i.Data == consulta.Data)
                                               .Where(i => i.MedicoId == consulta.MedicoId)
                                               .Where(i => i.Status == (int)Status.Agendada)
                                               .OrderBy(i => i.Hora);
                    foreach (var item in Consultas.ToList())
                    {

                        TimeSpan hora30MinDepois = item.Hora.Add(new TimeSpan(0, 30, 0));


                        /* Se for menor que a (hora+30min) e maior que a hora */
                        if ((consulta.Hora.CompareTo(hora30MinDepois)<=0)&&
                            (consulta.Hora.CompareTo(item.Hora)>=0)) 
                        {
                            ModelState.AddModelError("Hora", "Médico com consulta agendada as " + item.Hora.ToString() + " horas, é necessario dar intervalo de 30 minutos.");
                            return View(consulta);
                        }
                    }
                    consulta.Status = (int)Status.Agendada;
                    db.Consulta.Add(consulta);
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
        // GET: /Consulta/RealizarConsulta/5
        [Authorize(Roles = "Medico")]
        public ActionResult RealizarConsulta(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.Consulta.Find(id));
            }
        }

        //
        // POST: /Consulta/RealizarConsulta/5

        [HttpPost, Authorize(Roles = "Medico")]
        public ActionResult RealizarConsulta(int id, Consulta consulta)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                using (var db = new ClinicaEntities())
                {
                    consulta.Status = (int)Status.Realizada;
                    db.Entry(consulta).State = System.Data.EntityState.Modified;
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

        [Authorize(Roles = "Secretaria")]
        public ActionResult DesmarcarConsulta(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.Consulta.Find(id));
            }
        }

        //
        // POST: /Consulta/DesmarcarConsulta/5

        [HttpPost, Authorize(Roles = "Secretaria")]
        public ActionResult DesmarcarConsulta(int id, Consulta consulta)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                using (var db = new ClinicaEntities())
                {

                    /* Se for desmarcada antes da Data prevista a Consulta será excluída. */
                    if (consulta.Data >= DateTime.Now)
                    {
                        return View("Delete", db.Consulta.Find(consulta.ConsultaId));

                    }
                    /* Caso contrario será marcada como não comparecimento */
                    else
                    {
                        consulta.Status = (int)Status.Nao_Comparecido;
                        db.Entry(consulta).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                    }

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
        // GET: /Consulta/Delete/5
        [Authorize(Roles = "Secretaria")]
        public ActionResult Delete(int id)
        {
            ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {
                return View(db.Consulta.Find(id));
            }
        }

        //
        // POST: /Consulta/Delete/5

        [HttpPost, Authorize(Roles = "Secretaria")]
        public ActionResult Delete(int id, Consulta consulta)
        {
            ViewBag.Title = tituloCadastro;
            try
            {
                using (var db = new ClinicaEntities())
                {
                    db.Entry(consulta).State = System.Data.EntityState.Deleted;
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

        //Aux
        public enum Status
        {
            Agendada = 1,
            Realizada = 2,
            Nao_Comparecido = 3
        }

        public enum TipoAcao
        {
            Desmarcar = 1,
            Realizar = 2
        }

        public static String getStatusText(int statusCod)
        {
            Array statusArr = Enum.GetValues(typeof(ConsultaController.Status));
            String statusDesc = "";
            if (statusCod-1 <= statusArr.Length-1)
            {
                if (statusCod > 0)
                {
                    statusDesc = statusArr.GetValue(statusCod - 1).ToString().Replace('_', ' ').Replace("Nao", "Não");
                }
            }
            return statusDesc;
        }

    }
}
