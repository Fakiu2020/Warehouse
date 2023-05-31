using BaseProject.Application.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseProject.Application.Interfaces
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName, IList<string> roles);
    }
}
