using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DACS.Models.EF
{
    public class ProductComment : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 ký tự")]
        public int ProductId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
