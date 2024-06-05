using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models.EF
{
    public class Category : CommonAbstract
    {
        public Category() 
        { 
            this.Posts = new HashSet<Post>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 ký tự")]
        public string Title { get; set; }
        public string? Alias { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}
