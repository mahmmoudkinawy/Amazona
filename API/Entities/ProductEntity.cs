﻿namespace API.Entities;
public sealed class ProductEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureUrl { get; set; }
    public ProductBrandEntity ProductBrand { get; set; }
    public int ProductBrandId { get; set; }
    public ProductTypeEntity ProductType { get; set; }
    public int ProductTypeId { get; set; }
}
