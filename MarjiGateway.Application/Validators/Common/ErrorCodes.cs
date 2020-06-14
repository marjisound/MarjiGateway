namespace MarjiGateway.Application.Validators.Common
{
    public static class ErrorCodes
    {
        public static readonly string CardNumberIsRequired = nameof(CardNumberIsRequired);
        public static readonly string ExpiryYearIsRequired = nameof(ExpiryYearIsRequired);
        public static readonly string ExpiryMonthIsRequired = nameof(ExpiryMonthIsRequired);
        public static readonly string AmountIsRequired = nameof(AmountIsRequired);
        public static readonly string CurrencyIsRequired = nameof(CurrencyIsRequired);
        public static readonly string CvvIsRequired = nameof(CvvIsRequired);
    }
}