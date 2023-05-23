using System.ComponentModel.DataAnnotations;

namespace APILabb3.DTO.LinkDTO
{
    public class LinkPersonHobbieUpdateDto
    {
        [Required]
        [StringLength(30)]
        public string LinkName { get; set; }

        [Required]
        [StringLength(100)]
        [Url]
        public string Url { get; set; }
    }
}
