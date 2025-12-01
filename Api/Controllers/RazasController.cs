using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Modelos;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazasController : ControllerBase
    {
        private readonly ApiContext _context;

        public RazasController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Razas
        [HttpGet]
        public async Task<ActionResult<ApirResult<List<Raza>>>> GetRaza()
        {
            try

            {
                var data = await _context.Razas.ToListAsync();
                return ApirResult<List<Raza>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApirResult<List<Raza>>.Fail(ex.Message);
            }
        }

        // GET: api/Razas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApirResult<Raza>>> GetRaza(int id)
        {
            try
            {
                var raza = await _context.Razas
                    .Include(r => r.Animales)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (raza == null)
                {
                    return ApirResult<Raza>.Fail("Datos no encontrados");
                }

                return ApirResult<Raza>.Ok(raza);
            }
            catch (Exception ex)
            {
                return ApirResult<Raza>.Fail(ex.Message);
            }
        }

        // PUT: api/Razas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApirResult<Raza>>> PutRaza(int id, Raza raza)
        {
            if (id != raza.Id)
            {
                return ApirResult<Raza>.Fail("ID no coincide");
            }

            _context.Entry(raza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await RazaExists(id))
                {
                    return ApirResult<Raza>.Fail("Datos no encontrados ");
                }
                else
                {
                    return ApirResult<Raza>.Fail(ex.Message);
                }
            }

            return ApirResult<Raza>.Ok(raza);
        }
        // POST: api/Razas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApirResult<Raza>>> PostRaza(Raza raza)
        {
            try
            {
                _context.Razas.Add(raza);
                await _context.SaveChangesAsync();

                return ApirResult<Raza>.Ok(raza);
            }
            catch (Exception ex)
            {
                return ApirResult<Raza>.Fail(ex.Message);

            }
        }

        // DELETE: api/Razas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApirResult<Raza>>> DeleteRaza(int id)
        {
            try
            {
                var raza = await _context.Razas.FindAsync(id);
                if (raza == null)
                {
                    return ApirResult<Raza>.Fail("Datos no encontrados");
                }

                _context.Razas.Remove(raza);
                await _context.SaveChangesAsync();

                return ApirResult<Raza>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApirResult<Raza>.Fail(ex.Message);
            }
        }

        private async Task<bool> RazaExists(int id)
        {
            return await _context.Razas.AnyAsync(e => e.Id == id);
        }
    }
}
