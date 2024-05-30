namespace DACS.Models.EF
{
    public class CheckItem
    {

            public int ProductId { get; set; }
            public string Name { get; set; }

        public string? AddressAndPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string Detail { get; set; }

        public double? AvgRating { get; set; }

        public int ProductCategoryId { get; set; }

    }
}
