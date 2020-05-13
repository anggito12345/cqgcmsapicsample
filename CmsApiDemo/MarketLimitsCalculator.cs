using System.Collections.Generic;
using System.Linq;
using CmsApiSamples.Protocol.CMSApi;

namespace CmsApiDemo
{
    internal static class MarketLimitsCalculator
    {
        /// <summary>
        /// Same data you can see in CAST on Accounts - Market Limits.
        /// </summary>
        public class Limits
        {
            public bool? AllowedToTrade { get; set; }
            public double? MarginMultiplier { get; set; }
            public LimitValue CommodityPositionLimit { get; set; }
            public LimitValue InstrumentPositionLimit { get; set; }
            public LimitValue ContractPositionLimit { get; set; }
            public LimitValue TradeSizeLimit { get; set; }
            public LimitValue TradePriceLimitTicks { get; set; }
            public LimitValueDouble TradePriceLimitPercent { get; set; }
        }

        public static Limits CalculateEffectiveLimits(MarketLimits marketLimits, IList<ExchangeGroup> exchangeGroups, string symbol, int instrumentTypeId)
        {
            // Find which tradable commodity, fungible commodity and exchange group specified symbol belongs to.
            // Search in all commodities because MarketLimits records are not returned for a commodity
            // if everything is set to default on its level.
            var query =
                from exchangeGroup in exchangeGroups
                from fungibleCommodity in exchangeGroup.FungibleCommodityList
                from tradableCommodity in fungibleCommodity.TradableCommodityList
                where tradableCommodity.Symbol == symbol
                where fungibleCommodity.InstrumentTypeIdList.Contains(instrumentTypeId)
                select new
                {
                    ExchangeGroup = exchangeGroup,
                    FungibleCommodity = fungibleCommodity,
                    TradableCommodity = tradableCommodity
                };

            var path = query.FirstOrDefault();

            // No such symbol is available under passed exchange groups.
            if (path == null)
            {
                return null;
            }

            // Find corresponding market limits entries for found exchange group, fungible commodity and instrument.
            // Note that ?. (elvis operator) is used to propagate null (only non-default Market Limits are returned).
            MarketLimitsRecord categoryMarketLimits = path.FungibleCommodity.IsUs
                ? marketLimits.UsMarketLimits
                : marketLimits.NonUsMarketLimits;

            ExchangeMarketLimits exchangeMarketLimits = marketLimits
                .ExchangeMarketLimitsList.FirstOrDefault(x => x.ExchangeGroupId == path.ExchangeGroup.Id);

            CommodityMarketLimits commodityMarketLimits = exchangeMarketLimits
                ?.CommodityMarketLimitsList.FirstOrDefault(x => x.FungibleCommodityId == path.FungibleCommodity.Id);

            InstrumentMarketLimits instrumentMarketLimits = commodityMarketLimits
                ?.InstrumentMarketLimitsList.FirstOrDefault(x => x.InstrumentTypeId == instrumentTypeId);

            // Construct Limits from market limits entries.
            Limits allLimits = createLimitsFrom(marketLimits.AllMarketLimits);
            Limits categoryLimits = createLimitsFrom(categoryMarketLimits);
            Limits exchangeGroupLimits = createLimitsFrom(exchangeMarketLimits?.DefaultMarketLimits);
            Limits commodityLimits = createLimitsFrom(commodityMarketLimits?.PositionAndTradeLimits);
            Limits instrumentLimits = createLimitsFrom(instrumentMarketLimits?.PositionAndTradeLimits);

            // Merge all limits making more specific limits override more common ones.
            Limits effectiveLimits = mergeLimits(allLimits, categoryLimits, exchangeGroupLimits, commodityLimits, instrumentLimits);

            // If commodity is explicitly marked is tradeable override value.
            bool isExplicitlyTradeable = commodityMarketLimits?.TradableCommodityIdList.Contains(path.TradableCommodity.Id) ?? false;
            if (isExplicitlyTradeable)
            {
                effectiveLimits.AllowedToTrade = true;
            }

            return effectiveLimits;
        }

        private static Limits createLimitsFrom(MarketLimitsRecord record)
        {
            if (record == null)
            {
                return null;
            }

            return mergeLimits(createLimitsFrom(record.PositionAndTradeLimits), new Limits
            {
                AllowedToTrade = record.HasAllowedToTrade ? record.AllowedToTrade : (bool?)null,
                MarginMultiplier = record.HasMarginMultiplier ? record.MarginMultiplier : (double?)null
            });
        }

        private static Limits createLimitsFrom(PositionAndTradeLimits limits)
        {
            return limits == null
                ? null
                : new Limits
                {
                    CommodityPositionLimit = limits.HasCommodityPositionLimit ? limits.CommodityPositionLimit : null,
                    InstrumentPositionLimit = limits.HasInstrumentPositionLimit ? limits.InstrumentPositionLimit : null,
                    ContractPositionLimit = limits.HasContractPositionLimit ? limits.ContractPositionLimit : null,
                    TradeSizeLimit = limits.HasTradeSizeLimit ? limits.TradeSizeLimit : null,
                    TradePriceLimitTicks = limits.HasTradePriceLimitTicks ? limits.TradePriceLimitTicks : null,
                    TradePriceLimitPercent = limits.HasTradePriceLimitPercent ? limits.TradePriceLimitPercent : null
                };
        }

        /// <summary>
        /// Merge list of limits.
        /// </summary>
        private static Limits mergeLimits(Limits @default, params Limits[] overrides)
        {
            return overrides.Aggregate(@default, (current, @override) =>
            {
                if (current == null)
                {
                    return @override;
                }

                return new Limits
                {
                    AllowedToTrade = @override?.AllowedToTrade ?? current.AllowedToTrade,
                    MarginMultiplier = @override?.MarginMultiplier ?? current.MarginMultiplier,
                    CommodityPositionLimit = @override?.CommodityPositionLimit ?? current.CommodityPositionLimit,
                    InstrumentPositionLimit = @override?.InstrumentPositionLimit ?? current.InstrumentPositionLimit,
                    ContractPositionLimit = @override?.ContractPositionLimit ?? current.ContractPositionLimit,
                    TradeSizeLimit = @override?.TradeSizeLimit ?? current.TradeSizeLimit,
                    TradePriceLimitTicks = @override?.TradePriceLimitTicks ?? current.TradePriceLimitTicks,
                    TradePriceLimitPercent = @override?.TradePriceLimitPercent ?? current.TradePriceLimitPercent
                };
            });
        }
    }
}