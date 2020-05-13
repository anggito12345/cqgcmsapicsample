using System.Collections.Generic;
using System.Threading.Tasks;
using CmsApiSamples.Protocol.Extensions;
using CmsApiSamples.Protocol.CMSApi;

namespace CmsApiSamples.Services
{
    /// <summary>
    /// Class to demonstrate usage of Information related CMS API messages.
    /// </summary>
    public sealed class InformationService
    {
        private readonly CmsApiProxy _api;

        public InformationService(CmsApiProxy api)
        {
            _api = api;
        }

        /// <summary>
        /// Requests user information.
        /// </summary>
        public async Task<ServiceResult> UserInfoRequest(uint requestId, string userId)
        {
            UserInfoRequest userInfoRequest = Protocol.CMSApi.UserInfoRequest.CreateBuilder()
                .SetUserId(userId)
                .Build();

            InformationRequest informationRequest = InformationRequest.CreateBuilder()
                .SetId(requestId)
                .SetUserInfoRequest(userInfoRequest)
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddInformationRequest(informationRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests user entitlement services.
        /// </summary>
        public async Task<ServiceResult> UserEntitlementServiceRequest(uint requestId, string userId)
        {
            UserEntitlementServiceRequest userEntitlementServiceRequest = Protocol.CMSApi.UserEntitlementServiceRequest.CreateBuilder()
                .SetUserId(userId)
                .Build();

            InformationRequest informationRequest = InformationRequest.CreateBuilder()
                .SetId(requestId)
                .SetUserEntitlementServiceRequest(userEntitlementServiceRequest)
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddInformationRequest(informationRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests entitlement category list.
        /// </summary>
        public async Task<ServiceResult> EntitlementCategoryListRequest(uint requestId)
        {
            EntitlementCategoryListRequest entitlementCategoryListRequest = Protocol.CMSApi.EntitlementCategoryListRequest.CreateBuilder()
                .Build();

            InformationRequest informationRequest = InformationRequest.CreateBuilder()
                .SetId(requestId)
                .SetEntitlementCategoryListRequest(entitlementCategoryListRequest)
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddInformationRequest(informationRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests entitlement service.
        /// </summary>
        public async Task<ServiceResult> EntitlementServiceRequest(uint requestId, uint serviceId)
        {
            EntitlementServiceRequest entitlementServiceRequest = Protocol.CMSApi.EntitlementServiceRequest.CreateBuilder()
                .SetEntitlementServiceId(serviceId)
                .Build();

            InformationRequest informationRequest = InformationRequest.CreateBuilder()
                .SetId(requestId)
                .SetEntitlementServiceRequest(entitlementServiceRequest)
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddInformationRequest(informationRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests customer information.
        /// </summary>
        public async Task<ServiceResult> CustomerRequest(uint requestId, string customerId)
        {
            CustomerRequest customerRequest = Protocol.CMSApi.CustomerRequest.CreateBuilder()
                .SetId(customerId)                                
                .Build();

            InformationRequest informationRequest = InformationRequest.CreateBuilder()
                .SetId(requestId)
                .SetCustomerRequest(customerRequest)                
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddInformationRequest(informationRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }
    }
}
