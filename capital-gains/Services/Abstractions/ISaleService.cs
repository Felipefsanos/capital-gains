﻿using capital_gains.Entities;

namespace capital_gains.Services.Abstractions
{
    public interface ISaleService
    {
        decimal Sell(FinancialMarketOperation operation);
    }
}