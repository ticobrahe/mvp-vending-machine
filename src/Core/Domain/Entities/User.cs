namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal Deposit { get; set; }
        public string Role { get; set; }
        public bool IsLogin { get; set; }
    }
}
