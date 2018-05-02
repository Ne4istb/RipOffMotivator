using System;
using System.Linq;

namespace RipOffMotivator
{
    public interface INFCIntegration
    {
        void CreateNFCTag(string message, Guid id);
    }
}