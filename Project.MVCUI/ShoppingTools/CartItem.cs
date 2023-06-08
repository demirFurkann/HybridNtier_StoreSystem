using Newtonsoft.Json;

namespace Project.MVCUI.ShoppingTools
{
    [Serializable]
    public class CartItem
    {
        [JsonProperty("ID")]

        public int ID { get; set; }
        [JsonProperty("Name")]

        public string Name { get; set; }
        [JsonProperty("Amount")]

        public short Amount { get; set; }
        [JsonProperty("Price")]

        public decimal Price { get; set; }
        [JsonProperty("SubTotal")]

        public decimal SubTotal
        {
            get
            {
                return Amount * Price;
            }
        }

        public CartItem()
        {
            Amount++;
        }
    }
}
