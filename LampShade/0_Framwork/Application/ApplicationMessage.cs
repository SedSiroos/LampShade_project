using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public class ApplicationMessage
    {
        public const string DuplicatedRecord = "امکان ثبت رکورد تکراری وجود ندارد،لطفا مجددا تلاش بفرمایید.";
        public const string RecordNotFound = "رکورد با اطلاعات درخواست شده یافت نشد،مجددا تلاش بفرمایید.";
        public const string PasswordNotMatch = "پسور و تکرار آن باهم مطابقت ندارد.";
        public const string UserNotExist = "نام و پسور اشتباه است.";
        public const string PasswordNotFound = " پسور اشتباه است.";
    }
}
