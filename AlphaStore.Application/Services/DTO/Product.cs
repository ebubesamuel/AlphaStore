using System.Collections.Generic;

namespace AlphaStore.Application.Services.DTO
{
    public sealed class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quanitity { get; set; }
        public long? CategoryId { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
    }

    public sealed class UpdateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quanitity { get; set; }
    }

    public sealed class ProductDetailsDTO
    {
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quanitity { get; set; }
        public string Price { get; set; }
    }

    public sealed class ProductShortDTO
    {
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }

    public sealed class UpdateProductPriceDTO
    {
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
    }

    public sealed class ShoppingCartItemDTO
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Total { get; set; }
        public int Quantity { get; set; }
    }

    public sealed class ShoppingCartDTO
    {
        public long Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public IEnumerable<ShoppingCartItemDTO> Products { get; set; }
    }

    public sealed class ShoppingCartShortDTO
    {
        public long Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public int ProductCount { get; set; }
    }

    public sealed class CreateUpdateCartDTO
    {
        public long ProductId { get; set; }
        public int ProductCount { get; set; }
    }

    public sealed class CreateUpdateCategoryDTO
    {
        public string Name { get; set; }
    }

    public sealed class CategoryShortDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
