﻿namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial Staking APY History
/// </summary>
public record OkxFinancialStakingApyHistory
{
    /// <summary>
    /// APY(Annual percentage yield), e.g. 0.01 represents 1%
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Data time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
