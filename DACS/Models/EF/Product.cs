using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DACS.Models.EF
{
    public class Product : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(250, ErrorMessage = "Không được vượt quá 150 ký tự")]
        public string Name { get; set; }
        public string? Alias { get; set; }
        public string Detail { get; set; }
        public string? AddressAndPrice { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsHome { get; set; }
        public bool IsFeature {  get; set; }
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }
        public int ProductCategoryId { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }
        public virtual ProductCategory? ProductCategory { get; set; }
    }
}
