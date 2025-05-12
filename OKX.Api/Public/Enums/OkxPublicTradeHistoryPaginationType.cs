﻿namespace OKX.Api.Public;

/// <summary>
/// OKX Trade History Pagination Type
/// </summary>
public enum OkxPublicTradeHistoryPaginationType : byte
{
    /// <summary>
    /// TradeId
    /// </summary>
    [Map("1")]
    TradeId = 1,

    /// <summary>
    /// Timestamp
    /// </summary>
    [Map("2")]
    Timestamp = 2,
}