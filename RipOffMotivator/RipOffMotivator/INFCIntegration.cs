using System;

namespace RipOffMotivator.Droid.NFCModule
{
    public interface INFCIntegration
    {
        void CreateNFCTag(string message, Guid id);
    }
}