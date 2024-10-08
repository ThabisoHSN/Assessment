using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTO
{
    public class DynamicListResponse<T>
    {
        public string Error { get; set; }
        public List<T> Result { get; set; }
    }
}
