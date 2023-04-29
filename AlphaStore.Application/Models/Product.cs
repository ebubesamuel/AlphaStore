
using System;
using AlphaStore.Application.Models;

namespace AlphaStore.Application.Models
{
    public class Product : IDatabaseModel
    {
        public virtual long? Id { get; protected internal set; }
        public virtual string Name { get; protected internal set; }
        public virtual string Description { get; protected internal set; }
        public virtual ProductStatus Status { get; protected internal set; }
        public virtual int Quanitity { get; protected internal set; }
        public virtual string FileId { get; protected internal set; }
        public virtual long? CategoryId { get; protected internal set; }
        public virtual long? PriceId { get; protected internal set; }
        public virtual Category Category { get; protected internal set; }
        public virtual Price Price { get; protected internal set; }

        protected Product()
        {

        }

        protected Product(
            string name,
            string description,
            long? categoryId,
            int quantity,
            Price price)
        {
            if (categoryId is null
                || categoryId < default(long))
            {
                throw new ArgumentNullException(
                    nameof(categoryId),
                    "Product category can't be null or negative");
            }

            CategoryId = categoryId;

            ChangePrice(price);

            Update(
                name: name,
                description: description,
                quantity: quantity);
        }

        public static Product Create(string name,
            string description,
            long? categoryId,
            int quantity,
            Price price)
        {
            return new Product(
                name: name,
               description: description,
               categoryId: categoryId,
               quantity: quantity,
               price: price);
        }

        public void ChangePrice(
            Price price)
        {
            if (price is null)
            {
                throw new Exception("You can't set a null price!");
            }

            Price = price;
        }

        public void Update(
            string name,
            string description,
            int quantity)
        {
            if (name == null)
            {
                throw new ArgumentNullException(
                    nameof(name),
                    "Product name can't be null");
            }

            if (description == null
                || description.Length > 256)
            {
                throw new ArgumentNullException(
                    nameof(description),
                    "*** Invalid description. *** " +
                    "Description of product can't be null or omre than 256 characters");
            }

            if (quantity < default(int))
            {
                throw new ArgumentNullException(
                    nameof(quantity),
                    "Invalid quantity. " +
                    "Quantity should be a positive number or zero");
            }

            Name = name;
            Description = description;
            Quanitity = quantity;
        }
    }
}
