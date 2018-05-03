using System;

namespace RipOffMotivator
{
    public interface INFCIntegration
    {
        void CreateNFCTag(string message, Action<string, string> action);
    }
}