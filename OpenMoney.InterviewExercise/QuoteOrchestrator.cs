﻿using OpenMoney.InterviewExercise.Models;
using OpenMoney.InterviewExercise.QuoteClients;
using System.Threading.Tasks;

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

        public async Task<GetQuotesResponse> GetQuotes(GetQuotesRequest request)
        {
            return new GetQuotesResponse
            {
                MortgageQuote = await _mortgageQuoteClient.GetQuote(request),
                HomeInsuranceQuote =await _homeInsuranceQuoteClient.GetQuote(request)
            };
        }
    }
}