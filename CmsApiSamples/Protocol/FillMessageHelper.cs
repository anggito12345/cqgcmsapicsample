using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsApiSamples.Protocol.CMSApi;
using CmsApiSamples.Protocol.Extensions;

namespace CmsApiSamples.Protocol
{
    /// <summary>
    /// Helper class to fill messages.
    /// </summary>
    public sealed class FillMessageHelper
    {
        public static AccountSettings FillAccountSettings(int accountId,
            bool? isGiveUp = null, uint? statementOption = null, bool? zeroBalance = null,
            bool? reconciliation = null, string giveupBrokerageId = null,
            string tradingTimeFrom = null, string tradingTimeTo = null, string tradingTimeZone = null,
            bool? isInstruct = null,
            bool? liquidationOrdersOutsideTradingTime = null,
            bool? externallyProvidedExecution = null,
            string[] modesArray = null)
        {
            AccountSettings settings = Protocol.CMSApi.AccountSettings.CreateBuilder()
               .SetAccountId(accountId)
               .SetIfNotNull(isGiveUp, b => b.SetIsGiveup)
               .SetIfNotNull(giveupBrokerageId, b => b.SetGiveupBrokerageId)
               .SetIfNotNull(statementOption, b => b.SetStatementOption)
               .SetIfNotNull(zeroBalance, b => b.SetZeroBalance)
               .SetIfNotNull(reconciliation, b => b.SetReconciliation)
               .SetIfNotNull(isInstruct, b => b.SetIsInstruct)
               .SetTradingTimeFrom(tradingTimeFrom)
               .SetTradingTimeTo(tradingTimeTo)
               .SetTradingTimeZone(tradingTimeZone)
               .SetIfNotNull(liquidationOrdersOutsideTradingTime, b => b.SetLiquidationOrdersOutsideTradingTime)
               .SetIfNotNull(externallyProvidedExecution, b => b.SetExternallyProvidedExecution)
               .AddRangeModesAccountGroups(modesArray)
               .Build();
            return settings;
        }

        public static TradePriceLimit FillTradePiceLimit(uint mode, LimitValue limit)
        {
            TradePriceLimit tradePriceLimit = Protocol.CMSApi.TradePriceLimit.CreateBuilder()
                .SetMode(mode)
                .SetValue(limit)
                .Build();
            return tradePriceLimit;
        }

        public static LimitValue FillLimitValue(uint mode, int value)
        {
            LimitValue limit = Protocol.CMSApi.LimitValue.CreateBuilder()
                .SetMode(mode)
                .SetValue(value)
                .Build();
            return limit;
        }

        public static LimitValueDouble FillLimitValueDouble(uint mode, double value)
        {
            LimitValueDouble limit = Protocol.CMSApi.LimitValueDouble.CreateBuilder()
                .SetMode(mode)
                .SetValue(value)
                .Build();
            return limit;
        }

        public static MarginSubsystemParameters FillMarginSubsystemParameters(double? allowedMarginCredit = null,
            bool? crossMargining = null,
            uint? includeOtePp = null,
            uint? includeNovPp = null,
            uint? includeUplLl = null,
            uint? includeOteLl = null,
            bool? checkNegativeBalance = null,
            bool? useTheoPrices = null,
            int? theoTicks = null,
            bool? useBbaOte = null,
            bool? useBbaNovUpl = null,
            bool? adjustPriceByNetchange = null,
            bool? useBrokerageMarginsOnly = null,
            double? marginMultiplier = null)
        {
            MarginSubsystemParameters parameters = Protocol.CMSApi.MarginSubsystemParameters.CreateBuilder()
                .SetIfNotNull(allowedMarginCredit, b => b.SetAllowableMarginCredit)
                .SetIfNotNull(crossMargining, b => b.SetCrossMargining)
                .SetIfNotNull(includeOtePp, b => b.SetIncludeOtePp)
                .SetIfNotNull(includeNovPp, b => b.SetIncludeNovPp)
                .SetIfNotNull(includeUplLl, b => b.SetIncludeUplLl)
                .SetIfNotNull(includeOteLl, b => b.SetIncludeOteLl)
                .SetIfNotNull(checkNegativeBalance, b => b.SetCheckNegativeBalance)
                .SetIfNotNull(useTheoPrices, b => b.SetUseTheoPrices)
                .SetIfNotNull(theoTicks, b => b.SetTheoTicks)
                .SetIfNotNull(useBbaOte, b => b.SetUseBbaOte)
                .SetIfNotNull(useBbaNovUpl, b => b.SetUseBbaNovUpl)
                .SetIfNotNull(adjustPriceByNetchange, b => b.SetAdjustPriceByNetchange)
                .SetIfNotNull(useBrokerageMarginsOnly, b => b.SetUseBrokerageMarginsOnly)
                .SetIfNotNull(marginMultiplier, b => b.SetMarginMultiplier)
                .Build();
            return parameters;
        }

