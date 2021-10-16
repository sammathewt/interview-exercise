﻿using OpenMoney.InterviewExercise.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMoney.InterviewExercise.Models
{
    public class Error
    {
        public ErrorCode Code { get; set; }
        public string Message{ get; set; }        
        public Error(ErrorCode Code , string Message)
        {
            this.Code = Code;
            this.Message = Message;
        }
    }
}
