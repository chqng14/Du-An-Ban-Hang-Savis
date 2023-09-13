using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string? Ma { get; set; }
        public string? TenBlog { get; set; }
        public string? NoiDung { get; set; }
        public string? MoTa { get; set; }
    }
}
