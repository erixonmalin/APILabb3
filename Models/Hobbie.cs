using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace APILabb3.Models
{
    public class Hobbie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HobbieId { get; set; }

        [Required]
        [StringLength(40)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(70)]
        public string Summary { get; set; }

       
        [ForeignKey("Persons")]
        public int? FK_PersonId { get; set; } = null;
        public Person? Persons { get; set; } 
        public ICollection<Link>? Links { get; set; } 
    }
}
