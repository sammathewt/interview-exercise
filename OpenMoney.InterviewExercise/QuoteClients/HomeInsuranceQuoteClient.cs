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
    public interface IHomeInsuranceQuoteClient
    {
        Task<HomeInsuranceQuote> GetQuote(GetQuotesRequest getQuotesRequest);
    }

    public class HomeInsuranceQuoteClient : IHomeInsuranceQuoteClient
    {
        private IThirdPartyHomeInsuranceApi _api;
        
        public decimal contentsValue = 50_000;

        public HomeInsuranceQuoteClient(IThirdPartyHomeInsuranceApi api)
        {
            _api = api;
        }

        public async Task<HomeInsuranceQuote>  GetQuote(GetQuotesRequest getQuotesRequest)
        {

           if (getQuotesRequest.HouseValue > 10_000_000d)
            {
                return new HomeInsuranceQuote
                {
                    Error = new Error(ErrorCode.HouseThreSholdValue, "Max House value must not be greater than  10million.")
                };
            }
            
            var request = new ThirdPartyHomeInsuranceRequest
            {
                HouseValue = (decimal) getQuotesRequest.HouseValue,
                ContentsValue = contentsValue
            };

      
            var responses = await  _api.GetQuotes(request);
           
                var cheapestMonthlyQuote = QuoteHelper.GetMinimumQuoteResponse(responses);

            if (cheapestMonthlyQuote.Error.Message.ToString() != "")
            {
                return new HomeInsuranceQuote
                {
                    Error = new Error(cheapestMonthlyQuote.Error.Code, cheapestMonthlyQuote.Error.Message)
                };
                }
            else
            {
                return new HomeInsuranceQuote
                {
                  MonthlyPayment =cheapestMonthlyQuote.MonthlyPayment
                };

            }

        }
 
    }
}