using System;
using System.Linq;
using System.Threading.Tasks;

namespace RipOffMotivator
{
    public interface INFCIntegration
    {
        Task<Guid> CreateNFCTag(string message);
    }
}