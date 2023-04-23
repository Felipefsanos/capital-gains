using capital_gains.Services;

namespace UnitTests.Services
{
    public class OutputServiceTests
    {
        public readonly OutputService _service;

        public OutputServiceTests()
        {
            _service = new OutputService();
        }

        [Fact]
        public void ShoudlAddTaxWithZeroValue_WhenCallAddPurchaseTax()
        {
            _service.AddPurhaseTax();

            Assert.True(_service.Taxes.Count == 1);
            Assert.Contains(_service.Taxes, t => t.Tax == decimal.Zero);
        }

        [Fact]
        public void ShoudlAddTaxWithValue_WhenCallAddSellTax()
        {
            decimal taxValue = 5.00m;
            _service.AddSellTax(taxValue);

            Assert.True(_service.Taxes.Count == 1);
            Assert.Contains(_service.Taxes, t => t.Tax == taxValue);
        }

        [Fact]
        public void ShoudlReturnStringList_WhenCallGetProgramOutput()
        {
            decimal taxValue = 5.00m;
            _service.AddSellTax(taxValue);

            var result = _service.GetProgramOutput();

            Assert.Contains("5.00", result);
        }
    }
}
