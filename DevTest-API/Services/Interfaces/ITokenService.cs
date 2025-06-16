using DevTest_API.Entities;

namespace DevTest_API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerarToken(Usuario usuario);
    }
}
