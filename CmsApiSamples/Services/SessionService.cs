using System.Threading.Tasks;
using CmsApiSamples.Protocol.CMSApi;

namespace CmsApiSamples.Services
{
    /// <summary>
    /// Class to demonstrate usage of Session related CMS API messages.
    /// </summary>
    public sealed class SessionService
    {
        private readonly CmsApiProxy _api;

        public SessionService(CmsApiProxy api)
        {
            _api = api;
        }

        /// <summary>
        /// Performs logon.
        /// </summary>
        public async Task<ServiceResult> Logon(string userName, string password)
        {
            Logon logon = new Logon.Builder()
                .SetPassword(password)
                .SetUserName(userName)
                .SetClientAppId("CmsApiTest") //For samples only
                .SetClientVersion("CmsApiDemo") //For samples only
                .SetProtocolVersionMajor((uint)ProtocolVersion.PROTOCOL_VERSION_MAJOR)
                .SetProtocolVersionMinor((uint)ProtocolVersion.PROTOCOL_VERSION_MINOR)
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .SetLogon(logon)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Performs logoff.
        /// </summary>
        public async Task<ServiceResult> Logoff()
        {
            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .SetLogoff(new Logoff.Builder().Build())
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

    }
}
