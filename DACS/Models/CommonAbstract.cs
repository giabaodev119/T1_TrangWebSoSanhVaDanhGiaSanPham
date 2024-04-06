namespace DACS.Models
{
    public abstract class CommonAbstract
    {
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string? ModifiedBy {  get; set; } 
    }
}