        public static AccountRiskParameters FillAccountRiskParameters(int accountId,
            bool? liquidationOnly = null,
            bool? allowFutures = null,
            uint? allowOptions = null,
            bool? enforceTradeSizeLimit = null,
            int? tradeSizeLimit = null,
            bool? enforceTradeMarginLimit = null,
            double? tradeMarginLimit = null,
            bool? enforceTradePriceLimit = null,
            TradePriceLimit tradePriceLimit = null,
            bool? enforceCommodityPositionLimit = null,
            LimitValue commodityPositionLimitValue = null,
            bool? enforceContractPositionLimit = null,
            LimitValue contractPositionLimitValue = null,
            bool? enforceMarginSubsystemParams = null,
            MarginSubsystemParameters marginSubsystemParameters = null,
            bool? enforceDailyLossLimit = null,
            LossLimit dailyLossLimit = null,
            bool? enforceDeltaDailyLossLimit = null,
            LossLimit deltaDailyLossLimit = null,
            int? maxOrderRate = null,
            bool? rejectRiskyMarketOrders = null)
        {
            AccountRiskParameters parameters = Protocol.CMSApi.AccountRiskParameters.CreateBuilder()
                .SetAccountId(accountId)
                .SetIfNotNull(liquidationOnly, b => b.SetLiquidationOnly)
                .SetIfNotNull(allowFutures, b => b.SetAllowFutures)
                .SetIfNotNull(allowOptions, b => b.SetAllowOptions)
                .SetIfNotNull(enforceTradeSizeLimit, b => b.SetEnforceTradeSizeLimit)
                .SetIfNotNull(tradeSizeLimit, b => b.SetTradeSizeLimit)
                .SetIfNotNull(enforceTradeMarginLimit, b => b.SetEnforceTradeMarginLimit)
                .SetIfNotNull(tradeMarginLimit, b => b.SetTradeMarginLimit)
                .SetIfNotNull(enforceTradePriceLimit, b => b.SetEnforceTradePriceLimitTicks)
                .SetIfNotNull(tradePriceLimit, b => b.SetTradePriceLimitTicks)
                .SetIfNotNull(enforceCommodityPositionLimit, b => b.SetEnforceCommodityPositionLimit)
                .SetIfNotNull(commodityPositionLimitValue, b => b.SetCommodityPositionLimit)
                .SetIfNotNull(enforceContractPositionLimit, b => b.SetEnforceContractPositionLimit)
                .SetIfNotNull(contractPositionLimitValue, b => b.SetContractPositionLimit)
                .SetIfNotNull(enforceMarginSubsystemParams, b => b.SetEnforceMarginSubsystemParameters)
                .SetIfNotNull(marginSubsystemParameters, b => b.SetMarginSubsystemParameters)
                .SetIfNotNull(enforceDailyLossLimit, b => b.SetEnforceDailyLossLimit)
                .SetIfNotNull(dailyLossLimit, b => b.SetDailyLossLimit)
                .SetIfNotNull(enforceDeltaDailyLossLimit, b => b.SetEnforceDeltaDailyLossLimit)
                .SetIfNotNull(deltaDailyLossLimit, b => b.SetDeltaDailyLossLimit)
                .SetIfNotNull(maxOrderRate, b => b.SetMaximumOrderRate)
                .SetIfNotNull(rejectRiskyMarketOrders, b => b.SetRejectRiskyMarketOrders)
                .Build();
            return parameters;
        }

