using simApi.Base;

namespace simApi.Schema;

public class ProductResponse:BaseResponse
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Property { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public double WinEarnPoints { get; set; }
    public int MaxTotalScore { get; set; }
}
