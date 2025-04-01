﻿namespace OKX.Api.Account;

/// <summary>
/// OKX Trading Account Rest Api Client
/// </summary>
public class OkxAccountRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5AccountInstruments = "api/v5/account/instruments";
    private const string v5AccountBalance = "api/v5/account/balance";
    private const string v5AccountPositions = "api/v5/account/positions";
    private const string v5AccountPositionsHistory = "api/v5/account/positions-history";
    private const string v5AccountPositionRisk = "api/v5/account/account-position-risk";
    private const string v5AccountBills = "api/v5/account/bills";
    private const string v5AccountBillsArchive = "api/v5/account/bills-archive";
    private const string v5AccountBillsHistoryArchive = "api/v5/account/bills-history-archive";
    private const string v5AccountConfig = "api/v5/account/config";
    private const string v5AccountSetPositionMode = "api/v5/account/set-position-mode";
    private const string v5AccountSetLeverage = "api/v5/account/set-leverage";
    private const string v5AccountMaxSize = "api/v5/account/max-size";
    private const string v5AccountMaxAvailSize = "api/v5/account/max-avail-size";
    private const string v5AccountPositionMarginBalance = "api/v5/account/position/margin-balance";
    private const string v5AccountLeverageInfo = "api/v5/account/leverage-info";
    private const string v5AccountAdjustLeverageInfo = "api/v5/account/adjust-leverage-info";
    private const string v5AccountMaxLoan = "api/v5/account/max-loan";
    private const string v5AccountTradeFee = "api/v5/account/trade-fee";
    private const string v5AccountInterestAccrued = "api/v5/account/interest-accrued";
    private const string v5AccountInterestRate = "api/v5/account/interest-rate";
    private const string v5AccountSetGreeks = "api/v5/account/set-greeks";
    private const string v5AccountSetIsolatedMode = "api/v5/account/set-isolated-mode";
    private const string v5AccountMaxWithdrawal = "api/v5/account/max-withdrawal";
    private const string v5AccountRiskState = "api/v5/account/risk-state";
    private const string v5AccountInterestLimits = "api/v5/account/interest-limits";
    private const string v5AccountSpotManualBorrowRepay = "api/v5/account/spot-manual-borrow-repay";
    private const string v5AccountSetAutoRepay = "api/v5/account/set-auto-repay";
    private const string v5AccountSpotBorrowRepayHistory = "api/v5/account/spot-borrow-repay-history";
    private const string v5AccountPositionBuilder = "api/v5/account/position-builder";
    private const string v5AccountSetRiskOffsetAmount = "api/v5/account/set-riskOffset-amt";
    private const string v5AccountGreeks = "api/v5/account/greeks";
    private const string v5AccountPositionTiers = "api/v5/account/position-tiers";
    private const string v5AccountActivateOption = "api/v5/account/activate-option";
    private const string v5AccountSetAutoLoan = "api/v5/account/set-auto-loan";
    private const string v5AccountAccountLevelSwitchPreset = "api/v5/account/account-level-switch-preset";
    private const string v5AccountSetAccountSwitchPrecheck = "api/v5/account/set-account-switch-precheck";
    private const string v5AccountSetAccountLevel = "api/v5/account/set-account-level";
    private const string v5AccountSetCollateralAssets = "api/v5/account/set-collateral-assets";
    private const string v5AccountCollateralAssets = "api/v5/account/collateral-assets";
    private const string v5AccountMmpReset = "api/v5/account/mmp-reset";
    private const string v5AccountMmpConfig = "api/v5/account/mmp-config";

    /// <summary>
    /// Retrieve available instruments info of current account.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicInstrument>>> GetInstrumentsAsync(
       OkxInstrumentType instrumentType,
       string? instrumentFamily = null,
       string? instrumentId = null,
       string? underlying = null,
       CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("uly", underlying);

        return ProcessListRequestAsync<OkxPublicInstrument>(GetUri(v5AccountInstruments), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the account.
    /// </summary>
    /// <param name="currencies">Currencies</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountBalance>> GetBalancesAsync(IEnumerable<string>? currencies = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        if (currencies is not null && currencies.Count() > 0)
            parameters.AddOptional("ccy", string.Join(",", currencies));

        return ProcessOneRequestAsync<OkxAccountBalance>(GetUri(v5AccountBalance), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve information on your positions. When the account is in net mode, net positions will be displayed, and when the account is in long/short mode, long or short positions will be displayed.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountPosition>>> GetPositionsAsync(
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        string? positionId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("posId", positionId);

        return ProcessListRequestAsync<OkxAccountPosition>(GetUri(v5AccountPositions), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the updated position data for the last 3 months. Return in reverse chronological order using utime.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin mode</param>
    /// <param name="type">The type of closing position. It is the latest type if there are several types for the same position.</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested uTime, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records earlier than the requested uTime, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountPositionHistory>>> GetPositionsHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        OkxAccountMarginMode? marginMode = null,
        OkxClosingPositionType? type = null,
        string? positionId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("mgnMode", marginMode);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("posId", positionId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountPositionHistory>(GetUri(v5AccountPositionsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get account and position risk
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountPositionBalance>>> GetPositionRiskAsync(OkxInstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxAccountPositionBalance>(GetUri(v5AccountPositionRisk), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the bills of the account. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with the most recent first. This endpoint can retrieve data from the last 7 days.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with an end timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountBill>>> GetBillHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        string? currency = null,
        OkxAccountMarginMode? marginMode = null,
        OkxContractType? contractType = null,
        OkxAccountBillType? billType = null,
        OkxAccountBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("mgnMode", marginMode);
        parameters.AddOptionalEnum("ctType", contractType);
        parameters.AddOptionalEnum("type", billType);
        parameters.AddOptionalEnum("subType", billSubType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountBill>(GetUri(v5AccountBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the account’s bills. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with most recent first. This endpoint can retrieve data from the last 3 months.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with an end timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountBill>>> GetBillArchiveAsync(
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        string? currency = null,
        OkxAccountMarginMode? marginMode = null,
        OkxContractType? contractType = null,
        OkxAccountBillType? billType = null,
        OkxAccountBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("mgnMode", marginMode);
        parameters.AddOptionalEnum("ctType", contractType);
        parameters.AddOptionalEnum("type", billType);
        parameters.AddOptionalEnum("subType", billSubType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountBill>(GetUri(v5AccountBillsArchive), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Apply for bill data since 1 February, 2021 except for the current quarter.
    /// </summary>
    /// <param name="year">4 digits year</param>
    /// <param name="quarter">Quarter, valid value is Q1, Q2, Q3, Q4</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDownloadApplication>> ApplyBillDataAsync(int year, OkxQuarter quarter, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "year", year.ToOkxString() }
        };
        parameters.AddEnum("quarter", quarter);

        return ProcessOneRequestAsync<OkxDownloadApplication>(GetUri(v5AccountBillsHistoryArchive), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Apply for bill data since 1 February, 2021 except for the current quarter.
    /// </summary>
    /// <param name="year">4 digits year</param>
    /// <param name="quarter">Quarter, valid value is Q1, Q2, Q3, Q4</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDownloadLink>> GetBillDataAsync(int year, OkxQuarter quarter, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "year", year.ToOkxString() }
        };
        parameters.AddEnum("quarter", quarter);

        return ProcessOneRequestAsync<OkxDownloadLink>(GetUri(v5AccountBillsHistoryArchive), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve current account configuration.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountConfiguration>> GetConfigurationAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxAccountConfiguration>(GetUri(v5AccountConfig), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// FUTURES and SWAP support both long/short mode and net mode. In net mode, users can only have positions in one direction; In long/short mode, users can hold positions in long and short directions.
    /// </summary>
    /// <param name="positionMode"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxTradePositionMode?>> SetPositionModeAsync(OkxTradePositionMode positionMode, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("posMode", positionMode);

        var result = await ProcessOneRequestAsync<OkxAccountPositionModeContainer>(GetUri(v5AccountSetPositionMode), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<OkxTradePositionMode?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<OkxTradePositionMode?>(result.Request, result.Response, result.Data.Payload, result.Raw, result.Error);
    }

    /// <summary>
    /// There are 10 different scenarios for leverage setting:
    /// 1. Set leverage for MARGIN instruments under isolated-margin trade mode at pairs level.
    /// 2. Set leverage for MARGIN instruments under cross-margin trade mode and Single-currency margin account mode at pairs level.
    /// 3. Set leverage for MARGIN instruments under cross-margin trade mode and Multi-currency margin at currency level.
    /// 4. Set leverage for MARGIN instruments under cross-margin trade mode and Portfolio margin at currency level.
    /// 5. Set leverage for FUTURES instruments under cross-margin trade mode at underlying level.
    /// 6. Set leverage for FUTURES instruments under isolated-margin trade mode and buy/sell position mode at contract level.
    /// 7. Set leverage for FUTURES instruments under isolated-margin trade mode and long/short position mode at contract and position side level.
    /// 8. Set leverage for SWAP instruments under cross-margin trade at contract level.
    /// 9. Set leverage for SWAP instruments under isolated-margin trade mode and buy/sell position mode at contract level.
    /// 10. Set leverage for SWAP instruments under isolated-margin trade mode and long/short position mode at contract and position side level.
    /// 
    /// Note that the request parameter posSide is only required when margin mode is isolated in long/short position mode for FUTURES/SWAP instruments (see scenario 7 and 10 above).
    /// Please refer to the request examples on the right for each case.
    /// </summary>
    /// <param name="leverage">Leverage</param>
    /// <param name="currency">Currency</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountLeverage>>> SetLeverageAsync(
        int leverage,
        string? currency = null,
        string? instrumentId = null,
        OkxAccountMarginMode? marginMode = null,
        OkxTradePositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        if (leverage < 1)
            throw new ArgumentException("Invalid Leverage");

        if (string.IsNullOrEmpty(currency) && string.IsNullOrEmpty(instrumentId))
            throw new ArgumentException("Either instId or ccy is required; if both are passed, instId will be used by default.");

        if (marginMode is null)
            throw new ArgumentException("marginMode is required");

        var parameters = new ParameterCollection
        {
            { "lever", leverage.ToOkxString() }
        };
        parameters.AddEnum("mgnMode", marginMode);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptionalEnum("posSide", positionSide);

        return ProcessListRequestAsync<OkxAccountLeverage>(GetUri(v5AccountSetLeverage), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The maximum quantity to buy or sell. It corresponds to the "sz" from placement.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="price">Price</param>
    /// <param name="leverage">Leverage for instrument</param>
    /// <param name="unSpotOffset">Spot-Derivatives risk offset</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountMaximumOrderQuantity>>> GetMaximumOrderQuantityAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        string? currency = null,
        decimal? price = null,
        int? leverage = null,
        bool? unSpotOffset = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId }
        };
        parameters.AddEnum("tdMode", tradeMode);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("px", price?.ToOkxString());
        parameters.AddOptional("leverage", leverage?.ToOkxString());
        parameters.AddOptional("unSpotOffset", unSpotOffset);

        return ProcessListRequestAsync<OkxAccountMaximumOrderQuantity>(GetUri(v5AccountMaxSize), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get Maximum Available Tradable Amount
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="price">The available amount corresponds to price of close position. Only applicable to reduceOnly MARGIN.</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// <param name="unSpotOffset">Spot-Derivatives risk offset</param>
    /// <param name="quickMarginType">Quick Margin type. Only applicable to Quick Margin Mode of isolated margin</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountMaximumAvailableAmount>>> GetMaximumAvailableAmountAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        string? currency = null,
        decimal? price = null,
        bool? reduceOnly = null,
        bool? unSpotOffset = null,
        OkxQuickMarginType? quickMarginType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId }
        };
        parameters.AddEnum("tdMode", tradeMode);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("reduceOnly", reduceOnly);
        parameters.AddOptional("px", price?.ToOkxString());
        parameters.AddOptional("unSpotOffset", unSpotOffset);
        parameters.AddOptionalEnum("quickMgnType", quickMarginType);

        return ProcessListRequestAsync<OkxAccountMaximumAvailableAmount>(GetUri(v5AccountMaxAvailSize), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Increase or decrease the margin of the isolated position.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="type">Type</param>
    /// <param name="amount">Amount</param>
    /// <param name="currency">Currency, only applicable to MARGIN（Manual transfers and Quick Margin Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountMarginBalance>>> SetMarginAmountAsync(
        string instrumentId,
        OkxTradePositionSide positionSide,
        OkxAccountMarginAddReduce type,
        decimal amount,
        string? currency = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
            { "amt", amount.ToOkxString() }
        };
        parameters.AddEnum("posSide", positionSide);
        parameters.AddEnum("type", type);
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxAccountMarginBalance>(GetUri(v5AccountPositionMarginBalance), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get Leverage
    /// </summary>
    /// <param name="instrumentIds">ingle instrument ID or multiple instrument IDs (no more than 20) separated with comma</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountLeverage>>> GetLeverageAsync(
        string instrumentIds,
        OkxAccountMarginMode marginMode,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", instrumentIds }
        };
        parameters.AddEnum("mgnMode", marginMode);

        return ProcessListRequestAsync<OkxAccountLeverage>(GetUri(v5AccountLeverageInfo), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get leverage estimated info
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="marginMode">Margin mode</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="instrumentId">Instrument ID，e.g. BTC-USDT</param>
    /// <param name="currency">Currency used for margin, e.g. BTC</param>
    /// <param name="positionSide">posSide</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountLeverageEstimatedInformation>>> GetLeverageEstimatedInformationAsync(
        OkxInstrumentType instrumentType,
        OkxAccountMarginMode marginMode,
        int leverage,
        string? instrumentId = null,
        string? currency = null,
        OkxTradePositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "lever", leverage.ToOkxString() }
        };
        parameters.AddEnum("instType", instrumentType);
        parameters.AddEnum("mgnMode", marginMode);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptionalEnum("posSide", positionSide);

        return ProcessListRequestAsync<OkxAccountLeverageEstimatedInformation>(GetUri(v5AccountAdjustLeverageInfo), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get the maximum loan of instrument
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="currency">Margin Currency</param>
    /// <param name="marginCurrency">Margin Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountMaximumLoanAmount>>> GetMaximumLoanAmountAsync(
        OkxAccountMarginMode marginMode,
        string? instrumentId = null,
        string? currency = null,
        string? marginCurrency = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("mgnMode", marginMode);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("mgnCcy", marginCurrency);

        return ProcessListRequestAsync<OkxAccountMaximumLoanAmount>(GetUri(v5AccountMaxLoan), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get Fee Rates
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ruleType">Trading rule types</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountFeeRate>> GetFeeRatesAsync(
        OkxInstrumentType instrumentType,
        OkxInstrumentRuleType ruleType,
        string? instrumentId = null,
        string? underlying = null,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddEnum("ruleType", ruleType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("uly", underlying);
        parameters.AddOptional("instFamily", instrumentFamily);

        return ProcessOneRequestAsync<OkxAccountFeeRate>(GetUri(v5AccountTradeFee), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get interest-accrued
    /// </summary>
    /// <param name="type">Loan type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountInterestAccrued>>> GetInterestAccruedAsync(
        OkxAccountLoanType? type = null,
        string? currency = null,
        string? instrumentId = null,
        OkxAccountMarginMode? marginMode = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalEnum("mgnMode", marginMode);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountInterestAccrued>(GetUri(v5AccountInterestAccrued), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get the user's current leveraged currency borrowing interest rate
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountInterestRate>>> GetInterestRateAsync(
        string? currency = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxAccountInterestRate>(GetUri(v5AccountInterestRate), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Set the display type of Greeks.
    /// </summary>
    /// <param name="greeksType">Display type of Greeks.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxAccountGreeksType?>> SetGreeksAsync(OkxAccountGreeksType greeksType, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("greeksType", greeksType);

        var result = await ProcessOneRequestAsync<OkxAccountGreeksTypeContainer>(GetUri(v5AccountSetGreeks), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<OkxAccountGreeksType?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<OkxAccountGreeksType?>(result.Request, result.Response, result.Data.Payload, result.Raw, result.Error);
    }

    /// <summary>
    /// You can set the currency margin and futures/perpetual Isolated margin trading mode
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="marginMode">Isolated margin trading settings</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<RestCallResult<OkxAccountIsolatedMarginMode?>> SetIsolatedMarginModeAsync(OkxInstrumentType instrumentType, OkxAccountIsolatedMarginMode marginMode, CancellationToken ct = default)
    {
        if (!instrumentType.IsIn(OkxInstrumentType.Margin, OkxInstrumentType.Contracts)) throw new ArgumentException("Only Margin and Contracts allowed", nameof(instrumentType));
        var parameters = new ParameterCollection();
        parameters.AddEnum("isoMode", marginMode);
        parameters.AddEnum("type", instrumentType);

        var result = await ProcessOneRequestAsync<OkxAccountIsolatedMarginModeContainer>(GetUri(v5AccountSetIsolatedMode), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<OkxAccountIsolatedMarginMode?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<OkxAccountIsolatedMarginMode?>(result.Request, result.Response, result.Data.Payload, result.Raw, result.Error);
    }

    /// <summary>
    /// Retrieve the maximum transferable amount.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountWithdrawalAmount>>> GetMaximumWithdrawalsAsync(
        string? currency = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxAccountWithdrawalAmount>(GetUri(v5AccountMaxWithdrawal), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get account risk state
    /// Only applicable to Portfolio margin account
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountRiskState>> GetRiskStateAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxAccountRiskState>(GetUri(v5AccountRiskState), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Get borrow interest and limit
    /// </summary>
    /// <param name="type">Loan type</param>
    /// <param name="currency">Loan currency, e.g. BTC</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountInterestLimits>>> GetInterestLimitsAsync(
    OkxAccountLoanType? type = null,
    string? currency = null,
    CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxAccountInterestLimits>(GetUri(v5AccountInterestLimits), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Only applicable to Spot mode (enabled borrowing)
    /// </summary>
    /// <param name="side">Side</param>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="amount">Amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountBorrowRepay>> ManualBorrowRepayAsync(string side, string currency, decimal amount, CancellationToken ct = default)
    {
        if (side == "borrow") return ManualBorrowAsync(currency, amount, ct);
        if (side == "repay") return ManualRepayAsync(currency, amount, ct);

        throw new ArgumentException("Invalid side", nameof(side));
    }

    /// <summary>
    /// Only applicable to Spot mode (enabled borrowing)
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="amount">Amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountBorrowRepay>> ManualBorrowAsync(string currency, decimal amount, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ccy", currency },
            { "side", "borrow" },
            { "amt", amount.ToOkxString() }
        };

        return ProcessOneRequestAsync<OkxAccountBorrowRepay>(GetUri(v5AccountSpotManualBorrowRepay), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Only applicable to Spot mode (enabled borrowing)
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="amount">Amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountBorrowRepay>> ManualRepayAsync(string currency, decimal amount, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ccy", currency },
            { "side", "repay" },
            { "amt", amount.ToOkxString() }
        };

        return ProcessOneRequestAsync<OkxAccountBorrowRepay>(GetUri(v5AccountSpotManualBorrowRepay), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Only applicable to Spot mode (enabled borrowing)
    /// </summary>
    /// <param name="autoRepay">Whether auto repay is allowed or not under Spot mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<bool?>> SetAutoRepayAsync(bool autoRepay, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "autoRepay", autoRepay }
        };

        var result = await ProcessOneRequestAsync<OkxAccountAutoRepayContainer>(GetUri(v5AccountSetAutoRepay), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<bool?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<bool?>(result.Request, result.Response, result.Data.Payload, result.Raw, result.Error);
    }

    /// <summary>
    /// Retrieve the borrow/repay history under Spot mode
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="type">Event type
    /// auto_borrow
    /// auto_repay
    /// manual_borrow
    /// manual_repay</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts (included), Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts(included), Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountBorrowRepayHistory>>> GetBorrowRepayHistoryAsync(
        string currency,
        string type,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("type", type);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountBorrowRepayHistory>(GetUri(v5AccountSpotBorrowRepayHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Calculates portfolio margin information for virtual position/assets or current position of the user.
    /// You can add up to 200 virtual positions and 200 virtual assets in one request.
    /// </summary>
    /// <param name="accountLevel">Switch to account mode</param>
    /// <param name="importExisting">Whether import existing positions and assets. The default is true</param>
    /// <param name="leverage">Cross margin leverage in Multi-currency margin mode, the default is 1. If the allowed leverage is exceeded, set according to the maximum leverage. Only applicable to Multi-currency margin</param>
    /// <param name="simulatedPositions">List of simulated positions</param>
    /// <param name="simulatedAssets">List of simulated assets</param>
    /// <param name="greeksType">Greeks type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountPositionBuilder>>> PositionBuilderAsync(
    OkxAccountMode? accountLevel = null,
    bool importExisting = true,
    int? leverage = null,
    IEnumerable<OkxAccountSimulatedPosition>? simulatedPositions = null,
    IEnumerable<OkxAccountSimulatedAsset>? simulatedAssets = null,
    OkxAccountGreeksType? greeksType = OkxAccountGreeksType.BlackScholesGreeksInDollars,
    CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("acctLv", accountLevel);
        parameters.AddOptional("lever", leverage?.ToOkxString());
        parameters.AddOptional("inclRealPosAndEq", importExisting);
        parameters.AddOptional("simPos", simulatedPositions);
        parameters.AddOptional("simAsset", simulatedAssets);
        parameters.AddOptionalEnum("greeksType", greeksType);

        return ProcessListRequestAsync<OkxAccountPositionBuilder>(GetUri(v5AccountPositionBuilder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Set risk offset amount. This does not represent the actual spot risk offset amount. Only applicable to Portfolio Margin Mode.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="spotRiskOffsetAmount">Spot risk offset amount defined by users</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountRiskOffsetAmount>> SetRiskOffsetAmountAsync(
        string currency,
        decimal spotRiskOffsetAmount,
    CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ccy", currency },
            { "clSpotInUseAmt", spotRiskOffsetAmount.ToOkxString() }
        };

        return ProcessOneRequestAsync<OkxAccountRiskOffsetAmount>(GetUri(v5AccountSetRiskOffsetAmount), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve a greeks list of all assets in the account.
    /// </summary>
    /// <param name="currency">Single currency, e.g. BTC.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountGreeks>>> GetGreeksAsync(
    string? currency = null,
    CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxAccountGreeks>(GetUri(v5AccountGreeks), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve cross position limitation of SWAP/FUTURES/OPTION under Portfolio margin mode.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="underlying">Single underlying or multiple underlyings (no more than 3) separated with comma. Either uly or instFamily is required. If both are passed, instFamily will be used.</param>
    /// <param name="instrumentFamily">Single instrument family or instrument families (no more than 5) separated with comma. Either uly or instFamily is required. If both are passed, instFamily will be used.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountPositionTiers>> GetPositionTiersAsync(
    OkxInstrumentType instrumentType,
    string? underlying = null,
    string? instrumentFamily = null,
    CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("uly", underlying);
        parameters.AddOptional("instFamily", instrumentFamily);

        return ProcessOneRequestAsync<OkxAccountPositionTiers>(GetUri(v5AccountPositionTiers), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Activate option
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTimestamp>> ActivateOptionAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxTimestamp>(GetUri(v5AccountActivateOption), HttpMethod.Post, ct, signed: true);
    }

    /// <summary>
    /// Set auto loan
    /// Only applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    /// <param name="autoLoan">Whether to automatically make loans. Valid values are true, false. The default is true</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<bool?>> SetAutoLoanAsync(bool autoLoan, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "autoLoan", autoLoan }
        };

        var result = await ProcessOneRequestAsync<OkxAccountAutoLoanContainer>(GetUri(v5AccountSetAutoLoan), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<bool?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<bool?>(result.Request, result.Response, result.Data.Payload, result.Raw, result.Error);
    }

    /// <summary>
    /// Pre-set the required information for account mode switching. When switching from Portfolio margin mode back to Spot and futures mode / Multi-currency margin mode, and if there are existing cross-margin contract positions, it is mandatory to pre-set leverage.
    /// If the user does not follow the required settings, they will receive an error message during the pre-check or when setting the account mode.
    /// </summary>
    /// <param name="accountMode">Account mode</param>
    /// <param name="leverage"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountPresetMode>> SwitchPresetAccountModeAsync(
        OkxAccountMode accountMode,
        int? leverage = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("acctLv", accountMode);
        parameters.AddOptional("lever", leverage);

        return ProcessOneRequestAsync<OkxAccountPresetMode>(GetUri(v5AccountAccountLevelSwitchPreset), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve precheck information for account mode switching.
    /// </summary>
    /// <param name="accountMode">Account mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountPrecheckMode>> PrecheckAccountModeSwitchAsync(
    OkxAccountMode accountMode,
    CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("acctLv", accountMode);

        return ProcessOneRequestAsync<OkxAccountPrecheckMode>(GetUri(v5AccountSetAccountSwitchPrecheck), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Set account mode
    /// You need to set on the Web/App for the first set of every account mode.
    /// </summary>
    /// <param name="accountLevel">Account mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxAccountMode?>> SetLevelAsync(OkxAccountMode accountLevel, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("acctLv", accountLevel);

        var result = await ProcessOneRequestAsync<OkxAccountModeContainer>(GetUri(v5AccountSetAccountLevel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<OkxAccountMode?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<OkxAccountMode?>(result.Request, result.Response, result.Data.Payload, result.Raw, result.Error);
    }

    /// <summary>
    /// Set collateral assets
    /// </summary>
    /// <param name="type">Type</param>
    /// <param name="collateralEnabled">Whether or not set the assets to be collateral</param>
    /// <param name="currencies">Currency list, e.g. ["BTC","ETH"]. If type = custom, the parameter is required.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountCollateralAssets>> SetCollateralAssetsAsync(
        OkxAccountCollateralAssetsMode type,
        bool collateralEnabled,
        IEnumerable<string>? currencies = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("type", type);
        parameters.Add("collateralEnabled", collateralEnabled);
        parameters.AddOptional("ccyList", currencies);

        return ProcessOneRequestAsync<OkxAccountCollateralAssets>(GetUri(v5AccountSetCollateralAssets), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve collateral assets
    /// </summary>
    /// <param name="currency">Single currency or multiple currencies (no more than 20) separated with comma, e.g. "BTC" or "BTC,ETH".</param>
    /// <param name="collateralEnabled">Whether or not to be a collateral asset</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountCollateralAsset>>> GetCollateralAssetsAsync(
        string? currency = null,
        bool? collateralEnabled = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("collateralEnabled", collateralEnabled);

        return ProcessListRequestAsync<OkxAccountCollateralAsset>(GetUri(v5AccountCollateralAssets), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Reset MMP Status
    /// You can unfreeze by this endpoint once MMP is triggered.
    /// Only applicable to Option in Portfolio Margin mode, and MMP privilege is required.
    /// </summary>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<bool?>> ResetMmpAsync(string instrumentFamily, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instType", "OPTION" },
            { "instFamily", instrumentFamily }
        };

        var result = await ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5AccountMmpReset), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<bool?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<bool?>(result.Request, result.Response, result.Data.Result, result.Raw, result.Error);
    }

    /// <summary>
    /// Set MMP
    /// This endpoint is used to set MMP configure
    /// Only applicable to Option in Portfolio Margin mode, and MMP privilege is required.
    /// 
    /// What is MMP?
    /// Market Maker Protection (MMP) is an automated mechanism for market makers to pull their quotes when their executions exceed a certain threshold(`qtyLimit`) within a certain time frame(`timeInterval`). Once mmp is triggered, any pre-existing mmp pending orders(`mmp` and `mmp_and_post_only` orders) will be automatically canceled, and new orders tagged as MMP will be rejected for a specific duration(`frozenInterval`), or until manual reset by makers.
    /// 
    /// How to enable MMP?
    /// Please send an email to institutional@okx.com or contact your business development (BD) manager to apply for MMP. The initial threshold will be upon your request.
    /// </summary>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="timeInterval">Time window (ms). MMP interval where monitoring is done. "0" means disable MMP</param>
    /// <param name="frozenInterval">Frozen period (ms). "0" means the trade will remain frozen until you request "Reset MMP Status" to unfrozen</param>
    /// <param name="quantityLimit">Trade qty limit in number of contracts. Must be > 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountMmpConfiguration>> SetMmpAsync(string instrumentFamily, int timeInterval, int frozenInterval, int quantityLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instFamily", instrumentFamily },
            { "timeInterval", timeInterval.ToOkxString() },
            { "frozenInterval", frozenInterval.ToOkxString() },
            { "qtyLimit", quantityLimit.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxAccountMmpConfiguration>(GetUri(v5AccountMmpConfig), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// GET MMP Config
    /// This endpoint is used to get MMP configure information
    /// Only applicable to Option in Portfolio Margin mode, and MMP privilege is required.
    /// </summary>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountMmpConfigurationData>> GetMmpAsync(string instrumentFamily, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instFamily", instrumentFamily },
        };

        return ProcessOneRequestAsync<OkxAccountMmpConfigurationData>(GetUri(v5AccountMmpConfig), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

}
