using Moq;
using OpenMoney.InterviewExercise.Enums;
using OpenMoney.InterviewExercise.Models;
using OpenMoney.InterviewExercise.QuoteClients;
using OpenMoney.InterviewExercise.ThirdParties;
using System.Threading.Tasks;
using Xunit;

namespace OpenMoney.InterviewExercise.Tests
{
    public class MortgageQuoteClientFixture
    {
        private readonly Mock<IThirdPartyMortgageApi> _apiMock = new();

        [Fact]
        public async Task GetQuote_ShouldReturnHouse_IfHouseValue_Over10Mill()
        {
            const float deposit = 9_000;
            const float houseValue = 10_000_000;
            
            var mortgageClient = new MortgageQuoteClient(_apiMock.Object);
            var quote = await mortgageClient.GetQuote(new GetQuotesRequest
            {
                Deposit = deposit,
                HouseValue = houseValue
            });
            
            Assert.Equal(ErrorCode.HouseThreSholdValue, quote.Error.Code);
        }

        [Fact]
        public async Task GetQuote_ShouldReturn_AQuote()
        {
            const float deposit = 10_000;
            const float houseValue = 100_000;

            _apiMock
                .Setup(api => api.GetQuotes(It.IsAny<ThirdPartyMortgageRequest>()))
                .ReturnsAsync(new[]
                {
                    new ThirdPartyMortgageResponse { MonthlyPayment = 300m }
                });
            
            var mortgageClient = new MortgageQuoteClient(_apiMock.Object);
            var quote = await mortgageClient.GetQuote(new GetQuotesRequest
            {
                Deposit = deposit,
                HouseValue = houseValue
            });
            
            Assert.Equal(300m, (decimal)quote.MonthlyPayment);
        }

        [Fact]
        public async Task GetQuote_ShouldReturn_MinimumQuote()
        {
            const float deposit = 10_000;
            const float houseValue = 100_000;

            _apiMock
                .Setup(api => api.GetQuotes(It.IsAny<ThirdPartyMortgageRequest>()))
                .ReturnsAsync(new[]
                {
                    new ThirdPartyMortgageResponse { MonthlyPayment = 300m },
                    new ThirdPartyMortgageResponse { MonthlyPayment = 250m }
                });

            var mortgageClient = new MortgageQuoteClient(_apiMock.Object);
            var quote = await mortgageClient.GetQuote(new GetQuotesRequest
            {
                Deposit = deposit,
                HouseValue = houseValue
            });

            Assert.Equal(250m, (decimal)quote.MonthlyPayment);
        }

        [Fact]
        public async Task GetQuote_ShouldReturn_ApiNullOrEmptyError_When_ApiReturnsEmpty()
        {
            const float deposit = 10_000;
            const float houseValue = 100_000;

            _apiMock
                .Setup(api => api.GetQuotes(It.IsAny<ThirdPartyMortgageRequest>()))
                .ReturnsAsync(() => null);

            var mortgageClient = new MortgageQuoteClient(_apiMock.Object);
            var quote = await mortgageClient.GetQuote(new GetQuotesRequest
            {
                Deposit = deposit,
                HouseValue = houseValue
            });

            Assert.Equal(ErrorCode.ApiReturnNull, quote.Error.Code);
        }
    }
}