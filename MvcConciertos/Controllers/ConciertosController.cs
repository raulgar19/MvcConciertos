using Microsoft.AspNetCore.Mvc;
using MvcConciertos.Models;
using MvcConciertos.Services;

namespace MvcConciertos.Controllers
{
    public class ConciertosController : Controller
    {
        private readonly ConciertosService _service;

        public ConciertosController(ConciertosService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int? idCategoria)
        {
            List<CategoriaEvento> categorias = await _service.GetCategoriasAsync();
            List<Evento> eventos;

            if (idCategoria.HasValue)
            {
                eventos = await _service.GetEventosByCategoriaAsync(idCategoria.Value);
            }
            else
            {
                eventos = await _service.GetEventosAsync();
            }

            ViewData["Categorias"] = categorias;

            return View(eventos);
        }
    }
}
