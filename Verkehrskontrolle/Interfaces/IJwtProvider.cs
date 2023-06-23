using System.Diagnostics.Metrics;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
