using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    public class Checkout : ICheckout
    {
        //  store the pricing rules and scanned items in a list
        private readonly List<PricingRule> pricingRules;
        private readonly List<ScannedItem> scannedItems;

        // constructor to recieve pricingRules and initialize a new list for scanned items
        public Checkout(List<PricingRule> pricingRules)
        {
            this.pricingRules = pricingRules;
            this.scannedItems = new List<ScannedItem>();
        }


        public void Scan(string item)
        {
            var pricingRule = GetPricingRule(item);

            if (pricingRule != null)
            {
                var scannedItem = GetScannedItem(item);

                if (scannedItem != null) 
                    scannedItem.Quantity++;
                
                else 
                    scannedItems.Add(new ScannedItem(item));

            }
            else Console.WriteLine($"Error: Item '{item}' not found in pricing rules.");
            
        }
        public int GetTotalPrice()
        {
            int totalPrice = 0;
        }
            return totalPrice;
    }

        private ScannedItem GetScannedItem(string item)
        {
            return scannedItems.FirstOrDefault(scannedItem => scannedItem.Item == item);
        }
    public class PricingRule
    {
        public string Item { get; }
        public int UnitPrice { get; }
        public (int Quantity, int SpecialPrice) SpecialOffer { get; }
    }
    public class ScannedItem
    {
        public string Item { get; }
        public int Quantity { get; set; }


        //constructor to be used by scan method
        public ScannedItem(string item)
        {
            Item = item;
            Quantity = 1;
        }
    }
}
