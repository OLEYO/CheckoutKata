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
        }
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


        public ScannedItem(string item)
        {
            Item = item;
            Quantity = 1;
        }
    }
}
