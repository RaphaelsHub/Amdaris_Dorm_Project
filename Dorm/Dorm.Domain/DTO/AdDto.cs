using Dorm.Domain.Enum.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.DTO
{
    public record AdDto
    (
        int UserId,
        string Name,
        string Number,
        AdType Type,
        AdStatus Status,
        string Subject,
        string Description,
        decimal Price,
        byte[] Image,
        DateTime CreatedDate
    );
}
