﻿namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Side
/// </summary>
public enum OkxTradeOrderSide : byte
{
    /// <summary>
    /// Buy
    /// </summary>
    [Map("buy")]
    Buy = 1,

    /// <summary>
    /// Sell
    /// </summary>
    [Map("sell")]
    Sell = 2,
}