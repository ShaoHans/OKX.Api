﻿namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Copy Trading Lead Traders Ranks
/// </summary>
public record OkxCopyTradingLeadTradersRanks
{
    /// <summary>
    /// Data version
    /// </summary>
    [JsonProperty("dataVer")]
    public string DataVersion { get; set; } = string.Empty;

    /// <summary>
    /// Total number of pages
    /// </summary>
    [JsonProperty("totalPage")]
    public int TotalPages { get; set; }

    /// <summary>
    /// The rank information of lead traders
    /// </summary>
    [JsonProperty("ranks")]
    public List<OkxCopyTradingLeadTraderRank> Ranks { get; set; } = [];
}

/// <summary>
/// OKX Copy Trading Lead Traders Rank
/// </summary>
public record OkxCopyTradingLeadTraderRank
{
    /// <summary>
    /// assets under management
    /// </summary>
    [JsonProperty("aum")]
    public decimal AUM { get; set; }

    /// <summary>
    /// Current copy state
    /// </summary>
    [JsonProperty("copyState")]
    public OkxCopyTradingState State { get; set; }

    /// <summary>
    /// Maximum number of copy traders
    /// </summary>
    [JsonProperty("maxCopyTraderNum")]
    public int NumberOfCopyTradersMaximum { get; set; }

    /// <summary>
    /// Current number of copy traders
    /// </summary>
    [JsonProperty("copyTraderNum")]
    public int NumberOfCopyTradersCurrent { get; set; }

    /// <summary>
    /// Accumulated number of copy traders
    /// </summary>
    [JsonProperty("accCopyTraderNum")]
    public int NumberOfCopyTradersAccumulated { get; set; }

    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; } = string.Empty;

    /// <summary>
    /// Nick name
    /// </summary>
    [JsonProperty("nickName")]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// Margin currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Lead trader unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; } = string.Empty;

    /// <summary>
    /// Win ratio, 0.1 represents 10%
    /// </summary>
    [JsonProperty("winRatio")]
    public decimal WinRatio { get; set; }

    /// <summary>
    /// Lead days
    /// </summary>
    [JsonProperty("leadDays")]
    public int LeadDays { get; set; }

    /// <summary>
    /// Contract list which lead trader is leading
    /// </summary>
    [JsonProperty("traderInsts")]
    public List<string> TraderInstruments { get; set; } = [];

    /// <summary>
    /// Pnl (in USDT) of last 90 days.
    /// </summary>
    [JsonProperty("pnl")]
    public decimal ProfitAndLoss { get; set; }

    /// <summary>
    /// Pnl ratio of last 90 days. 0.1 represents 10%
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal ProfitAndLossRatio { get; set; }

    /// <summary>
    /// Pnl ratios
    /// </summary>
    [JsonProperty("pnlRatios")]
    public List<OkxCopyTradingLeadTraderProfitAndLossRatio> ProfitAndLossRatios { get; set; } = [];
}

/// <summary>
/// OKX Copy Trading Lead Trader Pnl Ratio
/// </summary>
public record OkxCopyTradingLeadTraderProfitAndLossRatio
{
    /// <summary>
    /// Begin time of pnl ratio on that day
    /// </summary>
    [JsonProperty("beginTs")]
    public long BeginTimestamp { get; set; }

    /// <summary>
    /// Begin time of pnl ratio on that day
    /// </summary>
    [JsonIgnore]
    public DateTime BeginTime => BeginTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Pnl ratio on that day
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal ProfitAndLossRatio { get; set; }
}