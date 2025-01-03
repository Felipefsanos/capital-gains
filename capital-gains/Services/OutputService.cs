﻿using capital_gains.Entities;
using capital_gains.Services.Abstractions;
using System.Text.Json;

namespace capital_gains.Services
{
    public class OutputService : IOutputService
    {
        public List<TaxResponseModel> Taxes { get; set; } = new List<TaxResponseModel>();

        public void AddPurhaseTax() => Taxes.Add(new TaxResponseModel(0.00m));
        public void AddSellTax(decimal taxValue) => Taxes.Add(new TaxResponseModel(taxValue));

        public string GetProgramOutput() => JsonSerializer.Serialize(Taxes, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
