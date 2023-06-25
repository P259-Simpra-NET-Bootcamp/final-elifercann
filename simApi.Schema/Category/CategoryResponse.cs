using simApi.Base;
using simApi.Data;

namespace simApi.Schema;

public class CategoryResponse:BaseResponse
{
    public string Name { get; set; }
    public string Tag { get; set; }
    public string Url { get; set; }
    public bool IsValid { get; set; }

    public virtual List<ProductResponse> Products { get; set; }
}
