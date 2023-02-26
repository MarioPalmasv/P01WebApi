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

        // localhost:SQLEXPRESS/api/categoria/getbyid/id=23&nombre=hola    (esto no es muy viable porque se ven los parámetros)
        // localhost:SQLEXPRESS/api/categoria/getbyid/id/23 (esta es la gg)
        [HttpGet]
        [Route("getbyId/(id)")]

        public IActionResult Get(int id)
        {
            var categoria = (from e in _tiendaContext.categoria
                             where e.id_categoria == id
                             select e).FirstOrDefault();

            if (categoria == null) { return NotFound(); }

            return Ok(categoria);
        }

        [HttpGet]
        [Route("find")]

        public IActionResult buscar (string filtro)
        {
            List<categoria> categoriaList = (from e in _tiendaContext.categoria
                                             where e.nombreCategoria.Contains(filtro) || e.descripcion.Contains(filtro)
                                             select e).ToList();

            if (categoriaList.Any()) { return Ok(categoriaList); }

            return NotFound();
        }

        [HttpPost] //Create o insert
        [Route("add")]

        public IActionResult crear([FromBody] categoria Categorianueva) //le ponemos Frombody para que lo busque en el código
        {
            try
            {
                _tiendaContext.categoria.Add(Categorianueva);
                _tiendaContext.SaveChanges();

                return Ok(Categorianueva); // tiene sentido regresar este objeto para corroborar el insert que se quedo en la d◘

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("update/(id)")]

        public IActionResult actualizar(int id, [FromBody] categoria categoriaActualizar)
        {
            try
            {
                categoria? categoriaExist = (from e in _tiendaContext.categoria
                                            where e.id_categoria == id
                                            select e).FirstOrDefault();
                
                if (categoriaExist == null) { return NotFound(); }

                categoriaExist.nombreCategoria = categoriaActualizar.nombreCategoria;
                categoriaExist.descripcion = categoriaActualizar.descripcion;

                _tiendaContext.Entry(categoriaExist).State = EntityState.Modified;
                _tiendaContext.SaveChanges();

                return Ok(categoriaActualizar);
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/(id)")]

        public IActionResult borrar(int id)
        {
            try
            {
                categoria? categoriaEliminar = (from e in _tiendaContext.categoria
                                         where e.id_categoria == id
                                         select e).FirstOrDefault();

                if (categoriaEliminar == null) { return NotFound(); }

                ////Esto se hace para eliminar los registros cosa que no se debe hacer

                _tiendaContext.categoria.Attach(categoriaEliminar); //para apuntar cual de todos vamos a eliminar
                _tiendaContext.categoria.Remove(categoriaEliminar);
                _tiendaContext.SaveChanges();

                ////_tiendaContext.Entry(categoriaEliminar).State = EntityState.Deleted;
                ////_tiendaContext.SaveChanges();

                //En lugar de borrar lo que hacemos es que modificamos un campo para saber que eso ya no está on

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