        public static AccountRouteRecord FillAccountRouteRecord(int routeCode, int? priority = null, int? omnibusAccountId = null, AccountRouteAttribute[] attributes = null)
        {
            AccountRouteRecord routeRecord = Protocol.CMSApi.AccountRouteRecord.CreateBuilder()
                .SetRouteCode(routeCode)
                .SetIfNotNull(priority, b => b.SetPriority)
                .SetIfNotNull(omnibusAccountId, b => b.SetOmnibusAccountId)
                .SetIfNotNull(attributes, b => b.AddRangeAttributes)
                .Build();
            return routeRecord;
        }

        public static AccountUserLink FillAccountUserLink(int accountId, string userId, bool? isViewOnly = null, bool? isForceCare = null)
        {
            AccountUserLink accountUsrLink = Protocol.CMSApi.AccountUserLink.CreateBuilder()
                .SetAccountId(accountId)
                .SetUserId(userId)
                .SetIfNotNull(isViewOnly, b => b.SetIsViewOnly)
                .SetIfNotNull(isForceCare, b => b.SetIsForceCare)
                .Build();
            return accountUsrLink;
        }

        public static PositionAndTradeLimits FillPositionAndTradeLimits(LimitValue commodityPosistionLimit = null,
            LimitValue instrumetnPositionLimit = null,
            LimitValue contractPositonLimit = null,
            LimitValue tradeSizeLimit = null,
            LimitValue tradePriceLimit = null,
            LimitValueDouble tradePriceLimitPercent = null)
        {
            PositionAndTradeLimits positoAndTradeLimits = Protocol.CMSApi.PositionAndTradeLimits.CreateBuilder()
                .SetIfNotNull(commodityPosistionLimit, b => b.SetCommodityPositionLimit)
                .SetIfNotNull(instrumetnPositionLimit, b => b.SetInstrumentPositionLimit)
                .SetIfNotNull(contractPositonLimit, b => b.SetContractPositionLimit)
                .SetIfNotNull(tradeSizeLimit, b => b.SetTradeSizeLimit)
                .SetIfNotNull(tradePriceLimit, b => b.SetTradePriceLimitTicks)
                .SetIfNotNull(tradePriceLimitPercent, b => b.SetTradePriceLimitPercent)
               .Build();
            return positoAndTradeLimits;
        }

        public static MarketLimitsRecord FillMarketLimitsRecord(bool? allowedToTrade = null, double? marginMultiplier = null,
            PositionAndTradeLimits positionsAndTradeLimits = null)
        {
            MarketLimitsRecord marketLimitsRecord = Protocol.CMSApi.MarketLimitsRecord.CreateBuilder()
                .SetIfNotNull(allowedToTrade, b => b.SetAllowedToTrade)
                .SetIfNotNull(marginMultiplier, b => b.SetMarginMultiplier)
                .SetIfNotNull(positionsAndTradeLimits, b => b.SetPositionAndTradeLimits)
                .Build();
            return marketLimitsRecord;
        }

        public static MarketLimits FillMarketLimits(MarketLimitsRecord allMarketLimitsRecord,
            MarketLimitsRecord usMarketLimitsRecords = null,
            MarketLimitsRecord nonUSMarketLimitsRecords = null,
            ExchangeMarketLimits[] exchangeMarketLimits = null)
        {
            MarketLimits marketLimits = Protocol.CMSApi.MarketLimits.CreateBuilder()
                .SetIfNotNull(allMarketLimitsRecord, b => b.SetAllMarketLimits)
                .SetIfNotNull(usMarketLimitsRecords, b => b.SetUsMarketLimits)
                .SetIfNotNull(nonUSMarketLimitsRecords, b => b.SetNonUsMarketLimits)
                .SetIfNotNull(exchangeMarketLimits, b => b.AddRangeExchangeMarketLimits)
                .Build();
            return marketLimits;
        }

