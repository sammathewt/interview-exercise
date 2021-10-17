using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenMoney.InterviewExercise.ThirdParties
{
    /// <summary>
    /// Don't change this interface! 
    /// </summary>
    public interface IThirdPartyHomeInsuranceApi
    {
        Task<IEnumerable<ThirdPartyHomeInsuranceResponse>> GetQuotes(ThirdPartyHomeInsuranceRequest request);
    }

    /// <summary>
    /// Don't change this model! 
    /// </summary>
    public class ThirdPartyHomeInsuranceRequest
    {
        public decimal HouseValue { get; set; }
        public decimal ContentsValue { get; set; }
    }

    /// <summary>
    /// Don't change this model! 
    /// </summary>
    public class ThirdPartyHomeInsuranceResponse : IQuoteResponse
    {
        public decimal MonthlyPayment { get; set; }
    }
}