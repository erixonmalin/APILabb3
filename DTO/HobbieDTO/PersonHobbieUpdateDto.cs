using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace APILabb3.DTO.HobbieDTO
{
    public class PersonHobbieUpdateDto
    {
        [Required]
        [StringLength(40)]
        [DisplayName("Title")]
        public string? HobbieTitle { get; set; }

        [Required]
        [StringLength(70)]
        [DisplayName("Summary")]
        public string? HobbieSummary { get; set; }

        public int PersonId { get; set; }
    }
}
