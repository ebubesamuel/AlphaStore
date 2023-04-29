using System;
using System.Collections.Generic;
using AlphaStore.Application.Models;

namespace AlphaStore.Application.Models
{
    public class Category : IDatabaseModel
    {
        public virtual long? Id { get; set; }
        public virtual string Name { get; protected internal set; }
        public virtual IList<Product> Products { get; protected internal set; }

        protected Category()
        {
        }

        protected Category(string name)
        {
            Name = name;
        }

        public static Category Create(string name)
        {
            return new Category(name);
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}

