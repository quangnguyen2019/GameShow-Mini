using System;
using System.Net;

namespace DTO
{
    public class Player
    {
        public EndPoint PlayerIP { get; set; }
        public string Name { get; set; }

        // Số câu trả lời đúng liên tiếp
        public int PlayerScore { get; set; }
    }
}
