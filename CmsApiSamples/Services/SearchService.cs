using System.Collections.Generic;
using System.Threading.Tasks;
using CmsApiSamples.Protocol.Extensions;
using CmsApiSamples.Protocol.CMSApi;

namespace CmsApiSamples.Services
{
    /// <summary>
    /// Samples for SearchRequest protocol messages.
    /// </summary>
    public class SearchService
    {
        private readonly CmsApiProxy _api;

        public SearchService(CmsApiProxy api)
        {
            _api = api;
        }

        /// <summary>
        /// Searches users.
        /// </summary>
        public async Task<ServiceResult> UserSearchRequest(uint requestId, IEnumerable<SearchOption> searchOptions, uint? top, bool? allMatchMode, bool? includeRemoved)
        {
            UserSearchRequest userSearchRequest = Protocol.CMSApi.UserSearchRequest.CreateBuilder()
                .AddRangeSearchOptions(searchOptions)
                .SetIfNotNull(allMatchMode, b => b.SetAllMatchMode)
                .Build();

            SearchRequest searchRequest = SearchRequest.CreateBuilder()
                .SetId(requestId)
                .SetUserSearchRequest(userSearchRequest)
                .SetIfNotNull(top, b => b.SetTop)
                .SetIfNotNull(includeRemoved, b => b.SetIncludeRemoved)
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddSearchRequest(searchRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Searches customers.
        /// </summary>
        public async Task<ServiceResult> CustomerSearchRequest(uint requestId, List<SearchOption> searchOptions, uint? top, bool? allMatchMode, bool? includeRemoved)
        {
            CustomerSearchRequest customerSearchRequest = Protocol.CMSApi.CustomerSearchRequest.CreateBuilder()
                .AddRangeSearchOptions(searchOptions)
                .SetIfNotNull(allMatchMode, b => b.SetAllMatchMode)
                .Build();

            SearchRequest searchRequest = SearchRequest.CreateBuilder()
                .SetId(requestId)
                .SetCustomerSearchRequest(customerSearchRequest)
                .SetIfNotNull(top, b => b.SetTop)
                .SetIfNotNull(includeRemoved, b => b.SetIncludeRemoved)
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddSearchRequest(searchRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Searches sales series.
        /// </summary>
        public async Task<ServiceResult> SalesSeriesSearchRequest(uint requestId, List<SearchOption> searchOptions, uint? top, bool? allMatchMode, bool? includeRemoved)
        {
            SalesSeriesSearchRequest salesSeriesSearchRequest = Protocol.CMSApi.SalesSeriesSearchRequest.CreateBuilder()
                .AddRangeSearchOptions(searchOptions)
                .SetIfNotNull(allMatchMode, b => b.SetAllMatchMode)
                .Build();

            SearchRequest searchRequest = SearchRequest.CreateBuilder()
                .SetId(requestId)
                .SetSalesSeriesSearchRequest(salesSeriesSearchRequest)
                .SetIfNotNull(top, b => b.SetTop)
                .SetIfNotNull(includeRemoved, b => b.SetIncludeRemoved)
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddSearchRequest(searchRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }
    }
}
