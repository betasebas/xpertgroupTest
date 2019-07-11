using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xpertgroupTest.Model;
using xpertgroupTest.Service;

namespace xpertgroupTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //Los metodos post deben validar por ser un prueba no se valida la seguridad que ofrece el framework
        [HttpPost]
        public JsonResult ProcesarOperaciones(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return Json(new RespuestaGeneral { EstadoOperacion = false, Mensaje = "Los datos estan vacios" });
                var operacion = new CubeService();
                var respuesta = operacion.Ejecutar(value);
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                // Al ser u excepción del sistema debe grabara el error en algun log o visor de eventos de windows.
                // El usuario nunca debe recibir errores del sistema.
                return Json(new RespuestaGeneral { EstadoOperacion = false, Mensaje = "Se ha presentado un error" });
            }
        }
    }
}