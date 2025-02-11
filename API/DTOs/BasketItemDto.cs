﻿namespace API.DTOs;
public sealed class BasketItemDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Price must be least 1")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one")]
    public int Quantity { get; set; }

    [Required]
    public string Brand { get; set; }

    [Required]
    public string Type { get; set; }
}