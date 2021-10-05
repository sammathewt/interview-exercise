using OpenMoney.InterviewExercise.Models;
using OpenMoney.InterviewExercise.QuoteClients;

namespace OpenMoney.InterviewExercise
{
    public class QuoteOrchestrator
    {
        private readonly IHomeInsuranceQuoteClient _homeInsuranceQuoteClient;
        private readonly IMortgageQuoteClient _mortgageQuoteClient;

        public QuoteOrchestrator(
            IHomeInsuranceQuoteClient homeInsuranceQuoteClient,
            IMortgageQuoteClient mortgageQuoteClient)
        {
            _homeInsuranceQuoteClient = homeInsuranceQuoteClient;
            _mortgageQuoteClient = mortgageQuoteClient;
        }

        public GetQuotesResponse GetQuotes(GetQuotesRequest request)
        {
            return new GetQuotesResponse
            {
                MortgageQuote = _mortgageQuoteClient.GetQuote(
                    new decimal(request.HouseValue),
                    new decimal(request.Deposit)
                ),
                HomeInsuranceQuote = _homeInsuranceQuoteClient.GetQuote(
                    new decimal(request.HouseValue)
                )
            };
        }
    }
}