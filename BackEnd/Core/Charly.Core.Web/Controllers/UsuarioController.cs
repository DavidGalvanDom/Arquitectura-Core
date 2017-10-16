//Module: ABC Catalogo de usuarios
//Date: 27 / September / 2017
//Owner: David Galvan

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;

using Charly.Core.Web.Models;
using Charly.Core.Web.Entity;
using Charly.Core.Web.Data;


namespace Charly.Core.Web.Controllers
{
    [Route("api/Usuario")]
    public class UsuarioController : Controller
    {
        private readonly Contextdb _db;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UsuarioController(Contextdb db, IMapper mapper, ILogger<UsuarioController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/Usuario
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listResult = await (this._db.Usuario).ToListAsync();

            if (!listResult.Any())
            {
                //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging?tabs=aspnetcore2x
                //_logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById() NOT FOUND");
                return NotFound();
            }

            var lstUsuarios = _mapper.Map<List<Usuario>, List<UsuarioDTO>>(listResult);
            
            return Ok(lstUsuarios);
        }

        // GET api/Usuario/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest(id);
            }

            var result = await this._db.Usuario.SingleAsync(usu => usu.UsuarioId == id);

            //Se utiliza el mapeo para regresar el Data Object
            var usuario = _mapper.Map<Usuario, UsuarioDTO>(result);

            if (result != null)
                return Ok(usuario);
            else
                return NotFound(id);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Models.UsuarioDTO usuarioDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _mapper.Map<Models.UsuarioDTO, Entity.Usuario>(usuarioDTO);
                    
                    _db.Usuario.Add(usuario);

                    await _db.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new NoContentResult();
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody]Models.UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _db.Usuario.Single(item => item.UsuarioId == usuarioDTO.UsuarioId);

                if (result != null)
                {
                    try
                    {
                        result.Nombre = usuarioDTO.Nombre;
                        result.Telefono = usuarioDTO.Telefono;
                        
                       // result.UpdatedBy = HttpContext.User.Claims.ToList()[0].Value;
                       
                        await _db.SaveChangesAsync();

                        return Ok();

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        return new NoContentResult();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest(id);
            }

            var result = await this._db.Usuario.SingleAsync(item => item.UsuarioId == id);

            if (result != null)
            {
                _db.Entry(result).State = EntityState.Deleted;
                _db.SaveChanges();
                return Ok();
            }
            else
                return NotFound(id);
        }
    
    }
}
