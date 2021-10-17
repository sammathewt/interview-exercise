using Moq;
using OpenMoney.InterviewExercise.Enums;
using OpenMoney.InterviewExercise.Models;
using OpenMoney.InterviewExercise.QuoteClients;
using OpenMoney.InterviewExercise.ThirdParties;
using System.Threading.Tasks;
using Xunit;

namespace OpenMoney.InterviewExercise.Tests
{
    public class HomeInsuranceQuoteClientFixture
    {
        private readonly Mock<IThirdPartyHomeInsuranceApi> _apiMock = new();

        [Fact]
        public async Task GetQuote_ShouldReturnNull_IfHouseValue_Over10Mill()
        {
            const float houseValue = 10_000_001;
            
            var mortgageClient = new HomeInsuranceQuoteClient(_apiMock.Object);
            var quote =await mortgageClient.GetQuote(new GetQuotesRequest
            {
                HouseValue = houseValue
            });

            Assert.Equal(ErrorCode.HouseThreSholdValue, quote.Error.Code);
        }

        [Fact]
        public async Task  GetQuote_ShouldReturn_AQuote()
        {
            const float houseValue = 100_000;

            _apiMock
                .Setup(api => api.GetQuotes(It.Is<ThirdPartyHomeInsuranceRequest>(r =>
                    r.ContentsValue == 50_000 && r.HouseValue == (decimal) houseValue)))
                .ReturnsAsync(new[]
                {
                    new ThirdPartyHomeInsuranceResponse { MonthlyPayment = 30 }
                });
            
            var mortgageClient = new HomeInsuranceQuoteClient(_apiMock.Object);
            var quote =await mortgageClient.GetQuote(new GetQuotesRequest
            {
                HouseValue = houseValue
            });
            
            Assert.Equal(30m, (decimal)quote.MonthlyPayment);
        }
        [Fact]
        public async Task GetQuote_ShouldReturn_MinimumQuote()
        {
            const float houseValue = 100_000;

            _apiMock
                .Setup(api => api.GetQuotes(It.Is<ThirdPartyHomeInsuranceRequest>(r =>
                    r.ContentsValue == 50_000 && r.HouseValue == (decimal)houseValue)))
                .ReturnsAsync(new[]
                {
                    new ThirdPartyHomeInsuranceResponse { MonthlyPayment = 30 },
                    new ThirdPartyHomeInsuranceResponse { MonthlyPayment = 20 }
                });

            var mortgageClient = new HomeInsuranceQuoteClient(_apiMock.Object);
            var quote = await mortgageClient.GetQuote(new GetQuotesRequest
            {
                HouseValue = houseValue
            });

            Assert.Equal(20m, (decimal)quote.MonthlyPayment);
        }

        [Fact]
        public async Task GetQuote_ShouldReturn_ApiNullError_When_ApiReturnsNull()
        {
            const float houseValue = 100_000;

            _apiMock
                .Setup(api => api.GetQuotes(It.Is<ThirdPartyHomeInsuranceRequest>(r =>
                    r.ContentsValue == 50_000 && r.HouseValue == (decimal)houseValue)))
                .ReturnsAsync(() => null);

            var mortgageClient = new HomeInsuranceQuoteClient(_apiMock.Object);
            var quote = await mortgageClient.GetQuote(new GetQuotesRequest
            {
                HouseValue = houseValue
            });

            Assert.Equal(ErrorCode.ApiReturnNull, quote.Error.Code);
        }
    }
}