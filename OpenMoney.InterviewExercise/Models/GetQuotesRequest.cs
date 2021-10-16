namespace OpenMoney.InterviewExercise.Models
{
    public class GetQuotesRequest
    {
        public float HouseValue { get; set; }
        public float Deposit { get; set; }
        public float MortgageAmount { get { return HouseValue - Deposit; } }
        public float LoanToValue { get { return MortgageAmount / HouseValue; } }
    }
}