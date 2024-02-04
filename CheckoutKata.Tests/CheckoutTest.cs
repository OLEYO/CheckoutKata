using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckoutKata.Tests
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void GetTotalPrice_SingleItemNoSpecialOffer_CorrectTotalPrice()
        {
            // Arrange
            var pricingRules = new List<PricingRule>
            {
                new PricingRule("A", 50, (0, 0))
            };

            var checkout = new Checkout(pricingRules);

            // Act
            checkout.Scan("A");
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(50, totalPrice);
        }

        [TestMethod]
        public void GetTotalPrice_MultipleItemsWithSpecialOffer_CorrectTotalPrice()
        {
            // Arrange
            var pricingRules = new List<PricingRule>
            {
                new PricingRule("A", 50, (3, 130))
            };

            var checkout = new Checkout(pricingRules);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(130, totalPrice);
        }

        [TestMethod]
        public void GetTotalPrice_ScannedItemNotInPricingRules_WarningMessage()
        {
            // Arrange
            var pricingRules = new List<PricingRule>
            {
                new PricingRule("A", 50, (0, 0))
            };

            var checkout = new Checkout(pricingRules);

            // Act
            checkout.Scan("B");
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            // Validate that the warning message is written to the console
            StringAssert.Contains("Warning: Item 'B' not found in pricing rules.", Console.Out.ToString());
            Assert.AreEqual(0, totalPrice);
        }

        [TestMethod]
        public void GetTotalPrice_ScannedItemWithoutSpecialOffer_CorrectTotalPrice()
        {
            // Arrange
            var pricingRules = new List<PricingRule>
            {
                new PricingRule("A", 50, (0, 0))
            };

            var checkout = new Checkout(pricingRules);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(100, totalPrice);
        }
    }
}
