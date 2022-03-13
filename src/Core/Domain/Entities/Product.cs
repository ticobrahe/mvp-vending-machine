namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public int AmountAvailable { get; set; }
        public decimal Cost { get; set; }
        public string Name { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; }
    }
}