        public static Account FillAccount(int? accountId = null, string name = null, string brkerageAccountNumber = null,
            uint? accountClass = null, string customerId = null,
            string accountTypeId = null, string salesSeriesId = null, string brokerageId = null,
            uint? subClass = null, string riskServerId = null, string accountClusterId = null)
        {
            Account account = Protocol.CMSApi.Account.CreateBuilder()
               .SetIfNotNull(accountId, b => b.SetId)
               .SetIfNotNull(name, b => b.SetName)
               .SetIfNotNull(brkerageAccountNumber, b => b.SetBrokerageAccountNumber)
               .SetIfNotNull(accountClass, b => b.SetClass)
               .SetIfNotNull(customerId, b => b.SetCustomerId)
               .SetIfNotNull(accountTypeId, b => b.SetAccountTypeId)
               .SetIfNotNull(salesSeriesId, b => b.SetSalesSeriesId)
               .SetIfNotNull(brokerageId, b => b.SetBrokerageId)
               .SetIfNotNull(subClass, b => b.SetSubClass)
               .SetIfNotNull(riskServerId, b => b.SetRiskServerInstanceId)
               .SetIfNotNull(accountClusterId, b => b.SetAccountClusterId)
               .Build();
            return account;
        }

        public static AccountTypeOverride FillAccountTypeOverride(string exchangeKey = null, string loginId = null, string typeId = null, string originId = null)
        {
            AccountTypeOverride accountTypeOverride = Protocol.CMSApi.AccountTypeOverride.CreateBuilder()
              .SetIfNotNull(exchangeKey, b => b.SetExchangeKey)
              .SetIfNotNull(loginId, b => b.SetLoginId)
              .SetIfNotNull(typeId, b => b.SetTypeId)
              .SetIfNotNull(originId, b => b.SetOriginId)
              .Build();
            return accountTypeOverride;
        }

        public static AccountCluster FillAccountCluster(string clusterId = null, string brokerageId = null, string name = null, bool? isRemoved = null)
        {
            AccountCluster accountCluster = Protocol.CMSApi.AccountCluster.CreateBuilder()
              .SetIfNotNull(clusterId, b => b.SetId)
              .SetIfNotNull(brokerageId, b => b.SetBrokerageId)
              .SetIfNotNull(name, b => b.SetName)
              .SetIfNotNull(isRemoved, b => b.SetIsRemoved)
              .Build();
            return accountCluster;
        }

        public static AccountClusterPriceOffset FillAccountClusterPriceOffset(string commodityId,
            int? intstrumentTypeId,
            int? priceOffcetTicks = null,
            int? hedgeOffsetTicks = null,
            uint[] hedgeExecInstruction = null,
            uint? icebergVisibleQtyPercent = null,
            uint[] clearedFields = null)
        {
            AccountClusterPriceOffset accountClusterPriceOffset = Protocol.CMSApi.AccountClusterPriceOffset.CreateBuilder()
              .SetIfNotNull(commodityId, b => b.SetCommodityId)
              .SetIfNotNull(intstrumentTypeId, b => b.SetInstrumentTypeId)
              .SetIfNotNull(priceOffcetTicks, b => b.SetPriceOffsetTicks)
              .SetIfNotNull(hedgeOffsetTicks, b => b.SetHedgeOffsetTicks)
              .SetIfNotNull(hedgeExecInstruction, b => b.AddRangeHedgeExecInstruction)
              .SetIfNotNull(icebergVisibleQtyPercent, b => b.SetIcebergVisibleQtyPercent)
              .SetIfNotNull(clearedFields, b => b.AddRangeClearedFields)
              .Build();
            return accountClusterPriceOffset;
        }

        public static Protocol.CMSApi.Tuple FillTuple(string first, string second, string third = null)
        {
            Protocol.CMSApi.Tuple tuple = Protocol.CMSApi.Tuple.CreateBuilder()
                .SetIfNotNull(first, b => b.SetFirst)
                .SetIfNotNull(second, b => b.SetSecond)
                .SetIfNotNull(third, b => b.SetThird)
                .Build();
            return tuple;
        }

        public static SearchOption FillSearchOption(string text, uint? criteria, uint? matchingRule)
        {
            return SearchOption
                .CreateBuilder()
                .SetIfNotNull(text, b => b.SetText)
                .SetIfNotNull(criteria, b => b.AddCriteria)
                .SetIfNotNull(matchingRule, b => b.SetMatchingRule)
                .Build();
        }

    }
}
