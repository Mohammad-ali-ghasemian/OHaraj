namespace OHaraj.Core.Domain.Entities.Shop
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int FinalPrice { get; set; }

        // Navigation Properties
        public Cart Cart { get; set; }
        public int CartId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
