using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaMVC3.Models;


//ThemeChoice
namespace ClinicaMVC3.Controllers
{
    public class ThemeChoiceController : Controller
    {


        String tituloCadastro = "";

        //
        // GET: /ThemeChoice/

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Consulta/Create
        [HttpPost]
        public ActionResult Create(String themeselected)
        {
            String Controller = (TempData["CurrentController"] == null) ? "Home" : TempData["CurrentController"].ToString();
            String Action = (TempData["CurrentAction"] == null) ? "Index" : TempData["CurrentAction"].ToString();
            int id = Int16.Parse(TempData["CurrentId"].ToString());
            Session["ThemeSelected"] = themeselected;
            if (id > 0)
            {
                return RedirectToAction(Action, Controller, new { id });
            }
            else
            {
                
                return RedirectToAction(Action, Controller);
            }
        }

    }
}
