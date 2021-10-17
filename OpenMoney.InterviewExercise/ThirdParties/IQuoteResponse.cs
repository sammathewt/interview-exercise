using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoney.InterviewExercise.ThirdParties
{
    public interface IQuoteResponse
    {
        decimal MonthlyPayment { get; set; }
    }
}
