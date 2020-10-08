using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class MessageDTO
    {
        [Required]
        public IList<string> Messages { get; set; } = new List<string>();
        

        public MessageDTO()
        {
            
        }

        public MessageDTO(params string[] messages)
        {
            Messages = messages;
        }
    }
}