using System.Linq;
using System.Threading.Tasks;
using OpenMoney.InterviewExercise.Enums;
using OpenMoney.InterviewExercise.Models;
using OpenMoney.InterviewExercise.Models.Quotes;
using OpenMoney.InterviewExercise.ThirdParties;

namespace OpenMoney.InterviewExercise.QuoteClients
{
    public interface IMortgageQuoteClient
    {
        Task<MortgageQuote>GetQuote(GetQuotesRequest getQuotesRequest);
    }

    public class MortgageQuoteClient : IMortgageQuoteClient
    {
        private readonly IThirdPartyMortgageApi _api;

        private const int HouseLoanMaxThreshold = 10000000;

        public MortgageQuoteClient(IThirdPartyMortgageApi api)
        {
            _api = api;
        }
        
        public async Task<MortgageQuote> GetQuote(GetQuotesRequest getQuotesRequest)
        {
            if(getQuotesRequest.HouseValue >= HouseLoanMaxThreshold)
            {
                return new MortgageQuote
                {
                    Error = new Error(ErrorCode.HouseThreSholdValue,"Max House value must not be greater than or equal to 10million.")
                };
            }
            var request = new ThirdPartyMortgageRequest
            {
                MortgageAmount = (decimal) getQuotesRequest.MortgageAmount
            };

            var response = await _api.GetQuotes(request);

            if(response == null)
            {
                return new MortgageQuote
                {
                    Error = new Error(ErrorCode.ApiReturnNull, "No Quote Found")
                };
            }

            var mortgageList = response.ToList();

            if(mortgageList.Count() < 1)
            {
                return new MortgageQuote
                {
                    Error = new Error(ErrorCode.ApiReturnEmpty, "No Quote Found")
                };
            }

            var cheapestMonthlyQuote = response.OrderBy(m => m.MonthlyPayment).FirstOrDefault().MonthlyPayment;

            return new MortgageQuote
            {
                MonthlyPayment = (float)cheapestMonthlyQuote
            };
            
        }
    }
}