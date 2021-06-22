using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool IsSuceedded { get; set; }
        public string Message { get; set; }

        public OperationResult()
        {
            IsSuceedded = false;
        }


        public OperationResult Succeeded(string message = "عملیات با موفقیت انجام شد.")
        {
            IsSuceedded = true;
            Message = message;
            return this;
        }

        public OperationResult Failed(string message)
        {
            IsSuceedded = false;
            Message = message;
            return this;
        }

    }
}
