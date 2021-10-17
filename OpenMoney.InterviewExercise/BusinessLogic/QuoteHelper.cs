using OpenMoney.InterviewExercise.Enums;
using OpenMoney.InterviewExercise.Models;
using OpenMoney.InterviewExercise.Models.Quotes;
using OpenMoney.InterviewExercise.ThirdParties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoney.InterviewExercise.BusinessLogic
{
    public class QuoteHelper
    {
        public static Quote GetMinimumQuoteResponse(IEnumerable<IQuoteResponse> responses)
        {
            if (responses == null)
            {
                return new Quote
                {

                    Error = new Error(

                        ErrorCode.ApiReturnNull, "Sorry, Please try out with some other data."
                    ),
                    MonthlyPayment = 1
                };
            }
            var mortgageList = responses.ToList();

            if (mortgageList.Count() < 1)
            {
                return new Quote
                {

                    Error = new Error(

                       ErrorCode.ApiReturnEmpty, "Sorry, No Mortgage Quote Found."
                   ),
                    MonthlyPayment = 1
                };
            }
            else
            {
                return new Quote
                {

                    Error = new Error(

                        ErrorCode.ApiReturnEmpty, ""
                   ),
                    MonthlyPayment = (float)responses.OrderBy(m => m.MonthlyPayment).FirstOrDefault().MonthlyPayment
                };
            }
            //{
            //        Code = ErrorCode.ApiReturnNull
            //    }
            //    }
            //    throw new Exception(ErrorCode.ApiReturnNull.ToString());
            //}

            //var mortgageList = responses.ToList();

            //if (mortgageList.Count() < 1)
            //{
            //    throw new Exception(ErrorCode.ApiReturnEmpty.ToString());                
            //}

          //  return (float)responses.OrderBy(m => m.MonthlyPayment).FirstOrDefault().MonthlyPayment;

        }
    }
}
