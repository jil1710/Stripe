namespace Stripe.Models
{
    public class ProductEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public long Quantity { get; set; }
        public string Photo { get; set; }
    }
}
