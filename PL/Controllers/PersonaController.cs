using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            ML.Persona persona = new ML.Persona();
            result = BL.Persona.GetAll();

            if (result.Correct)
            {
                persona.Personas = result.Objects;
                return View(persona);
            }
            return View(persona);
        }
        [HttpGet]
        public ActionResult Form(int? IdPersona)
        {
            ML.Result result = new ML.Result();
            ML.Persona persona = new ML.Persona();

            if (IdPersona != null)
            {
                result = BL.Persona.GetById(IdPersona.Value);
                if (result.Correct)
                {
                    persona = (ML.Persona)result.Object;
                    return View(persona);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                }
                return PartialView("Modal");
            }
            else
            {
                return View(persona);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Persona persona)
        {
            ML.Result result = new ML.Result();

            if (persona.IdPersona == 0)
            {
                result = BL.Persona.Add(persona);
                if (result.Correct)
                {
                    ViewBag.Message = "Se inserto el registro ";
                }
                else
                {
                    ViewBag.Message = "Error al insertar el registro";
                }
            }
            else
            {
                result = BL.Persona.Update(persona);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo el registro ";
                }
                else
                {
                    ViewBag.Message = "Error al actualizar el registro";
                }
            }

            return PartialView("Modal");

        }
        [HttpGet]
        public ActionResult Delete(int IdPersona)
        {
            ML.Result result = BL.Persona.Delete(IdPersona);
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino el registro";
            }
            else
            {
                ViewBag.Message = "Error al eliminar el registro";
            }
            return PartialView("Modal");
        }
    }
}
