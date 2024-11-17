namespace OHaraj.Core.Domain.Entities.Shop
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int FinalPrice { get; set; }

        // Navigation Properties
        CartId
        ProductId
    }
}
