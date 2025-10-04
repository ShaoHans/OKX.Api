﻿namespace OKX.Api.Trade;

/// <summary>
/// Represents a transaction in OKX, a cryptocurrency exchange platform.
/// </summary>
public record OkxTradeTransaction
{
    /// <summary>
    /// Type of the instrument involved in the transaction.
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Unique identifier of the instrument involved in the transaction.
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Unique identifier of the last trade associated with this transaction.
    /// </summary>
    /// <remarks>
    /// For certain categories such as partial_liquidation, full_liquidation, or adl, the value is "0".
    /// </remarks>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Unique identifier of the order associated with this transaction.
    /// </summary>
    /// <remarks>
    /// For block trading, this is always "".
    /// </remarks>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client-supplied unique identifier of the order.
    /// <remarks>
    /// For block trading, this is always "".
    /// </remarks>
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Unique identifier of the bill associated with this transaction.
    /// </summary>
    [JsonProperty("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// Transaction type
    /// </summary>
    [JsonProperty("subType")]
    public OkxAccountBillSubType TransactionType { get; set; }

    /// <summary>
    /// The price at which the last fill occurred.
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal? LastFilledPrice { get; set; }

    /// <summary>
    /// The quantity that was filled in the last trade.
    /// </summary>
    [JsonProperty("fillSz")]
    public decimal? LastFilledQuantity { get; set; }

    /// <summary>
    /// The index price at the moment of trade execution.
    /// </summary>
    /// <remarks>
    /// For cross currency spot pairs (such as BTC-ETH), it returns baseCcy-USDT (BTC-USDT) index price.
    /// </remarks>
    [JsonProperty("fillIdxPx")]
    public decimal? LastFilledIndexPrice { get; set; }

    /// <summary>
    /// Last filled profit and loss, applicable to orders which have a trade and aim to close position. It always is 0 in other conditions
    /// </summary>
    [JsonProperty("fillPnl")]
    public decimal? LastFilledProfitAndLoss { get; set; }

    /// <summary>
    /// Mark volatility when filled
    /// Only applicable to options; return "" for other instrument types
    /// </summary>
    [JsonProperty("fillPxVol")]
    public decimal? LastFilledPriceVolatility { get; set; }

    /// <summary>
    /// Options price when filled, in the unit of USD
    /// Only applicable to options; return "" for other instrument types
    /// </summary>
    [JsonProperty("fillPxUsd")]
    public decimal? LastFilledPriceUsd { get; set; }

    /// <summary>
    /// Mark volatility when filled
    /// Only applicable to options; return "" for other instrument types
    /// </summary>
    [JsonProperty("fillMarkVol")]
    public decimal? LastFilledMarkVolatility { get; set; }

    /// <summary>
    /// Forward price when filled
    /// Only applicable to options; return "" for other instrument types
    /// </summary>
    [JsonProperty("fillFwdPx")]
    public decimal? LastFilledForwardPrice { get; set; }

    /// <summary>
    /// Mark price when filled
    /// Applicable to FUTURES, SWAP, OPTION
    /// </summary>
    [JsonProperty("fillMarkPx")]
    public decimal? LastFilledMarkPrice { get; set; }

    /// <summary>
    /// Specifies whether the order was a buy or sell.
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Specifies the position side of the order (long, short, or net innet mode).
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Indicates whether the order acted as liquidity taker or maker. 
    /// </summary>
    /// <remarks>
    /// Not applicable to system orders such as ADL and liquidation.
    /// </remarks>
    [JsonProperty("execType")]
    public OkxTradeOrderRole OrderFlowType { get; set; }

    /// <summary>
    /// The currency in which the transaction fee is paid.
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; } = string.Empty;

    /// <summary>
    /// The transaction fee.
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Unix timestamp of the transaction's generation time in milliseconds.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// the transaction's generation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Trade time which is the same as fillTime for the order channel.
    /// </summary>
    [JsonProperty("fillTime")]
    public long? FillTimestamp { get; set; }

    /// <summary>
    /// Trade time which is the same as fillTime for the order channel.
    /// </summary>
    [JsonIgnore]
    public DateTime? FillTime => FillTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Fee rate. This field is returned for SPOT and MARGIN only
    /// </summary>
    [JsonProperty("feeRate")]
    public decimal? FeeRate { get; set; }

    /// <summary>
    /// Trade quote currency.
    /// </summary>
    [JsonProperty("tradeQuoteCcy")]
    public string? TradeQuoteCurrency { get; set; }
}
