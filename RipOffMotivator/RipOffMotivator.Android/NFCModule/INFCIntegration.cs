using System;

namespace RipOffMotivator.Droid.NFCModule
{
    interface INFCIntegration
    {
        void CreateNFCTag(string message, Guid id);
    }
}