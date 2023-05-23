namespace APILabb3.DTO.PersonDTO
{
    public class PersonHobbieLinkGetDto
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int HobbieId { get; set; }
        public string HobbieTitle { get; set; }
        public string HobbieSummary { get; set; }
        public int LinkId { get; set; }
        public string LinkName { get; set; }

        public string Url { get; set; }
    }
}
