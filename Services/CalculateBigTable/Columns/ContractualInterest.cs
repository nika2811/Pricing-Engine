namespace Pricing_Engine.Services.CalculateBigTable.Columns
{
    public class ContractualInterest
    {
        public decimal CalculateContractualInterest
            (string paymentType, decimal paymentAmount, decimal interestRate, decimal interestSpread, decimal commitmentAmount, decimal monthlyFeeIncome)
        {
            decimal paymentAmountTotal = 0;

            if (paymentType is "Interest only" or "Principal interest")
            {
                paymentAmountTotal = (paymentAmount * interestSpread) + (paymentAmount * interestSpread * interestRate);
            }
            else 
            {
                paymentAmountTotal = commitmentAmount + monthlyFeeIncome + (paymentAmount * interestSpread);
            }

            return paymentAmountTotal;
        }

    }
}
