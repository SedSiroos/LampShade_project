namespace AccountManagement.Application.Contract.Account
{
    public class ChangePasswordAccount
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
