using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModels.Blog
{
    public class BlogDTO
    {
        public string? TieuDe { get; set; }

        public string? NoiDung { get; set; }

        public string? MoTaNgan { get; set; }

    }
}
