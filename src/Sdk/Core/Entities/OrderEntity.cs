namespace Sdk.Core.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string Email { get; set; }
        public decimal MinBinWidth { get; set; }
    }
}
