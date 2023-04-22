namespace capital_gains.Entities
{
    public class ExecutionState
    {
        public decimal ActualProfit { get; private set; }
        public decimal WeightedAverage { get; set; }
        public long CurrentQuantity { get; private set; }

        public bool HasLoss() => ActualProfit < 0;

        public void SetProfit(decimal profit) => ActualProfit += profit;

        public void SellActions(long quantity)
        {
            CurrentQuantity -= quantity;

            if (CurrentQuantity == 0)
                ActualProfit = decimal.Zero;
        }

        public void PurchaseActions(long quantity) => CurrentQuantity += quantity;
    }
}
