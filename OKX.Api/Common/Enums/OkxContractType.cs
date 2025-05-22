﻿namespace OKX.Api.Common;

/// <summary>
/// OKX Contract Type
/// </summary>
public enum OkxContractType : byte
{
    /// <summary>
    /// Linear
    /// </summary>
    [Map("linear")]
    Linear = 1,

    /// <summary>
    /// Inverse
    /// </summary>
    [Map("inverse")]
    Inverse = 2,
}