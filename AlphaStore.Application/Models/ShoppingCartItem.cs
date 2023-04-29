using System;
using AlphaStore.Application.Models;

namespace AlphaStore.Application.Models
{
    public class ShoppingCartItem : IDatabaseModel
    {
        public virtual long? Id { get; protected internal set; }
        public virtual long? ProductId { get; protected internal set; }
        public virtual long? ShoppingCartId { get; protected internal set; }
        public virtual Product Product { get; protected internal set; }
        public virtual ShoppingCart ShoppingCart { get; protected internal set; }

        protected ShoppingCartItem()
        {

        }

        protected ShoppingCartItem(
            Product product,
            ShoppingCart shoppingCart)
        {
            ProductId = product.Id;
            ShoppingCartId = shoppingCart.Id;
            Product = product;
            ShoppingCart = shoppingCart;
        }

        public static ShoppingCartItem Create(
            Product product,
            ShoppingCart shoppingCart)
        {
            return new ShoppingCartItem(
                product,
                shoppingCart);
        }
    }
}

