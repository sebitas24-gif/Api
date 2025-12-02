using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspeciesController : ControllerBase
    {
        private readonly ApiContext _context;

        public EspeciesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Especies
        [HttpGet]
        public async Task<ActionResult<ApirResult<List<Especie>>>> GetEspecie()
        {
            try
            {
                var data = await _context.Especies.ToListAsync();
                return ApirResult<List<Especie>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApirResult<List<Especie>>.Fail(ex.Message);
            }
        }

        // GET: api/Especies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApirResult<Especie>>> GetEspecie(int id)
        {
            try
            {
                var especie = await _context
                    .Especies
                    .Include(e => e.Animales)
                    .FirstOrDefaultAsync(e => e.Codigo == id);

                if (especie == null)
                {
                    return ApirResult<Especie>.Fail("Datos no encontrados");
                }

                return ApirResult<Especie>.Ok(especie);
            }
            catch (Exception ex)
            {
                return ApirResult<Especie>.Fail(ex.Message);
            }
        }

        // PUT: api/Especies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApirResult<Especie>>> PutEspecie(int id, Especie especie)
        {
            if (id != especie.Codigo)
            {
                return ApirResult<Especie>.Fail("Identificador no coincide");
            }

            _context.Entry(especie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await EspecieExists(id))
                {
                    return ApirResult<Especie>.Fail("Datos no encontrados");
                }
                else
                {
                    return ApirResult<Especie>.Fail(ex.Message);
                }
            }

            return ApirResult<Especie>.Ok(null);
        }

        // POST: api/Especies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApirResult<Especie>>> PostEspecie(Especie especie)
        {
            try
            {
                _context.Especies.Add(especie);
                await _context.SaveChangesAsync();

                return ApirResult<Especie>.Ok(especie);
            }
            catch (Exception ex)
            {
                return ApirResult<Especie>.Fail(ex.Message);
            }
        }

        // DELETE: api/Especies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApirResult<Especie>>> DeleteEspecie(int id)
        {
            try
            {
                var especie = await _context.Especies.FindAsync(id);
                if (especie == null)
                {
                    return ApirResult<Especie>.Fail("Datos no encontrados");
                }

                _context.Especies.Remove(especie);
                await _context.SaveChangesAsync();

                return ApirResult<Especie>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApirResult<Especie>.Fail(ex.Message);
            }
        }

        private async Task<bool> EspecieExists(int id)
        {
            return await _context.Especies.AnyAsync(e => e.Codigo == id);
        }
    }
}
