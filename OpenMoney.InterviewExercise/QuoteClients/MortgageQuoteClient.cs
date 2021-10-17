using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenMoney.InterviewExercise.BusinessLogic;
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
            if(getQuotesRequest.HouseValue > HouseLoanMaxThreshold)
            {
                return new MortgageQuote
                {
                    Error = new Error(ErrorCode.HouseThreSholdValue,"Max House value must not be greater than  10million.")
                };
            }
            var request = new ThirdPartyMortgageRequest
            {
                MortgageAmount = (decimal) getQuotesRequest.MortgageAmount
            };

           
            var responses = await _api.GetQuotes(request);
            var cheapestMonthlyQuote = QuoteHelper.GetMinimumQuoteResponse(responses);
            if (cheapestMonthlyQuote.Error.Message.ToString() != "")
            {
                return new MortgageQuote
                {
                    Error = new Error(cheapestMonthlyQuote.Error.Code, cheapestMonthlyQuote.Error.Message)
                };
            }
            else
            {
                return new MortgageQuote
                {
                    MonthlyPayment = cheapestMonthlyQuote.MonthlyPayment
                };

            }
       

        }
    }
}