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

    }
}
