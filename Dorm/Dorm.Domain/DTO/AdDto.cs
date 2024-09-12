using Dorm.Domain.Enum.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dorm.Domain.DTO
{
    public record AdDto
    (
        //[property: JsonIgnore]
        int id,
        //[property: JsonIgnore]
        int UserId,
        string Name,
        string Number,
        AdType Type,
        AdStatus Status,
        string Subject,
        string Description,
        decimal Price,
        byte[] Image,
        //[property: JsonIgnore]
        DateTime CreatedDate
    );
}
