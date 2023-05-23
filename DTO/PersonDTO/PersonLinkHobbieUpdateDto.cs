using Microsoft.Identity.Client;

namespace APILabb3.DTO.PersonDTO
{
    public class PersonLinkHobbieUpdateDto
    {
        public int HobbieId { get; set; }
        public int LinkId { get; set; }
        public string LinkName { get; set; }
        public string Url { get; set; }
    }
}
