namespace _0_Framework.Application
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string UserSystem = "2";
        public const string ContentUploader = "3";
        public const string ColleagueUser = "6";
        

        public static string GetRoleBy(long id)
        {
            return id switch
            {
                1 => "مدیر سیستم",
                3 => "محتواگذار",
                6=> "کاربرهمکار",
                _ => ""
            };
        }
    }
}
