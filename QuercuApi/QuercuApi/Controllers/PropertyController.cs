using QuercuApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuercuApi.Entities;

namespace QuercuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _connection;

        public PropertyController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");

        }

        [HttpGet]
        [AllowAnonymous] 
        [Route("ConsultarProperty")]
        public IActionResult ConsultarProperty()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<PropertyEnt>("ConsultarProperty",
                        new { },
                        commandType: CommandType.StoredProcedure).ToList();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RegistrarProperty")]
        public IActionResult RegistrarProperty(PropertyEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<long>("RegistrarProperty",
                        new { entidad.PropertyTypeId, entidad.OwnerId, entidad.Number, entidad.Address, entidad.Area, entidad.ConstructionArea },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ConsultarPropertyPorId/{id}")]
        public IActionResult ConsultarPropertyPorId(long id)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<PropertyEnt>("ConsultarPropertyPorId",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure).SingleOrDefault();

                    if (datos != null)
                    {
                        return Ok(datos);
                    }
                    else
                    {
                        return NotFound($"No se encontró el Property con ID: {id}");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("ActualizarProperty")]
        public IActionResult ActualizarProperty(PropertyEnt entidad)
        {
            try
            {

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarProperty",
                        new { entidad.PropertyTypeId, entidad.OwnerId, entidad.Number, entidad.Address, entidad.Area, entidad.ConstructionArea, entidad.Id },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        [HttpPut]
        [AllowAnonymous]
        [Route("ActualizarEstadoProperty")]
        public IActionResult ActualizarEstadoProperty(PropertyEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarEstadoProperty",
                        new { entidad.Id },
                        commandType: CommandType.StoredProcedure);

                    return Ok(datos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("BuscarProductos/{nombreProducto}")]
        //public IActionResult BuscarProductos(string nombreProducto)
        //{
        //    try
        //    {
        //        using (var context = new SqlConnection(_connection))
        //        {
        //            var datos = context.Query<ProductosEnt>("BuscarProductos",
        //                new { nombreProducto },
        //                commandType: CommandType.StoredProcedure).ToList();

        //            return Ok(datos);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


    }
}




