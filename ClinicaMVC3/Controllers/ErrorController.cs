using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaMVC3.Models;



//Error
namespace ClinicaMVC3.Controllers
{
    public class ErrorController : Controller
    {
	
		
        //
        // GET: /Error/

        public ActionResult Error()
        {
            #region Tratamento do erro
            String erroTratado = "Ocorreu um erro, tente novamente.";
            if (TempData["Error"] != null)
            {
                erroTratado = TempData["Error"].ToString();                
            }


            if (erroTratado.Contains("The DELETE statement conflicted with the REFERENCE"))
            {
                
                erroTratado =  "É impossivel excluir este registro, é provavel que ele já foi utilizado em outro cadastro.";
            }

            #endregion

            ViewBag.Error = erroTratado;


            return View();    
            
        }

    }
}
