using ProyectoAPI.Entities;
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
    public class PropertyTypeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;
        private string _connection;

        public PropertyTypeController(IConfiguration configuration, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
            _utilitarios = utilitarios;
        }

        [HttpGet]
        [AllowAnonymous] 
        [Route("ConsultarPropertyType")]
        public IActionResult ConsultarPropertyType()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<PropertyTypeEnt>("ConsultarPropertyType",
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
        [Route("RegistrarPropertyType")]
        public IActionResult RegistrarPropertyType(PropertyTypeEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<long>("RegistrarPropertyType",
                        new { entidad.Description },
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
        [Route("ConsultarPropertyTypePorId/{id}")]
        public IActionResult ConsultarPropertyTypePorId(long id)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<OwnerEnt>("ConsultarPropertyTypePorId",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure).SingleOrDefault();

                    if (datos != null)
                    {
                        return Ok(datos);
                    }
                    else
                    {
                        return NotFound($"No se encontró el PropertyType con ID: {id}");
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
        [Route("ActualizarOwner")]
        public IActionResult ActualizarPropertyType(PropertyTypeEnt entidad)
        {
            try
            {

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarPropertyType",
                        new { entidad.Description, entidad.Id },
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
        [Route("ActualizarEstadoPropertyType")]
        public IActionResult ActualizarEstadoPropertyType(PropertyTypeEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarEstadoPropertyType",
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


        [HttpGet] //PARA EL DROPDOWN
        [AllowAnonymous]
        [Route("ConsultarTipoPropertyType")]
        public IActionResult ConsultarTipoPropertyType()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<SelectListItem>("ConsultarTipoPropertyType",
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




