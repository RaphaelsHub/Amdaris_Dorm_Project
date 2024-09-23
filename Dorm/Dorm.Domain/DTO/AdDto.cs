using Dorm.Domain.Enum.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dorm.Domain.DTO
{
    public class AdDto
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
        public DateTime CreatedDate { get; set; }
        public bool CanEdit { get; set; }
    }
}
