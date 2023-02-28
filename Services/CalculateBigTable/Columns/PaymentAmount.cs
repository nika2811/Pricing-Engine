namespace Pricing_Engine.Services.CalculateBigTable.Columns;

public class PaymentAmount
{
    public decimal CalculatePaymentAmount(decimal usedPayment, int month, decimal maintenanceRate, string interestType,
        decimal commitmentAmount, decimal monthlyFeeIncome)
    {
        var paymentAmount = usedPayment * month + usedPayment * maintenanceRate;

        if (interestType == "Variable") paymentAmount += commitmentAmount + monthlyFeeIncome;

        return paymentAmount;
    }
}