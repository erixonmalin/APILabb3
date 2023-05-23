using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace APILabb3.DTO.HobbieDTO
{
    public class PersonHobbieCreateDto
    {
        [Required]
        [StringLength(40)]
        [DisplayName("Title of hobbie")]
        public string? HobbieTitle { get; set; }

        [Required]
        [StringLength(70)]
        [DisplayName("Summary")]
        public string? Summary { get; set; }

        public int? PersonId { get; set; } = null;
    }
}
