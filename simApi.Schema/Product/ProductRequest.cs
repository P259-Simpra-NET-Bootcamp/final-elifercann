using simApi.Base;
using System.ComponentModel.DataAnnotations;

namespace simApi.Schema;

public class ProductRequest:BaseRequest
{
    [Required(ErrorMessage = "CategoryId is a required field.")]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Name is a required field.")]
    [MaxLength(30, ErrorMessage = "Name must consist of at maximum 30 caharacters")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Description is a required field.")]
    [MaxLength(30, ErrorMessage = "Description must consist of at maximum 30 caharacters")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Property is a required field.")]
    [MaxLength(30, ErrorMessage = "Property must consist of at maximum 30 caharacters")]
    public string Property { get; set; }
   
    public bool IsActive { get; set; }
    [Required(ErrorMessage = "WinEarnPoints is a required field.")]
    public double WinEarnPoints { get; set; }
    [Required(ErrorMessage = "Price is a required field.")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "MaxTotalScore is a required field.")]
    public int MaxTotalScore { get; set; }
}
