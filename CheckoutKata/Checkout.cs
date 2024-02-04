using System;
using System.Collections.Generic;
using System.Linq;

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

                //check if the item already exists and add quantity if it does
                if (scannedItem != null) 
                    scannedItem.Quantity++;
                
                //if the item doesnt exist then add to scannedItems
                else 
                    scannedItems.Add(new ScannedItem(item));

            }
            //error if the item doesnt exist in pricing rules 
            else 
                Console.WriteLine($"Error: Item '{item}' not found in pricing rules.");
            
        }

        public int GetTotalPrice()
        {
            //explicitly set totalPrice to 0 
            int totalPrice = 0;

            //foreach scanned item add to the total price, while checking if the items qualify for special price.
            foreach (var scannedItem in scannedItems)
            {
                var pricingRule = GetPricingRule(scannedItem.Item);

                if (pricingRule != null)
                    totalPrice += pricingRule.CalculateTotalPrice(scannedItem.Quantity);

                //error when pricing rules cant be found for a scanned item
                else
                    Console.WriteLine($"Error: Pricing rules for item '{scannedItem.Item}' not found.");
                
            }

            return totalPrice;
        }

        private PricingRule GetPricingRule(string item)
        {
            return pricingRules.FirstOrDefault(rule => rule.Item == item);
        }

        private ScannedItem GetScannedItem(string item)
        {
            return scannedItems.FirstOrDefault(scannedItem => scannedItem.Item == item);
        }
    }

    public class PricingRule
    {
        public string Item { get; } //name of the item that the rule applies to
        public int UnitPrice { get; } //normal price of item
        public (int Quantity, int SpecialPrice) SpecialOffer { get; } //quantity required to get special price, and said special price

        public PricingRule(string item, int unitPrice, (int Quantity, int SpecialPrice) specialOffer)
        {
            Item = item; 
            UnitPrice = unitPrice; 
            SpecialOffer = specialOffer; 
        }

        public int CalculateTotalPrice(int quantity)
        {
            int regularPrice = quantity * UnitPrice;

            //check if the pricing rule has a special offer 
            if (SpecialOffer.Quantity > 0)
            {

                //how many items under special offer price 
                int specialPriceSets = quantity / SpecialOffer.Quantity;

                //how many items remain to be under normal price
                int remainderQuantity = quantity % SpecialOffer.Quantity;

                //calculating price total price based off items that qualified for special offer and ones that did not.
                int specialPriceTotal = (specialPriceSets * SpecialOffer.SpecialPrice) + (remainderQuantity * UnitPrice);

                return specialPriceTotal;
            }

            //return regular price if theres no special offer
            return regularPrice;
        }
    }

    public class ScannedItem
    {
        public string Item { get; } // item name
        public int Quantity { get; set; }


        //constructor to be used by scan method
        public ScannedItem(string item)
        {
            Item = item;
            Quantity = 1;
        }
    }
}
