using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using CmsApiSamples.Protocol;
using CmsApiSamples.Protocol.Extensions;
using CmsApiSamples.Protocol.CMSApi;
using CMSApi = CmsApiSamples.Protocol.CMSApi;

namespace CmsApiSamples.Services
{
    /// <summary>
    /// Class to demonstrate usage of Account related CMS API messages.
    /// </summary>
    public sealed class AccountsService
    {
        private readonly CmsApiProxy _api;

        public AccountsService(CmsApiProxy api)
        {
            _api = api;
        }

        /// <summary>
        /// Requests account information.
        /// </summary>
        public async Task<ServiceResult> AccountInfoRequest(uint requestId, int? accountId, string brokerageAccountNumber)
        {
            AccountInfoRequest accountInfoRequest = Protocol.CMSApi.AccountInfoRequest.CreateBuilder()
                .SetIfNotNull(accountId, b => b.SetAccountId)
                .SetIfNotNull(brokerageAccountNumber, b => b.SetBrokerageAccountNumber)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountInfoRequest(accountInfoRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests account market limits.
        /// </summary>
        public async Task<ServiceResult> AccountMarketLimitsRequest(uint requestId, int accountId)
        {
            AccountMarketLimitsRequest accountMarketLimitsRequest = Protocol.CMSApi.AccountMarketLimitsRequest.CreateBuilder()
                .SetAccountId(accountId)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountMarketLimitsRequest(accountMarketLimitsRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Searches account by search opeitons.
        /// </summary>
        public async Task<ServiceResult> AccountSearchRequest(uint requestId, IEnumerable<SearchOption> searchOptions, uint? top, bool? allMatchMode, bool? includeRemoved)
        {
            AccountSearchRequest accountSearchRequest = Protocol.CMSApi.AccountSearchRequest.CreateBuilder()
                .AddRangeSearchOptions(searchOptions)
                .SetIfNotNull(allMatchMode, b => b.SetAllMatchMode)
                .SetIfNotNull(top, b => b.SetTop)
                .SetIfNotNull(includeRemoved, b => b.SetIncludeRemoved)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountSearchRequest(accountSearchRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests account settings.
        /// </summary>
        public async Task<ServiceResult> AccountSettingsRequest(uint requestId, int accountId)
        {
           
            AccountSettingsRequest accountSettingsRequest = Protocol.CMSApi.AccountSettingsRequest.CreateBuilder()
                .SetAccountId(accountId)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountSettingsRequest(accountSettingsRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests account user authorization list.
        /// </summary>
        public async Task<ServiceResult> AccountUserAuthorizationListRequest(uint requestId, int? accountId, string userId, uint? top)
        {
            AccountUserAuthorizationListRequest accountUserAuthorizationList = Protocol.CMSApi.AccountUserAuthorizationListRequest.CreateBuilder()
                .SetIfNotNull(accountId, b => b.SetAccountId)
                .SetIfNotNull(userId, b => b.SetUserId)
                .SetIfNotNull(top, b => b.SetTop)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountUserAuthorizationListRequest(accountUserAuthorizationList)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests account available exchange groups.
        /// </summary>
        public async Task<ServiceResult> AccountAvailableExchangeGroupsRequest(uint requestId, int accountId)
        {
            AccountAvailableExchangeGroupsRequest accountAvailableExchangeGroupsRequest = Protocol.CMSApi.AccountAvailableExchangeGroupsRequest.CreateBuilder()
                .SetAccountId(accountId)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountAvailableExchangeGroupsRequest(accountAvailableExchangeGroupsRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests balance records.
        /// </summary>
        public async Task<ServiceResult> BalanceRecordsRequest(uint requestId, int? accountId, int? balanceId, string currencyCode)
        {
            BalanceRecordsRequest balanceRecordsRequest = Protocol.CMSApi.BalanceRecordsRequest.CreateBuilder()
                .SetIfNotNull(accountId, b => b.SetAccountId)
                .SetIfNotNull(balanceId, b => b.SetBalanceId)
                .SetIfNotNull(currencyCode, b => b.SetCurrency)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetBalanceRecordsRequest(balanceRecordsRequest)
                .Build();
            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Creates balance record.
        /// </summary>
        public async Task<ServiceResult> CreateBalanceRecord(uint requestId, int accountId, double? collateral, string currencyCode, double endCashBalance)
        {
            CreateBalanceRecord createBalanceRecord = Protocol.CMSApi.CreateBalanceRecord.CreateBuilder()
                .SetAccountId(accountId)
                .SetCurrency(currencyCode)
                .SetEndCashBalance(endCashBalance)
                .SetIfNotNull(collateral, b => b.SetCollateral)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetCreateBalanceRecord(createBalanceRecord)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Update balance record
        /// </summary>
        public async Task<ServiceResult> UpdateBalanceRecord(uint requestId, int balanceId, double? collateral, double endCashBalance)
        {
            UpdateBalanceRecord updateBalanceRecord = Protocol.CMSApi.UpdateBalanceRecord.CreateBuilder()
                .SetBalanceId(balanceId)
                .SetEndCashBalance(endCashBalance)
                .SetIfNotNull(collateral, b => b.SetCollateral)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateBalanceRecord(updateBalanceRecord)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests risk parameters.
        /// </summary>
        public async Task<ServiceResult> AccountRiskParametersRequest(uint requestId, int accountId)
        {
            AccountRiskParametersRequest accountRiskParametersRequest = Protocol.CMSApi.AccountRiskParametersRequest.CreateBuilder()
                .SetAccountId(accountId)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountRiskParametersRequest(accountRiskParametersRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Updates risk parameters.
        /// </summary>
        public async Task<ServiceResult> UpdateRiskParameters(uint requestId, int accountId,
                                                              bool? liquidationOnly, bool? allowFutures, uint? allowOptions)
        {
            UpdateRiskParameters updateRiskParameter = Protocol.CMSApi.UpdateRiskParameters.CreateBuilder()
                .SetAccountId(accountId)
                .SetIfNotNull(liquidationOnly, b => b.SetLiquidationOnly)
                .SetIfNotNull(allowFutures, b => b.SetAllowFutures)
                .SetIfNotNull(allowOptions, b => b.SetAllowOptions)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateRiskParameters(updateRiskParameter)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests account positions.
        /// </summary>
        public async Task<ServiceResult> AccountPositionsRequest(uint requestId, int accountId)
        {
            AccountPositionsRequest accountPositionsRequest = Protocol.CMSApi.AccountPositionsRequest.CreateBuilder()
                .SetAccountId(accountId)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountPositionsRequest(accountPositionsRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests account routes.
        /// </summary>
        public async Task<ServiceResult> AccountRouteListRequest(uint requestId, int accountId)
        {
            AccountRouteListRequest accountRouteListRequest = Protocol.CMSApi.AccountRouteListRequest.CreateBuilder()
                .SetAccountId(accountId)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountRouteListRequest(accountRouteListRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests account collateral.
        /// </summary>
        public async Task<ServiceResult> AccountCollateralRequest(uint requestId, int accountId)
        {
            AccountCollateralRequest accountCollateralRequest = Protocol.CMSApi.AccountCollateralRequest.CreateBuilder()
                .SetAccountId(accountId)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountCollateralRequest(accountCollateralRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests account equity.
        /// </summary>
        public async Task<ServiceResult> AccountEquityRequest(uint requestId, int accountId, string currency)
        {
            AccountEquityRequest accountEquityRequest = Protocol.CMSApi.AccountEquityRequest.CreateBuilder()
                .SetAccountId(accountId)
                .SetIfNotNull(currency, b => b.SetCurrency)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountEquityRequest(accountEquityRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Clones account.
        /// </summary>
        public async Task<ServiceResult> CloneAccount(uint requestId, int sourceAccountId, string newAccountName,
                                                      string newAccountBrokerageNumber, string newAccountUserId)
        {
            CloneAccount cloneAccount = Protocol.CMSApi.CloneAccount.CreateBuilder()
               .SetSourceAccountId(sourceAccountId)
               .SetNewAccountName(newAccountName)
               .SetNewAccountBrokerageNumber(newAccountBrokerageNumber)
               .SetIfNotNull(newAccountUserId, b => b.SetNewAccountUserId)
               .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetCloneAccount(cloneAccount)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Updates account record.
        /// </summary>
        public async Task<ServiceResult> UpdateAccount(uint requestId, int accountId, string newAccountName)
        {
            AccountInfoRequest accountInfoRequest = CMSApi.AccountInfoRequest.CreateBuilder()
               .SetAccountId(accountId)
               .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountInfoRequest(accountInfoRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);
            Account account = serverMessage.GetTradeRoutingResult(0).AccountScopeResult.AccountInfoResult.Account;

            Account changedAccount = Protocol.CMSApi.Account.CreateBuilder()
                .SetId(accountId)
                .SetName(newAccountName)
                .SetBrokerageAccountNumber(account.BrokerageAccountNumber)
                .SetBrokerageName(account.BrokerageName)
                .SetSalesSeriesNumber(account.SalesSeriesNumber)
                .SetSalesSeriesName(account.SalesSeriesName)
                .SetSalesSeriesId(account.SalesSeriesId)
                .SetClass(account.Class)
                .SetCustomerId(account.CustomerId)
                .SetAccountTypeId(account.AccountTypeId)
                .Build();

            UpdateAccount updateAccount = Protocol.CMSApi.UpdateAccount.CreateBuilder()
                .SetAccount(changedAccount)
                .Build();
            accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateAccount(updateAccount)
                .Build();

            clientMessage = createClientMessage(++requestId, accountScopeRequest);
            serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests property list.
        /// </summary>
        public async Task<ServiceResult> LookupPropertyListRequest(uint requestId, uint typeID)
        {
            LookupPropertyListRequest lookupPropertyListRequest = CMSApi.LookupPropertyListRequest.CreateBuilder()
             .AddPropertyType(typeID)
             .Build();

            TradeRoutingRequest tradeRoutingRequest = TradeRoutingRequest.CreateBuilder()
               .SetLookupPropertyListRequest(lookupPropertyListRequest)
               .SetId(requestId)
               .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddTradeRoutingRequest(tradeRoutingRequest)
                .Build();
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Updates account settings.
        /// </summary>
        public async Task<ServiceResult> UpdateAccountSettings(uint requestId, AccountSettings settings, AccountSettings settingsOrig = null)
        {
            UpdateAccountSettings accountSettingsRequest = Protocol.CMSApi.UpdateAccountSettings.CreateBuilder()
                .SetSettings(settings)
                .SetIfNotNull(settingsOrig, b => b.SetOriginalSettings)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateAccountSettings(accountSettingsRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }


        /// <summary>
        /// Updates account risk parameters.
        /// </summary>
        public async Task<ServiceResult> UpdateAccountRiskParameters(uint requestId, AccountRiskParameters riskParameters, AccountRiskParameters riskParametersOrig = null)
        {
            UpdateAccountRiskParameters updAccountRiskParametersRequest = Protocol.CMSApi.UpdateAccountRiskParameters.CreateBuilder()
                .SetRiskParameters(riskParameters)
                .SetIfNotNull(riskParametersOrig, b => b.SetOriginalRiskParameters)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateAccountRiskParameters(updAccountRiskParametersRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Updates account route list.
        /// </summary>
        public async Task<ServiceResult> UpdateAccountRouteList(uint requestId, int accountId, AccountRouteRecord[] routesToSet, string[] routeCodesToRemove)
        {

            UpdateAccountRouteList updAccountRouteListRequest = Protocol.CMSApi.UpdateAccountRouteList.CreateBuilder()
                .SetAccountId(accountId.ToString())
                .SetIfNotNull(routesToSet, b => b.AddRangeRoutesToSet)
                .SetIfNotNull(routeCodesToRemove, b => b.AddRangeRouteCodesToRemove)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateAccountRouteList(updAccountRouteListRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Updates account authorization list. NOT IMPLEMENTED in 2.7 API. For future use.
        /// </summary>
        public async Task<ServiceResult> UpdateAccountUserAuthorizationList(uint requestId, AccountUserLink[] linksToSet = null, CMSApi.Tuple[] linksToRemove = null)
        {

            UpdateAccountUserAuthorizationList updAccountUserAuthorizationList = Protocol.CMSApi.UpdateAccountUserAuthorizationList.CreateBuilder()
                .SetIfNotNull(linksToSet, b => b.AddRangeLinksToSet)
                .SetIfNotNull(linksToRemove, b => b.AddRangeLinksToRemove)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateAccountUserAuthorizationList(updAccountUserAuthorizationList)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Updates account market limits.
        /// </summary>
        public async Task<ServiceResult> UpdateAccountMarketLimits(uint requestId, int accountId, MarketLimits marketLimits, MarketLimits marketLimitsOrig = null)
        {
            UpdateAccountMarketLimits updateMarketLimits = Protocol.CMSApi.UpdateAccountMarketLimits.CreateBuilder()
                .SetAccountId(accountId.ToString())
                .SetMarketLimits(marketLimits)
                .SetIfNotNull(marketLimitsOrig, b => b.SetOriginalMarketLimits)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateAccountMarketLimits(updateMarketLimits)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Creates account.
        /// </summary>
        public async Task<ServiceResult> CreateAccount(uint requestId, Account account)
        {
            CreateAccount createAccount = Protocol.CMSApi.CreateAccount.CreateBuilder()
                .SetAccount(account)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetCreateAccount(createAccount)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Restore Accounts.
        /// </summary>
        public async Task<ServiceResult> RestoreAccount(uint requestId, int accountId)
        {
            RestoreAccount restoreAccount = Protocol.CMSApi.RestoreAccount.CreateBuilder()
                .SetAccountId(accountId.ToString())
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetRestoreAccount(restoreAccount)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        public async Task<ServiceResult> AccountTypeOverridesRequest(uint requestId, int accountId)
        {
            AccountTypeOverrideListRequest overridesRequest = Protocol.CMSApi.AccountTypeOverrideListRequest.CreateBuilder()
                .SetAccountId(accountId.ToString())
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountTypeOverrideListRequest(overridesRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }


        /// <summary>
        /// Updates account type override list.
        /// </summary>
        public async Task<ServiceResult> UpdateAccountTypeOverrideList(uint requestId, int accountId, AccountTypeOverride[] overridesToSet = null, CMSApi.Tuple[] overridesToRemove = null)
        {
            UpdateAccountTypeOverrideList overridesUpdate = Protocol.CMSApi.UpdateAccountTypeOverrideList.CreateBuilder()
                .SetAccountId(accountId.ToString())
                .SetIfNotNull(overridesToSet, b => b.AddRangeOverridesToSet)
                .SetIfNotNull(overridesToRemove, b => b.AddRangeOverridesToRemove)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateAccountTypeOverrideList(overridesUpdate)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Gets account group.
        /// </summary>
        public async Task<ServiceResult> AccountGroupRequest(uint requestId, int accountId)
        {
            AccountGroupRequest accountGroupRequest = Protocol.CMSApi.AccountGroupRequest.CreateBuilder()
                .SetAccountId(accountId.ToString())
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountGroupRequest(accountGroupRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Gets route list.
        /// </summary>
        public async Task<ServiceResult> RouteListRequest(uint requestId, string accountId)
        {
            RouteListRequest accountRouteListRequest = Protocol.CMSApi.RouteListRequest.CreateBuilder()
                .SetAccountId(accountId)
                .Build();
            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetRouteListRequest(accountRouteListRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Requests account type list.
        /// </summary>
        public async Task<ServiceResult> AccountTypeRequest(uint requestId)
        {
            return await LookupPropertyListRequest(requestId, (uint)TradeRoutingLookupPropertyType.ACCOUNT_TYPE);
        }

        /// <summary>
        /// Requests account cluster.
        /// </summary>
        public async Task<ServiceResult> AccountClusterRequest(uint requestId, string accountClusterId)
        {
            AccountClusterRequest accountClusterRequest = Protocol.CMSApi.AccountClusterRequest.CreateBuilder()
                .SetAccountClusterId(accountClusterId)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetAccountClusterRequest(accountClusterRequest)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Creates account cluster.
        /// </summary>
        public async Task<ServiceResult>CreateAccountClusterRequest(uint requestId, string clusterName, string brokerageId)
        {
            AccountCluster accountCluster = FillMessageHelper.FillAccountCluster(brokerageId : brokerageId, name : clusterName);

            CreateAccountCluster createAccountCluster = Protocol.CMSApi.CreateAccountCluster.CreateBuilder()
                .SetAccountCluster(accountCluster)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetCreateAccountCluster(createAccountCluster)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Updates account cluster.
        /// </summary>
        public async Task<ServiceResult> UpdateAccountCluster(uint requestId, AccountCluster accountCluster, AccountCluster accountClusterOrig = null)
        {

            UpdateAccountCluster updateAccountCluster = Protocol.CMSApi.UpdateAccountCluster.CreateBuilder()
                .SetAccountCluster(accountCluster)
                .SetIfNotNull(accountClusterOrig, b=> b.SetOriginalAccountCluster)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateAccountCluster(updateAccountCluster)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        /// <summary>
        /// Updates account cluster price Offsets.
        /// </summary>
        public async Task<ServiceResult> UpdateAccountClusterPriceOffsets(uint requestId, string accountClusterId, 
            AccountClusterPriceOffset[] offsetsToSet = null, AccountClusterPriceOffset[] offsetsToRemove = null)
        {

            UpdateAccountClusterPriceOffsets updateAccountPriceOffsets = Protocol.CMSApi.UpdateAccountClusterPriceOffsets.CreateBuilder()
                .SetAccountClusterId(accountClusterId)
                .SetIfNotNull(offsetsToSet, b => b.AddRangeOffsetsToSet)
                .SetIfNotNull(offsetsToRemove, b => b.AddRangeOffsetsToRemove)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder()
                .SetUpdateAccountClusterPriceOffsets(updateAccountPriceOffsets)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, accountScopeRequest);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        private ClientMessage createClientMessage(uint requestId, AccountScopeRequest accountScopeRequest)
        {
            TradeRoutingRequest tradeRoutingRequest = TradeRoutingRequest.CreateBuilder()
                .SetAccountScopeRequest(accountScopeRequest)
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
