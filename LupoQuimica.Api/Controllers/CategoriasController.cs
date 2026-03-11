using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LupoQuimica.Api.Data;
using LupoQuimica.Shared;
using Microsoft.AspNetCore.Authorization;

namespace LupoQuimica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.OrderBy(c => c.Nome).ToListAsync();
        }

        // POST: api/categorias
        [HttpPost]
        [Authorize] // Protegido: precisa de login
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);
        }

        // DELETE: api/categorias/5
        [HttpDelete("{id}")]
        [Authorize] // Protegido: precisa de login
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            // Opcional: Verificar se existem produtos usando esta categoria antes de deletar
            var temProdutos = await _context.Produtos.AnyAsync(p => p.CategoriaId == id);
            if (temProdutos)
            {
                return BadRequest("Não é possível excluir uma categoria que possui produtos vinculados.");
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}