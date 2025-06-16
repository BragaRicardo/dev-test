using DevTest_API.Entities;
using DevTest_API.Entities.Enums;

namespace DevTest_API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync(string nombre = null, Estado? estado = null);
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task<Usuario> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
        Task<Usuario?> GetByCorreoAsync(string correo);
    }
}