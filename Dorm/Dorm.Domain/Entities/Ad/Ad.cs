using Dorm.Domain.Enum.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.Entities.Ad
{
    public class Ad
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public AdType Type { get; set; }
        public AdStatus Status { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreatedDate { get; set;}
    }
}
