using Newtonsoft.Json;

namespace Project.MVCUI.ShoppingTools
{
    [Serializable]
    public class Cart
    {
        [JsonProperty("_sepetim")]
        Dictionary<int, CartItem> _sepetim;

        public Cart()
        {
            _sepetim = new Dictionary<int, CartItem>();
        }
        [JsonProperty("Sepetim")]

        public List<CartItem> Sepetim
        {
            get
            {
                return _sepetim.Values.ToList();
            }
        }

        public void SepeteEkle(CartItem item)
        {
            if (_sepetim.ContainsKey(item.ID))
            {
                _sepetim[item.ID].Amount++;
                return;
            }
            _sepetim.Add(item.ID, item);
        }

        public void SepettenCikar(int id)
        {
            if (_sepetim[id].Amount > 0)
            {
                _sepetim[id].Amount--;
                return;
            }
            _sepetim.Remove(id);
        }
        [JsonProperty("TotalPrice")]

        public decimal TotalPrice
        {
            get
            {
                return _sepetim.Sum(x => x.Value.SubTotal);
            }
        }
    }
}
