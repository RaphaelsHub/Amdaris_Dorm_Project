using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.Domain.Responces
{
    public record BaseResponse<T>(T? Data, string? Description)
    {

        public bool Success => Data != null;
        public bool Success_ => Data == null;
    };
}
