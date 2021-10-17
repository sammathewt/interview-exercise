using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenMoney.InterviewExercise.ThirdParties
{
    /// <summary>
    /// Don't change this interface! 
    /// </summary>
    public interface IThirdPartyMortgageApi
    {
        Task<IEnumerable<ThirdPartyMortgageResponse>> GetQuotes(ThirdPartyMortgageRequest request);
    }

    /// <summary>
    /// Don't change this model! 
    /// </summary>
    public class ThirdPartyMortgageRequest
    {
        public decimal MortgageAmount { get; set; }
    }

    /// <summary>
    /// Don't change this model! 
    /// </summary>
    public class ThirdPartyMortgageResponse : IQuoteResponse
    {
        public decimal MonthlyPayment { get; set; }
    }
}