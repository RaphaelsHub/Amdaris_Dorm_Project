using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorm.BLL.Settings
{
    public class AuthSettings
    {
        public TimeSpan TimeExp { get; set; }
        public string? SecretKey {  get; set; }
    }
}
