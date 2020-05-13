# Example of calculating effective `allowed_to_trade` for a TradableCommodity

Next exchange group structure was returned with
  TradeRoutingResult.AccountScopeResult.AccountExchangeGroupsResult:

    ExchangeGroup{111}
      FungibleCommodity{222}
        TradableCommodity{888}
      FungibleCommodity{333}
        TradableCommodity{999}

Next MarketLimits was returned with
  TradeRoutingResult.AccountSearchResult.AccountMarketLimitsResult:

    {
      "all_market_limits": { "allowed_to_trade": false, ... },
      "us_market_limits": { "allowed_to_trade": false, ... },
      "exchange_market_limits": {
        "0": {
          "exchange_group_id": 111,
          "default_market_limits": {
            "allowed_to_trade": false,
            "position_and_trade_limits": { ... },
          },
          "commodity_market_limits": {
            "0": {
              // TradableCommodity{888} belongs to this FungibleCommodity.
              // But it's not mentioned in tradable_commodity_id list.
              // Go up in hierarchy.
              // Is everything under ExchangeGroup#111 is allowed to trade?
              // allowed_to_trade = false => Nothing is allowed to trade if it's not explicitly allowed.
              // TradableCommodity{888} is not allowed to trade.
              "fungible_commodity_id": 222,
              "position_and_trade_limits": { ... }
              // "tradable_commodity_id" is missing
            },
            "1": {
              "fungible_commodity_id": 333,
              "tradable_commodity_id": {
                // TradableCommodity{999} is explicitly allowed to trade.
                "0": "999"
              }
      ...
    }
