using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILabb3.Models
{
    public class Link
    {
        [Key]
        public int LinkId { get; set; } = 0;

        [Required]
        [StringLength(30)]
        public string LinkName { get; set; }

        [Required]
        [StringLength(100)]
        [Url]
        public string Url { get; set; }

        [ForeignKey("Hobbies")]
        public int? FK_HobbiesId { get; set; } = null;
        public virtual Hobbie? Hobbies { get; set; }
    }
}
