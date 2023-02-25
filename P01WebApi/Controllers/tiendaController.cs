using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P01WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace P01WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tiendaController : ControllerBase
    {
        private readonly tiendaContext _tiendaContext;

        public tiendaController(tiendaContext tiendaContext)
        {
            _tiendaContext = tiendaContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<categoria> listadoCategorias = (from e in _tiendaContext.categoria
                                                 select e).ToList();
            if (listadoCategorias.Count == 0) { return NotFound(); }

            return Ok(listadoCategorias);
        }

    }
}
