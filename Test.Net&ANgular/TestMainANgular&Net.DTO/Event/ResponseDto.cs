using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMainANgular_Net.DTO.Event
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string EventMessage { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; } // Ensure this property exists
    }


}
