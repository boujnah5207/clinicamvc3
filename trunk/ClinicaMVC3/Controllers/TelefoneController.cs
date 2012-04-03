using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaMVC3.Models;


//Telefone
namespace ClinicaMVC3.Controllers
{
    public class TelefoneController : Controller
    {

        String tituloCadastro = "";

        public enum tipoTelefone
        {
            Celular = 1,
            Telefone = 2
        }

        public static List<SelectListItem> SelectListTelefones(ClinicaEntities db)
        {

            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var telefone in db.Telefone.ToList())
            {
                selectList.Add(new SelectListItem()
                {
                    Value = telefone.TelefoneId.ToString(),
                    Text = telefone.Numero,
                    Selected = false
                });
            }
            return selectList;

        }


        //
        // GET: /Telefone/

        public ActionResult Index()
        {
		    ViewBag.Title = tituloCadastro;
            using (var db = new ClinicaEntities())
            {                
                return View(db.Telefone.ToList());    
            }
        }

    }
}
