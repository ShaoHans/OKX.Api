﻿namespace OKX.Api.Financial;

/// <summary>
/// OKX Flexible Simple Earn Savings Order
/// </summary>
public record OkxFinancialSimpleEarnSavingsOrder
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Purchase/redemption amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Action type.
    /// purchase: purchase saving shares
    /// redempt: redeem saving shares
    /// </summary>
    [JsonProperty("side")]
    public OkxFinancialSimpleEarnSavingsSide Side { get; set; }

    /// <summary>
    /// Annual purchase rate
    /// Only applicable to purchase saving shares
    /// The interest rate of the new subscription will cover the interest rate of the last subscription
    /// The rate value range is between 1% and 365%
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }
}
