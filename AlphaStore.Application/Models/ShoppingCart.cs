using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using AlphaStore.Application.Models;

namespace AlphaStore.Application.Models
{
    public class ShoppingCart : IDatabaseModel
    {

        public virtual long? Id { get; protected internal set; }
        public virtual decimal TotalPrice { get; protected internal set; }
        public virtual string Currency { get; protected internal set; }
        public virtual ShoppingCartStatus Status { get; protected internal set; }
        public virtual IList<ShoppingCartItem> Items { get; protected internal set; }

        protected ShoppingCart()
        {

        }

        protected ShoppingCart(
           string currency)
        {
            Status = ShoppingCartStatus.Pending;
            TotalPrice = 0;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            Items = new List<ShoppingCartItem>();
        }

        public static ShoppingCart Create(
            string currency)
        {
            return new ShoppingCart(currency);
        }

        public void AddItem(
            Product product,
            int quantity)
        {
            if (product.Price.Currency != Currency)
            {
                throw new InvalidOperationException(
                    $"Can't add product with different currency " +
                    $"[{product.Price.Currency}] <> [{Currency}]");
            }

            for (var i = 0; i < quantity; i++)
            {
                var item = ShoppingCartItem.Create(
                    product,
                    this);

                Items.Add(item);
                TotalPrice += item.Product.Price.Amount;
            }
        }
    }
}

