using APILabb3.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APILabb3.DTO.LinkDTO
{
    public class LinkGetDto
    {
        public int LinkId { get; set; }
        public string LinkName { get; set; }
        public string Url { get; set; }
    }
}
