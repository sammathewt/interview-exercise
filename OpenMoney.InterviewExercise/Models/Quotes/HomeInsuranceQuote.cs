namespace OpenMoney.InterviewExercise.Models.Quotes
{
    public class HomeInsuranceQuote
    {
        public float MonthlyPayment { get; set; }
        public float BuildingsCover { get; set; }
        public float ContentsCover { get; set; }
        
      //  public int LengthInMonths { get; set; }
        public Error Error { get; set; }
    }
}