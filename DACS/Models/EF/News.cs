using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace DACS.Models.EF
{
    public class News : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 ký tự")]
        public string Title { get; set; }
        public string? Alias { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }

    }
}
