using System.Collections.Generic;
using System.Threading.Tasks;
using CmsApiSamples.Protocol.Extensions;
using CmsApiSamples.Protocol.CMSApi;
using CMSApi = CmsApiSamples.Protocol.CMSApi;

namespace CmsApiSamples.Services
{
    /// <summary>
    /// Class to demonstrate usage of Orders related CMS API messages.
    /// </summary>
    public sealed class OrdersService
    {
        private readonly CmsApiProxy _api;

        public OrdersService(CmsApiProxy api)
        {
            _api = api;
        }

        /// <summary>
        /// Adds fill.
        /// </summary>
        public async Task<ServiceResult> AddFill(uint requestId, int accountId,
                                                 string orderId,
                                                 double price,
                                                 uint quantity,
                                                 long? fillTime,
                                                 bool? isAggressive)
        {
            AddFill addFill = Protocol.CMSApi.AddFill.CreateBuilder()
                .SetAccountId(accountId)
                .SetOrderId(orderId)
                .SetPrice(price)
                .SetQuantity(quantity)
                .SetIfNotNull(fillTime, b => b.SetFillUtcTime)
                .SetIfNotNull(isAggressive, b => b.SetIsAggressive)
                .Build();

            OrderScopeRequest orderScopeRequest = OrderScopeRequest.CreateBuilder()
                .SetAddFill(addFill)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, orderScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Creates External Order.
        /// </summary>
        public async Task<ServiceResult> CreateExternalOrder(uint requestId, int accountId,
                                                             string userId,
                                                             string contractSymbol,
                                                             double fillPrice,
                                                             uint fillQuantity,
                                                             long? fillTime,
                                                             double? limitPrice,
                                                             uint orderType,
                                                             uint side,
                                                             double? stopPrice)
        {
            CreateExternalOrder createExternalOrder = Protocol.CMSApi.CreateExternalOrder.CreateBuilder()
                .SetAccountId(accountId)
                .SetUserId(userId)
                .SetContractSymbol(contractSymbol)
                .SetFillPrice(fillPrice)
                .SetFillQuantity(fillQuantity)
                .SetOrderType(orderType)
                .SetSide(side)
                .SetIfNotNull(stopPrice, b => b.SetStopPrice)
                .SetIfNotNull(fillTime, b => b.SetFillUtcTime)
                .SetIfNotNull(limitPrice, b => b.SetLimitPrice)
                .Build();

            OrderScopeRequest orderScopeRequest = OrderScopeRequest.CreateBuilder()
                .SetCreateExternalOrder(createExternalOrder)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, orderScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests order details.
        /// </summary>
        public async Task<ServiceResult> OrderDetailsRequest(uint requestId, int accountId, string orderId)
        {
            OrderDetailsRequest orderDetailsRequest = Protocol.CMSApi.OrderDetailsRequest.CreateBuilder()
                .SetAccountId(accountId)
                .SetOrderId(orderId)
                .Build();

            OrderScopeRequest orderScopeRequest = OrderScopeRequest.CreateBuilder()
                .SetOrderDetailsRequest(orderDetailsRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, orderScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests tree with related order chains for synthetic or synthetic strategy orders.
        /// </summary>
        public async Task<ServiceResult> RelatedOrderTreeRequest(uint requestId, string chainOrderId, string tradeLocationId, uint? orderRequestsTop = null, uint? top = null)
        {
            RelatedOrderTreeRequest relatedTreeRequest = Protocol.CMSApi.RelatedOrderTreeRequest.CreateBuilder()
                .SetChainOrderId(chainOrderId)
                .SetTradeLocationId(tradeLocationId)
                .SetIfNotNull(orderRequestsTop, b => b.SetOrderRequestsTop)
                .SetIfNotNull(top, b => b.SetTop)
                .Build();

            OrderScopeRequest orderScopeRequest = OrderScopeRequest.CreateBuilder()
                .SetRelatedOrderTreeRequest(relatedTreeRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, orderScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests compound orders tree.
        /// </summary>
        public async Task<ServiceResult> CompoundOrderTreeRequest(uint requestId, string compoundTreeId, string tradeLocationId, uint? orderRequestsTop = null, uint? top = null)
        {
            CompoundOrderTreeRequest compundOrderTreeRequest = Protocol.CMSApi.CompoundOrderTreeRequest.CreateBuilder()
                .SetCompoundTreeId(compoundTreeId)
                .SetTradeLocationId(tradeLocationId)
                .SetIfNotNull(orderRequestsTop, b => b.SetOrderRequestsTop)
                .SetIfNotNull(top, b => b.SetTop)
                .Build();

            OrderScopeRequest orderScopeRequest = OrderScopeRequest.CreateBuilder()
                .SetCompoundOrderTreeRequest(compundOrderTreeRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, orderScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests order strategy tree, describing synthetic strategy.
        /// </summary>
        public async Task<ServiceResult> SyntheticStrategyTreeRequest(uint requestId, string rootChainOrderId, string tradeLocationId)
        {
            SyntheticStrategyTreeRequest syntheticStrategyTreeRequest = Protocol.CMSApi.SyntheticStrategyTreeRequest.CreateBuilder()
                .SetRootChainOrderId(rootChainOrderId)
                .SetTradeLocationId(tradeLocationId)
                .Build();

            OrderScopeRequest orderScopeRequest = OrderScopeRequest.CreateBuilder()
                .SetSyntheticStrategyTreeRequest(syntheticStrategyTreeRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, orderScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Cancels order.
        /// </summary>
        public async Task<ServiceResult> CancelOrder(uint requestId, string chainOrderId, int? accountId = null, string tradeLocationId = null)
        {
            CancelOrder cancelOrder = Protocol.CMSApi.CancelOrder.CreateBuilder()
                .SetChainOrderId(chainOrderId)
                .SetIfNotNull(accountId, b => b.SetAccountId)
                .SetIfNotNull(tradeLocationId, b => b.SetTradeLocationId)
                .Build();

            OrderScopeRequest orderScopeRequest = OrderScopeRequest.CreateBuilder()
                .SetCancelOrder(cancelOrder)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, orderScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Reflect order as canceled without sending real cancel request to exchange.
        /// </summary>
        public async Task<ServiceResult> ReflectAsCanceledOrder(uint requestId, string chainOrderId, int? accountId = null, string tradeLocationId = null)
        {
            ReflectAsCanceledOrder reflectAsCanceledOrder = Protocol.CMSApi.ReflectAsCanceledOrder.CreateBuilder()
                .SetChainOrderId(chainOrderId)
                .SetIfNotNull(accountId, b => b.SetAccountId)
                .SetIfNotNull(tradeLocationId, b => b.SetTradeLocationId)
                .Build();

            OrderScopeRequest orderScopeRequest = OrderScopeRequest.CreateBuilder()
                .SetReflectAsCanceledOrder(reflectAsCanceledOrder)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, orderScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Cancel whole tree of compound orders.
        /// </summary>
        public async Task<ServiceResult> CancelCompoundOrderTree(uint requestId, string compoundTreeId, int? accountId = null, string tradeLocationId = null)
        {
            CancelCompoundOrderTree cancelCompoundOrderTree = Protocol.CMSApi.CancelCompoundOrderTree.CreateBuilder()
                .SetCompoundTreeId(compoundTreeId)
                .SetIfNotNull(accountId, b => b.SetAccountId)
                .SetIfNotNull(tradeLocationId, b => b.SetTradeLocationId)
                .Build();

            OrderScopeRequest orderScopeRequest = OrderScopeRequest.CreateBuilder()
                .SetCancelCompoundOrderTree(cancelCompoundOrderTree)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, orderScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Searches order by search options.
        /// </summary>
        public async Task<ServiceResult> OrderSearchRequest(uint requestId, IEnumerable<SearchOption> searchOptions, uint? top, bool? allMatchMode, bool? onlyArchived)
        {
            OrderSearchRequest orderSearchRequest = Protocol.CMSApi.OrderSearchRequest.CreateBuilder()
                .AddRangeSearchOptions(searchOptions)
                .SetIfNotNull(top, b => b.SetTop)
                .SetIfNotNull(allMatchMode, b => b.SetAllMatchMode)
                .SetIfNotNull(onlyArchived, b => b.SetArchived)
                .Build();

            OrderScopeRequest orderScopeRequest = OrderScopeRequest.CreateBuilder()
                .SetOrderSearchRequest(orderSearchRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, orderScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        private ClientMessage createClientMessage(uint requestId, OrderScopeRequest orderScopeRequest)
        {
            TradeRoutingRequest tradeRoutingRequest = TradeRoutingRequest.CreateBuilder()
                .SetOrderScopeRequest(orderScopeRequest)
                .SetId(requestId)
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddTradeRoutingRequest(tradeRoutingRequest)
                .Build();

            return clientMessage;
        }
    }
}
