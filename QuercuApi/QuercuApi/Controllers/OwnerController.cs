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
    public class OwnerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _connection;

        public OwnerController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        [AllowAnonymous] 
        [Route("ConsultarOwners")]
        public IActionResult ConsultarOwners()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<OwnerEnt>("ConsultarOwners",
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
        [Route("RegistrarOwner")]
        public IActionResult RegistrarOwner(OwnerEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<long>("RegistrarOwner",
                        new {  entidad.Name, entidad.Telephone, entidad.Email, entidad.IdentificationNumber, entidad.Address },
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
        [Route("ConsultarOwnerPorId/{id}")]
        public IActionResult ConsultarOwnerPorId(long id)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<OwnerEnt>("ConsultarOwnerPorId",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure).SingleOrDefault();

                    if (datos != null)
                    {
                        return Ok(datos);
                    }
                    else
                    {
                        return NotFound($"No se encontró el Owner con ID: {id}");
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
        public IActionResult ActualizarOwner(OwnerEnt entidad)
        {
            try
            {

                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarOwner",
                        new {  entidad.Name, entidad.Telephone, entidad.Email, entidad.IdentificationNumber, entidad.Address, entidad.Id },
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
        [Route("ActualizarEstadoOwner")]
        public IActionResult ActualizarEstadoOwner(OwnerEnt entidad)
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Execute("ActualizarEstadoOwner",
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
        [Route("ConsultarTipoOwner")]
        public IActionResult ConsultarTipoOwner()
        {
            try
            {
                using (var context = new SqlConnection(_connection))
                {
                    var datos = context.Query<SelectListItem>("ConsultarTipoOwner",
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




