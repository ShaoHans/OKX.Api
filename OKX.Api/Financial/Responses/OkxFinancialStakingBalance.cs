﻿namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial Staking Balance
/// </summary>
public record OkxFinancialStakingBalance
{
    /// <summary>
    /// Currency, e.g. BETH
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Currency amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Latest interest accrual
    /// </summary>
    [JsonProperty("latestInterestAccrual")]
    public decimal LatestInterestAccrual { get; set; }

    /// <summary>
    /// Total interest accrual
    /// </summary>
    [JsonProperty("totalInterestAccrual")]
    public decimal TotalInterestAccrual { get; set; }

    /// <summary>
    /// Query data time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long? Timestamp { get; set; }

    /// <summary>
    /// Query data time
    /// </summary>
    [JsonIgnore]
    public DateTime? Time => Timestamp?.ConvertFromMilliseconds();
}
