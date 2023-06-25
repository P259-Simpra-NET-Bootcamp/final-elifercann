using simApi.Base;
using System.ComponentModel.DataAnnotations;

namespace simApi.Schema;

public class CategoryRequest:BaseRequest
{
    [Required(ErrorMessage = "Name is a required field.")]
    [MaxLength(30, ErrorMessage = "Name must consist of at maximum 30 caharacters")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Tag is a required field.")]
    [MaxLength(30, ErrorMessage = "Tag must consist of at maximum 30 caharacters")]
    public string Tag { get; set; }
    [Required(ErrorMessage = "Url is a required field.")]
    [MaxLength(30, ErrorMessage = "Url must consist of at maximum 30 caharacters")]
    public string Url { get; set; }
    public bool IsValid { get; set; }
    public virtual List<ProductRequest> Products { get; set; }
}
