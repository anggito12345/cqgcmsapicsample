using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CmsApiSamples;
using CmsApiSamples.Protocol;
using CmsApiSamples.Protocol.CMSApi;
using CmsApiSamples.Services;
using Tuple = CmsApiSamples.Protocol.CMSApi.Tuple;

namespace CmsApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string CmsApiUrl = appSettings["CmsApiUrl"];
            string userName = appSettings["UserName"];
            string userPassword = appSettings["UserPassword"];

            // Creates new instance of service.
            CmsApiService service = new CmsApiService(CmsApiUrl);
            Console.WriteLine("Service started.");
            try
            {
                // Logons with user which has access to CmsApi.
                printServerMessage(service.SessionService.Logon(userName, userPassword));

                uint requestId = 0;


                //executeAccountRelatedRequests(service, ref requestId);

                printServerMessage(service.OperationsService.createBalanceRequestService(++requestId));

                /*executeInformationRelatedRequests(service, ref requestId);
                executeSearchRequests(service, ref requestId);
                
                calculateEffectiveMarketLimits(service, ref requestId);
                executeOrderRelatedRequests(service, ref requestId);
                executeOperationRelatedRequests(service, ref requestId);*/

                // Sends Logoff message.
                printServerMessage(service.SessionService.Logoff());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                service.CloseWebSocket();
                Console.WriteLine("Service stopped.");
            }

            Console.WriteLine("Press any key to close window.");
            Console.ReadKey();
        }

        public static System.Collections.Specialized.NameValueCollection getConfiguration()
        {
            return ConfigurationManager.AppSettings;
        }

        private static void executeInformationRelatedRequests(CmsApiService service, ref uint requestId)
        {
            // Please fill the following variables with your values. This fields are required for client messages.
            const string USER_ID = "";//"G567", "A396", "I141002"
            const uint ENTITLEMENT_SERVICE_ID = 0; 
            const string CUSTOMER_ID = "";

            // Gets trader info by ID.
            printServerMessage(service.InformationService.UserInfoRequest(++requestId, USER_ID));

            // Gets entitlement categories.
            printServerMessage(service.InformationService.EntitlementCategoryListRequest(++requestId));

            // Gets user's entitlement services.
            printServerMessage(service.InformationService.UserEntitlementServiceRequest(++requestId, USER_ID));

            // Gets entitlement service.
            printServerMessage(service.InformationService.EntitlementServiceRequest(++requestId, ENTITLEMENT_SERVICE_ID));

            // Gets customer by ID.
            printServerMessage(service.InformationService.CustomerRequest(++requestId, CUSTOMER_ID));

            
        }

        private static void executeSearchRequests(CmsApiService service, ref uint requestId)
        {
            // Please fill the following variables with your values. This fields are required for client messages.
            const string USER_SEARCH_TEXT = "CmsApiDemo";
            string USER_SEARCH_DOMAIN = LoginDomain.CQG_TRADE_ROUTING.ToString();
            const string CUSTOMER_SEARCH_TEXT = "CmsApiDemo";
            const string SALES_SERIES_SEARCH_TEXT = "CmsApiDemo";

            // Searches users.
            // Prepare searchOptions: we will search users containing USER_SEARCH_TEXT in user name and search only among CQG Gateway login domain (CQG_TRADE_ROUTING).
            SearchOption option1 = FillMessageHelper.FillSearchOption(USER_SEARCH_TEXT, (uint)UserSearchRequest.Types.SearchCriteria.USER_NAME,
                (uint) SearchOption.Types.MatchingRule.CONTAINS);
            SearchOption option2 = FillMessageHelper.FillSearchOption(USER_SEARCH_DOMAIN, (uint)UserSearchRequest.Types.SearchCriteria.LOGIN_DOMAIN,
                null);
            List<SearchOption > searchOptions = new List<SearchOption>(){ option1, option2 };

            printServerMessage(service.SearchService.UserSearchRequest(++requestId, 
                searchOptions: searchOptions,
                top: null,
                allMatchMode: true,
                includeRemoved: null));

            // Searches users, including removed.
            printServerMessage(service.SearchService.UserSearchRequest(++requestId,
                searchOptions: searchOptions,
                top: null,
                allMatchMode: null,
                includeRemoved: true));

            // Searches customers.
            SearchOption option3 = FillMessageHelper.FillSearchOption(CUSTOMER_SEARCH_TEXT, (uint)CustomerSearchRequest.Types.SearchCriteria.NAME,
                (uint)SearchOption.Types.MatchingRule.CONTAINS);
            searchOptions = new List<SearchOption>() {option3};
            printServerMessage(service.SearchService.CustomerSearchRequest(++requestId,
                searchOptions: searchOptions,
                top: null,
                allMatchMode: null,
                includeRemoved: null));

            // Searches customers, including removed.
            printServerMessage(service.SearchService.CustomerSearchRequest(++requestId,
                searchOptions: searchOptions,
                top: null,
                allMatchMode: null,
                includeRemoved: true));

            // Searches sales series.
            SearchOption option4 = FillMessageHelper.FillSearchOption(SALES_SERIES_SEARCH_TEXT, (uint)SalesSeriesSearchRequest.Types.SearchCriteria.NAME,
                (uint)SearchOption.Types.MatchingRule.CONTAINS);
            searchOptions = new List<SearchOption>() { option4 };
            printServerMessage(service.SearchService.SalesSeriesSearchRequest(++requestId,
                searchOptions: searchOptions,
                top: null,
                allMatchMode: null,
                includeRemoved: null));
            // Searches sales series, including removed.
            printServerMessage(service.SearchService.SalesSeriesSearchRequest(++requestId,
                searchOptions: searchOptions,
                top: null,
                allMatchMode: null,
                includeRemoved: true));
        }

        private static void executeOperationRelatedRequests(CmsApiService service, ref uint requestId)
        {
            // Please fill the following variables with your values. This fields are required for client messages.
            const string USER_ID = "";
            const string ADDRESS_COUNTRY = "US";
            const string ADDRESS_STATE = "DC";
            const string EMAIL = "myemail@mail.com";

            List<string> userEmails = new List<string>();
            userEmails.Add(EMAIL);

            // These fields are auto generated as an example. Use your values from CQG Gateway Stage environment.
            string newUserName = string.Format("CloneUser{0}_{1}", USER_ID, DateTime.Now.ToString("ffff"));
            string newUserFirstName = string.Format("Clone{0}_{1}", USER_ID, DateTime.Now.ToString("ffff"));
            string newUserLastName = string.Format("CloneUserLastName{0}_{1}", USER_ID, DateTime.Now.ToString("ffff"));

            // Clones user.
            printServerMessage(service.OperationsService.CloneUser(++requestId, USER_ID, newUserName, newUserFirstName, newUserLastName, null,
                ADDRESS_COUNTRY, ADDRESS_STATE, null, null, null, null, userEmails, null, null));
        }

        private static void executeAccountRelatedRequests(CmsApiService service, ref uint requestId)
        {
            // Please fill the following variables with your values. This fields are required for client messages.
            const int ACCOUNT_ID = 0;
            const int REMOVED_ACCOUNT_ID = 0;
            const string ACCOUNT_CLUSTER_ID = "";
            const string BROKERAGE_ACCOUNT_NUMBER = "";
            const string BROKERAGE_ID = "";
            const string SALES_SERIES_ID = "";
            const string CUSTOMER_ID = "";
            const string USER_ID = "";
            const string SEARCH_TEXT = "CmsApiDemo";
            const int BALANCE_ID = 0;

            const string CURRENCY_CODE = "EUR";
            const int DEFAULT_CLASS_ID = 1;
            const int DEFAULT_SUB_CLASS = 1;
            const string DEFAULT_ACCOUNT_TYPE = "2";

            const string NEW_ACCOUNT_NAME = "";
            const string NEW_CLUSTER_NAME = "";
            const int ROUTE_CODE1 = 0;
            const int ROUTE_CODE2 = 0;
            const int ROUTE_CODE3 = 0;
            const int ROUTE_CODE4 = 0;

            const string MODES_GROUP1_ID = "";
            const string MODES_GROUP2_ID = "";
            const string TRADING_TIME_ZONE_ID = "";

            const string OVERRIDES_EXCHANGE_KEY = ""; // e.g. "84"
            const string OVERRIDES_LOGIN_ID = ""; // e.g "36"
            const string OVERRIDES_TYPE_ID = ""; // e.g. "1"
            const string OVERRIDES_ORIGIN_ID = "";  // e.g. "1"

            const string COMMODITY1_ID = ""; // e.g. "1"
            const string COMMODITY2_ID = "";
            const string COMMODITY3_ID = "";
            const int INSTRUMENT_TYPE1_ID = 0;
            const int INSTRUMENT_TYPE2_ID = 0;
            const int INSTRUMENT_TYPE3_ID = 0;

            // Gets account information by account ID or Brokerage Account Number.
            printServerMessage(service.AccountsService.AccountInfoRequest(++requestId, ACCOUNT_ID, null));
            printServerMessage(service.AccountsService.AccountInfoRequest(++requestId, null, BROKERAGE_ACCOUNT_NUMBER));

            // Searches account (without removed records, includeRemoved is false by default).
            SearchOption option1 = FillMessageHelper.FillSearchOption(SEARCH_TEXT, (uint)AccountSearchRequest.Types.SearchCriteria.ACCOUNT_NAME,
                (uint)SearchOption.Types.MatchingRule.CONTAINS);

            List<SearchOption> searchOptions = new List<SearchOption>() { option1 };
            printServerMessage(service.AccountsService.AccountSearchRequest(++requestId,
                searchOptions: searchOptions,
                top: null,
                allMatchMode: null,
                includeRemoved: null));
            // Searches account (with removed records).
            printServerMessage(service.AccountsService.AccountSearchRequest(++requestId,
                searchOptions: searchOptions,
                top: null,
                allMatchMode: null,
                includeRemoved: true));

            // Gets account settings.
            printServerMessage(service.AccountsService.AccountSettingsRequest(++requestId, ACCOUNT_ID));

            // Gets account user authorization list.
            printServerMessage(service.AccountsService.AccountUserAuthorizationListRequest(++requestId, ACCOUNT_ID, USER_ID, null));

            // Gets account available exchange groups.
            printServerMessage(service.AccountsService.AccountAvailableExchangeGroupsRequest(++requestId, ACCOUNT_ID));

            // Gets account balances records.
            printServerMessage(service.AccountsService.BalanceRecordsRequest(++requestId, ACCOUNT_ID, null, null));
            printServerMessage(service.AccountsService.BalanceRecordsRequest(++requestId, ACCOUNT_ID, null, CURRENCY_CODE));
            printServerMessage(service.AccountsService.BalanceRecordsRequest(++requestId, null, BALANCE_ID, null));

            // Creates balance record for account.
            printServerMessage(service.AccountsService.CreateBalanceRecord(++requestId, ACCOUNT_ID, null, CURRENCY_CODE, 5.0));
            printServerMessage(service.AccountsService.BalanceRecordsRequest(++requestId, ACCOUNT_ID, null, CURRENCY_CODE));

            // Updates balance record by ID.
            printServerMessage(service.AccountsService.UpdateBalanceRecord(++requestId, BALANCE_ID, 5.9, 999.9));
            printServerMessage(service.AccountsService.BalanceRecordsRequest(++requestId, null, BALANCE_ID, null));
            printServerMessage(service.AccountsService.UpdateBalanceRecord(++requestId, BALANCE_ID, null, 888.8));
            printServerMessage(service.AccountsService.BalanceRecordsRequest(++requestId, null, BALANCE_ID, null));

            // Gets account risk parameters by account ID.
            printServerMessage(service.AccountsService.AccountRiskParametersRequest(++requestId, ACCOUNT_ID));

            // Updates account risk parameters.
            printServerMessage(service.AccountsService.UpdateRiskParameters(++requestId, ACCOUNT_ID, true, true, 1));
            printServerMessage(service.AccountsService.AccountRiskParametersRequest(++requestId, ACCOUNT_ID));
            printServerMessage(service.AccountsService.UpdateRiskParameters(++requestId, ACCOUNT_ID, false, false, null));
            printServerMessage(service.AccountsService.AccountRiskParametersRequest(++requestId, ACCOUNT_ID));

            // Gets account positions.
            printServerMessage(service.AccountsService.AccountPositionsRequest(++requestId, ACCOUNT_ID));

            // Gets account routes.
            printServerMessage(service.AccountsService.AccountRouteListRequest(++requestId, ACCOUNT_ID));

            // Gets account collateral.
            printServerMessage(service.AccountsService.AccountCollateralRequest(++requestId, ACCOUNT_ID));

            // Gets account equity.
            printServerMessage(service.AccountsService.AccountEquityRequest(++requestId, ACCOUNT_ID, null));
            printServerMessage(service.AccountsService.AccountEquityRequest(++requestId, ACCOUNT_ID, CURRENCY_CODE));

            // This field is auto generated as an example. Use your values from CQG Gateway Stage environment.
            string newAccountName = string.Format("CloneAccount{0}_{1}", ACCOUNT_ID, DateTime.Now.ToString("ffff"));

            // Clones account.
            printServerMessage(service.AccountsService.CloneAccount(++requestId, ACCOUNT_ID, newAccountName, BROKERAGE_ACCOUNT_NUMBER, null));

            // This field is auto generated as an example. Use your values.
            string changedAccountName = string.Format("UpdateAccount{0}_{1}", ACCOUNT_ID, DateTime.Now.ToString("ffff"));

            // Updates account.
            printServerMessage(service.AccountsService.UpdateAccount(++requestId, ACCOUNT_ID, changedAccountName));

            // Updates account settings. Setup data.
            string[] modesAccountGroups = { MODES_GROUP1_ID, };
            string[] modesAccountGroupsOrig = { MODES_GROUP1_ID, MODES_GROUP2_ID };
            AccountSettings settingsOrig = FillMessageHelper.FillAccountSettings(ACCOUNT_ID, isInstruct: false,
                tradingTimeFrom: "11:00", tradingTimeTo: "12:00", tradingTimeZone: TRADING_TIME_ZONE_ID, modesArray: modesAccountGroupsOrig);
            // Note: It is valid case when tradingTimeFrom >  tradingTimeTo, it means overnight period.
            AccountSettings settings = FillMessageHelper.FillAccountSettings(ACCOUNT_ID, isInstruct: false,
                  tradingTimeFrom: "15:00", tradingTimeTo: "14:00", tradingTimeZone: TRADING_TIME_ZONE_ID, modesArray: modesAccountGroups);

            // [option1]: update settings using settingsOrig values only.
            printServerMessage(service.AccountsService.UpdateAccountSettings(++requestId, settingsOrig, null));
            // [option2]:update settings using diff between settings and settingsOrig values.
            printServerMessage(service.AccountsService.UpdateAccountSettings(++requestId, settings, settingsOrig));

            // Gets account group.
            printServerMessage(service.AccountsService.AccountGroupRequest(++requestId, ACCOUNT_ID));

            // Updating account risk parameters. Setup data.
            MarginSubsystemParameters marginSubsystemParameters = FillMessageHelper.FillMarginSubsystemParameters(
                allowedMarginCredit: 0,
                crossMargining: true,
                includeOtePp: (uint)MarginSubsystemParameters.Types.IncludeOption.YES,
                includeNovPp: (uint)MarginSubsystemParameters.Types.IncludeOption.YES,
                includeUplLl: (uint)MarginSubsystemParameters.Types.IncludeOption.YES,
                includeOteLl: (uint)MarginSubsystemParameters.Types.IncludeOption.YES,
                checkNegativeBalance: false,
                useTheoPrices: false,
                useBbaOte: true,
                useBbaNovUpl: true,
                adjustPriceByNetchange: false,
                useBrokerageMarginsOnly: false,
                marginMultiplier: 1);
            AccountRiskParameters riskParametersOrig = FillMessageHelper.FillAccountRiskParameters(ACCOUNT_ID,
                liquidationOnly: false,
                allowFutures: true,
                allowOptions: (uint)AccountRiskParameters.Types.OptionsTrading.ALL,
                enforceTradeSizeLimit: true,
                tradeSizeLimit: 0,
                enforceTradeMarginLimit: true,
                tradeMarginLimit: 0,
                enforceTradePriceLimit: true,
                tradePriceLimit: FillMessageHelper.FillTradePiceLimit((uint)PriceLimitMode.ALL_LMT, FillMessageHelper.FillLimitValue((uint)LimitMode.LIMITED, 1)),
                enforceCommodityPositionLimit: true,
                commodityPositionLimitValue: FillMessageHelper.FillLimitValue((uint)LimitMode.LIMITED, 2),
                enforceContractPositionLimit: true,
                contractPositionLimitValue: FillMessageHelper.FillLimitValue((uint)LimitMode.LIMITED, 3),
                enforceMarginSubsystemParams: true,
                marginSubsystemParameters: marginSubsystemParameters);

            // Updates account risk parameters.
            printServerMessage(service.AccountsService.UpdateAccountRiskParameters(++requestId, riskParametersOrig));

            // Gets route list.
            printServerMessage(service.AccountsService.RouteListRequest(++requestId, ACCOUNT_ID.ToString()));

            // Updates route list. Setup data for updating route list.
            AccountRouteRecord[] routesToSet = new AccountRouteRecord[]
            {
                FillMessageHelper.FillAccountRouteRecord(routeCode: ROUTE_CODE1, priority: 1),
                FillMessageHelper.FillAccountRouteRecord(routeCode: ROUTE_CODE2, priority: 2),
            };
            string[] routeCodesToRemove = { ROUTE_CODE3.ToString(), ROUTE_CODE4.ToString() };

            // Updates route list.
            printServerMessage(service.AccountsService.UpdateAccountRouteList(++requestId, ACCOUNT_ID, routesToSet, routeCodesToRemove));

            PositionAndTradeLimits positions = FillMessageHelper.FillPositionAndTradeLimits(
                commodityPosistionLimit: FillMessageHelper.FillLimitValue((uint)LimitMode.LIMITED, 1),
                instrumetnPositionLimit: FillMessageHelper.FillLimitValue((uint)LimitMode.LIMITED, 2),
                contractPositonLimit: FillMessageHelper.FillLimitValue((uint)LimitMode.LIMITED, 4),
                tradeSizeLimit: FillMessageHelper.FillLimitValue((uint)LimitMode.LIMITED, 2),
                tradePriceLimit: FillMessageHelper.FillLimitValue((uint)LimitMode.LIMITED, 3),
                tradePriceLimitPercent: FillMessageHelper.FillLimitValueDouble((uint)LimitMode.LIMITED, 4.23));
            MarketLimitsRecord allMarketLimits = FillMessageHelper.FillMarketLimitsRecord(
                allowedToTrade: false,
                marginMultiplier: 1,
                positionsAndTradeLimits: positions);

            printServerMessage(service.AccountsService.AccountTypeRequest(++requestId));

            // Creates account. Setup data.
            MarketLimits limits = FillMessageHelper.FillMarketLimits(allMarketLimits);
            printServerMessage(service.AccountsService.UpdateAccountMarketLimits(++requestId, ACCOUNT_ID, limits));

            Account account = FillMessageHelper.FillAccount(name: NEW_ACCOUNT_NAME,
                brkerageAccountNumber: BROKERAGE_ACCOUNT_NUMBER,
                accountClass: DEFAULT_CLASS_ID,
                customerId: CUSTOMER_ID,
                accountTypeId: DEFAULT_ACCOUNT_TYPE,
                subClass: DEFAULT_SUB_CLASS,
                salesSeriesId: SALES_SERIES_ID);

            // Creates account.
            printServerMessage(service.AccountsService.CreateAccount(++requestId, account));

            // Restore removed account.
            printServerMessage(service.AccountsService.RestoreAccount(++requestId, REMOVED_ACCOUNT_ID));

            // Gets type overrides for account.
            printServerMessage(service.AccountsService.AccountTypeOverridesRequest(++requestId, ACCOUNT_ID));

            // Update account type overrides. Setup data.
            AccountTypeOverride[] overridesToSet = new AccountTypeOverride[]
            {
                FillMessageHelper.FillAccountTypeOverride(OVERRIDES_EXCHANGE_KEY, OVERRIDES_LOGIN_ID, OVERRIDES_TYPE_ID, OVERRIDES_ORIGIN_ID)
            };
            Tuple overrideToRemove = FillMessageHelper.FillTuple(OVERRIDES_LOGIN_ID, OVERRIDES_EXCHANGE_KEY);
            Tuple[] overrideToRemoveList = new Tuple[] { overrideToRemove };

            // Update account type overrides. 
            printServerMessage(service.AccountsService.UpdateAccountTypeOverrideList(++requestId, ACCOUNT_ID, overridesToSet, overrideToRemoveList));

            // Gets account cluster.
            printServerMessage(service.AccountsService.AccountClusterRequest(++requestId, ACCOUNT_CLUSTER_ID));

            // Creates account cluster.
            printServerMessage(service.AccountsService.CreateAccountClusterRequest(++requestId, NEW_CLUSTER_NAME, BROKERAGE_ID));

            // Updates account cluster price offset.
            AccountClusterPriceOffset[] offsetsToSet = new AccountClusterPriceOffset[]
            {
                FillMessageHelper.FillAccountClusterPriceOffset(COMMODITY1_ID, INSTRUMENT_TYPE1_ID, priceOffcetTicks: 10),
                FillMessageHelper.FillAccountClusterPriceOffset(COMMODITY2_ID, INSTRUMENT_TYPE2_ID, priceOffcetTicks: 20)
            };
            AccountClusterPriceOffset[] offsetsToRemove = new AccountClusterPriceOffset[]
            {
                FillMessageHelper.FillAccountClusterPriceOffset(COMMODITY3_ID, INSTRUMENT_TYPE3_ID),
            };
            printServerMessage(service.AccountsService.UpdateAccountClusterPriceOffsets(++requestId, ACCOUNT_CLUSTER_ID, offsetsToSet, offsetsToRemove));

            //Updates Account Cluster to make it Removed.
            AccountCluster accountCluster = FillMessageHelper.FillAccountCluster(ACCOUNT_CLUSTER_ID, BROKERAGE_ID, NEW_CLUSTER_NAME, isRemoved: true);
            printServerMessage(service.AccountsService.UpdateAccountCluster(++requestId, accountCluster));

        }

        private static void executeOrderRelatedRequests(CmsApiService service, ref uint requestId)
        {
            // Please fill the following variables with your values. This fields are required for client messages.
            const int ACCOUNT_ID = 0;
            const string ORDER_ID = "";
            const string CHAIN_ORDER_ID = "";
            const string ROOT_CHAIN_ORDER_ID = "";
            const string COMPOUND_TREE_ID = "";
            const string TRADE_LOCATION_ID = "";
            const string USER_ID = "";
            const string SEARCH_TEXT = "CmsApiDemo";

            SearchOption option1 = FillMessageHelper.FillSearchOption(SEARCH_TEXT, (uint)OrderSearchRequest.Types.SearchCriteria.USER_NAME,
                (uint)SearchOption.Types.MatchingRule.CONTAINS);

            List<SearchOption> searchOptions = new List<SearchOption>() { option1 };
            // Searches order.
            printServerMessage(service.OrdersService.OrderSearchRequest(++requestId, searchOptions, null, null, null));

            // Gets order details.
            printServerMessage(service.OrdersService.OrderDetailsRequest(++requestId, ACCOUNT_ID, ORDER_ID));

            // Adds fill.
            printServerMessage(service.OrdersService.AddFill(++requestId, ACCOUNT_ID, ORDER_ID, 5.9, 1, null, true));

            // Creates external order.
            printServerMessage(service.OrdersService.CreateExternalOrder(++requestId, ACCOUNT_ID, USER_ID, "F.US.EP.M13", 5.9, 1, null, null, 1, 1, null));

            // Gets requests tree with related order chains for synthetic or synthetic strategy orders.
            printServerMessage(service.OrdersService.RelatedOrderTreeRequest(++requestId, CHAIN_ORDER_ID, TRADE_LOCATION_ID));

            // Gets Compound orders tree.
            printServerMessage(service.OrdersService.CompoundOrderTreeRequest(++requestId, COMPOUND_TREE_ID, TRADE_LOCATION_ID));

            // Gets order strategy tree, describing synthetic strategy.
            printServerMessage(service.OrdersService.SyntheticStrategyTreeRequest(++requestId, ROOT_CHAIN_ORDER_ID, TRADE_LOCATION_ID));

            // Cancels order using Trade Location parameter. (Trade Location is mutually exclusive with AccountId).
            printServerMessage(service.OrdersService.CancelOrder(++requestId, CHAIN_ORDER_ID, null, TRADE_LOCATION_ID));
            // Cancels order using Account Id parameter.
            printServerMessage(service.OrdersService.CancelOrder(++requestId, CHAIN_ORDER_ID, ACCOUNT_ID));

            // Reflects order as canceled. (Trade Location is mutually exclusive with AccountId).
            printServerMessage(service.OrdersService.ReflectAsCanceledOrder(++requestId, CHAIN_ORDER_ID, null, TRADE_LOCATION_ID));
            printServerMessage(service.OrdersService.ReflectAsCanceledOrder(++requestId, CHAIN_ORDER_ID, ACCOUNT_ID));

            // Cancels compound order tree. (Trade Location is mutually exclusive with AccountId).
            printServerMessage(service.OrdersService.CancelCompoundOrderTree(++requestId, COMPOUND_TREE_ID, null, TRADE_LOCATION_ID));
            printServerMessage(service.OrdersService.CancelCompoundOrderTree(++requestId, COMPOUND_TREE_ID, ACCOUNT_ID));
        }

        private static void calculateEffectiveMarketLimits(CmsApiService service, ref uint requestId)
        {
            // Please fill the following variables with your values. This fields are required for client messages.
            const int ACCOUNT_ID = 0;
            const string SYMBOL = "";
            const string INSTRUMENT_TYPE = ""; // e.g. "Future".

            // Make requests to CMS API.
            ServiceResult exchangeGroupsResultss = service.AccountsService.AccountAvailableExchangeGroupsRequest(++requestId, ACCOUNT_ID).Result;
            ServiceResult marketLimitsResult = service.AccountsService.AccountMarketLimitsRequest(++requestId, ACCOUNT_ID).Result;

            AccountExchangeGroupsResult exchangeGroupsResult = exchangeGroupsResultss.ServerMessage.TradeRoutingResultList[0]
                .AccountScopeResult.AccountExchangeGroupsResult;

            // Find instrument type id by name.
            InstrumentType instrumentType = exchangeGroupsResult.InstrumentTypeList.FirstOrDefault(x => x.Name.Text_ == INSTRUMENT_TYPE);
            int? instrumentTypeId = instrumentType?.Id;

            if (instrumentTypeId == null)
            {
                Console.WriteLine("Instrument type '{0}' doesn't exist, choose one of next:", INSTRUMENT_TYPE);
                foreach (InstrumentType type in exchangeGroupsResult.InstrumentTypeList)
                {
                    Console.WriteLine("- {0}", type.Name.Text_);
                }

                Console.WriteLine();

                return;
            }

            // Calculate limits for symbol.
            MarketLimits marketLimits = marketLimitsResult.ServerMessage.TradeRoutingResultList[0]
                .AccountScopeResult.AccountMarketLimitsResult.AccountMarketLimits;

            IList<ExchangeGroup> exchangeGroups = exchangeGroupsResult.ExchangeGroupList;

            MarketLimitsCalculator.Limits limits = MarketLimitsCalculator.CalculateEffectiveLimits(marketLimits, exchangeGroups, SYMBOL, (int)instrumentTypeId);

            if (limits == null)
            {
                Console.WriteLine("{0} {1} was not found.", INSTRUMENT_TYPE, SYMBOL);
                return;
            }

            // Print limits which are acceptable to specified instrument type.
            Console.WriteLine("Effective Market Limits for {0} {1} for account {2}:", INSTRUMENT_TYPE, SYMBOL, ACCOUNT_ID);
            Console.WriteLine("- Allowed To Trade: {0}", limits.AllowedToTrade);

            printLimitValue("- Commodity Position Limit (commodity level)", limits.CommodityPositionLimit);

            if (instrumentType.AllowedLimitsList.Contains((int)InstrumentType.Types.MarketLimitType.INSTRUMENT_POSITION_LIMIT))
            {
                printLimitValue("- Instrument Position Limit", limits.InstrumentPositionLimit);
            }

            if (instrumentType.AllowedLimitsList.Contains((int)InstrumentType.Types.MarketLimitType.CONTRACT_POSITION_LIMIT))
            {
                printLimitValue("- Contract Position Limit", limits.ContractPositionLimit);
            }

            if (instrumentType.AllowedLimitsList.Contains((int)InstrumentType.Types.MarketLimitType.TRADE_SIZE_LIMIT))
            {
                printLimitValue("- Trade Size Limit", limits.TradeSizeLimit);
            }

            if (instrumentType.AllowedLimitsList.Contains((int)InstrumentType.Types.MarketLimitType.TRADE_PRICE_LIMIT))
            {
                printLimitValue("- Trade Price Limit (ticks)", limits.TradePriceLimitTicks);
                printLimitValueDecimal("- Trade Price Limit (percent)", limits.TradePriceLimitPercent);
            }

            Console.WriteLine("- Margin Multiplier (exchange group level): {0:0.000}", limits.MarginMultiplier);

            Console.WriteLine();
        }

        private static void printServerMessage(Task<ServiceResult> serviceResult)
        {
            Console.WriteLine(serviceResult.Result.ServerMessage);
        }

        private static void printLimitValue(string prefix, LimitValue limit)
        {
            if (limit == null)
            {
                Console.WriteLine("{0}: not set", prefix);
                return;
            }

            LimitMode mode = (LimitMode)limit.Mode;
            Console.WriteLine("{0}: {1} ({2})", prefix, mode.ToString().ToLower(), limit.Value);
            foreach (ExpirationLimit expirationLimit in limit.ExpirationLimitList)
            {
                Console.WriteLine("\t days: {0:3} limit: {1}", expirationLimit.DaysBeforeExpiration, expirationLimit.Value);
            }
        }

        private static void printLimitValueDecimal(string prefix, LimitValueDouble limit)
        {
            if (limit == null)
            {
                Console.WriteLine("{0}: not set", prefix);
                return;
            }

            LimitMode mode = (LimitMode)limit.Mode;
            Console.WriteLine("{0}: {1} ({2})", prefix, mode.ToString().ToLower(), limit.Value);
        }
    }
}