using CmsApiSamples.Protocol.CMSApi;

namespace CmsApiSamples.Services
{
    /// <summary>
    /// Class representing pair of related Incoming/Outcoming messages, used in UI model.
    /// </summary>
    public sealed class ServiceResult
    {
        /// <summary>
        /// Initialize instance of ServiceResult class.
        /// </summary>
        public ServiceResult(ClientMessage clientMessage, ServerMessage serverMessage)
        {
            ClientMessage = clientMessage;
            ServerMessage = serverMessage;
        }

        /// <summary>
        /// Client message sent to server.
        /// </summary>
        public ClientMessage ClientMessage { get; private set; }

        /// <summary>
        /// Server message returned back.
        /// </summary>
        public ServerMessage ServerMessage { get; private set; }
    }
}
