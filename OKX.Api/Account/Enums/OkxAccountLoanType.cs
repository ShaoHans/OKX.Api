﻿namespace OKX.Api.Account;

/// <summary>
/// OKX Loan Type
/// </summary>
public enum OkxAccountLoanType : byte
{
    /// <summary>
    /// VIP Loans
    /// </summary>
    [Map("1")]
    VIP = 1,

    /// <summary>
    /// Market Loans
    /// </summary>
    [Map("2")]
    Market = 2,
}