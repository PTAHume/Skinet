using System.ComponentModel.DataAnnotations;

namespace API.DTOs;
public class BasketItemDTO
{
	[Required]
	public int Id { get; set; }
	[Required]
	public string ProductName { get; set; }
	[Required]
	[Range(0.01, double.MaxValue, ErrorMessage = "Price must be grater than 0")]
	public decimal Price { get; set; }
	[Required]
	public string PictureUrl { get; set; }
	[Required]
    [Range(1, double.MaxValue, ErrorMessage = "Price must be grater than 0")]
	public int Quantity { get; set; }
	[Required]
	public string Brand { get; set; }
	[Required]
	public string Type { get; set; }
}
