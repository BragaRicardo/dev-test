using DevTest_API.Configuration;
using DevTest_API.Data;
using DevTest_API.Entities;
using DevTest_API.Entities.Enums;
using DevTest_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DevTest_API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync(string nombre = null, Estado? estado = null)
        {
            var query = _context.Usuarios.AsQueryable();
            if (!string.IsNullOrEmpty(nombre)) query = query.Where(u => u.NombreCompleto.Contains(nombre));
            if (estado.HasValue) query = query.Where(u => u.Estado == estado.Value);
            return await query.ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            usuario.FechaRegistro = DateTime.UtcNow;
            usuario.PasswordHash = Base64Utils.Encode(usuario.PasswordHash);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> GetByCorreoAsync(string correo)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(u => u.Correo == correo);
        }
    }
}
