using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.ViewModels.Image
{
    public class ResponseImageDeleteVM
    {
        public Guid idProductDetail { get; set; }
        public List<string>? lstImageRemove { get; set; }
    }
}
