using System;

namespace NTT.WebApi.DTOs
{
    public class MessageDTO
    {
        public Boolean state { get; set; }
        public string message { get; set; }
        public dynamic entity { get; set; }
    }
}
