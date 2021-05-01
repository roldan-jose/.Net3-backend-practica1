using EvalucacionTecnica.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_CURSO.CodeHelp;

namespace EvalucacionTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        public readonly AgendaContext agendactx;

        public InfoController(AgendaContext _agendactx)
        {
            agendactx = _agendactx;
        }

        [HttpGet]
        public async Task<IEnumerable<InfoModel>> GetAll()
        {
            return await agendactx.DboInfos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var infoM = await agendactx.DboInfos.FindAsync(id);
            if(infoM == null)
            {
                return NotFound(ErrorsHelp.RespuestaHttp(404, $"La información con ID {id} no se encuentra registrada."));
            }
            else
            {
                return Ok(infoM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InfoModel info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorsHelp.ModelStateErrors(ModelState));
            }
            if (await agendactx.DboInfos.Where(x => x.IdPersona == info.IdPersona).AnyAsync())
            {
                return BadRequest(ErrorsHelp.RespuestaHttp(400, $"El ID: {info.IdPersona} ya existe."));
            }
            if (await agendactx.DboInfos.Where(x => x.Email == info.Email).AnyAsync())
            {
                return BadRequest(ErrorsHelp.RespuestaHttp(400, $"El correo electrónico: {info.Email}  ya existe."));
            }

            await agendactx.DboInfos.AddAsync(info);
            await agendactx.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = info.IdPersona }, info);     
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] InfoModel info)
        {
            if (info.IdPersona == 0)
            {
                info.IdPersona = id;
            }

            if (info.IdPersona != id)
            {
                return BadRequest(ErrorsHelp.RespuestaHttp(400, "Error al enviar la petición"));
            }
            if (!await agendactx.DboInfos.Where(x => x.IdPersona == info.IdPersona).AsNoTracking().AnyAsync())
            {
                return NotFound(ErrorsHelp.RespuestaHttp(404, $"El curso {info.IdPersona} no existe"));
            }
            if (await agendactx.DboInfos.Where(x => x.Email == info.Email && x.IdPersona != info.IdPersona).AsNoTracking().AnyAsync())
            {
                return BadRequest(ErrorsHelp.RespuestaHttp(400, $"El código {info.Email} ya existe"));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorsHelp.ModelStateErrors(ModelState));
            }

            agendactx.Entry(info).State = EntityState.Modified;
            await agendactx.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromQuery] string email)
        {

            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(ErrorsHelp.RespuestaHttp(400, "El campo está vacio."));
            }


            var infom = await agendactx.DboInfos.FindAsync(id);
            if (infom == null)
            {
                return NotFound();
            }
            if (await agendactx.DboInfos.Where(x => x.Email == email && x.IdPersona != id).AnyAsync())
            {
                return BadRequest(ErrorsHelp.RespuestaHttp(400, $"El correo electrónico {email}  ya existe."));
            }

            infom.Email = email;
            if (!TryValidateModel(infom, nameof(infom)))
            {
                return BadRequest(ErrorsHelp.ModelStateErrors(ModelState));
            }
            await agendactx.SaveChangesAsync();
            return StatusCode(200, infom);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var info = await agendactx.DboInfos.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }

            agendactx.DboInfos.Remove(info);
            await agendactx.SaveChangesAsync();
            return NoContent();
        }
    }
}
