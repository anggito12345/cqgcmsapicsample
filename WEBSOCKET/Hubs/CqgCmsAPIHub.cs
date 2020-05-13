using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using CmsApiSamples;
using CmsApiSamples.Protocol;
using CmsApiSamples.Protocol.CMSApi;
using CmsApiSamples.Services;
using System;

namespace WEBSOCKET.Hubs
{
    public class CqgCmsAPIHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {                        
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        [HubMethodName("Logon")]
        public async Task Logon(string user, string password, string connID)
        {
            try
            {
                var appSettings = CmsApiDemo.Program.getConfiguration();
                string CmsApiUrl = appSettings["CmsApiUrl"];
                string userName = appSettings["UserName"];
                string userPassword = appSettings["UserPassword"];

                // Creates new instance of service.
                CmsApiService service = new CmsApiService(CmsApiUrl);

                var Result = await service.SessionService.Logon(userName, userPassword);
                
                
                await Clients.User(connID).SendAsync("ReceiveMessage", user, Result.ServerMessage.ToString());

            } catch (Exception e) {
                await Clients.User(connID).SendAsync("ReceiveMessage", user, e.Message);
            }
            
        }

    }
}