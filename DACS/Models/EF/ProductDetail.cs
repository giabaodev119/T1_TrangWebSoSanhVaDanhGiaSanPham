namespace DACS.Models.EF
{
    public class ProductDetail
    {
        private Product product { get; set; }
        private List<ProductComment> comments { get; set; }
    }
}
