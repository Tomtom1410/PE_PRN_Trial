using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    public class CustomerDeleteDto
    {
        public int CustomerDeleteCount { get; set; }
        public int OrderDeleteCount { get; set; }
        public int OrderDetailDeleteCount { get; set; }
    }
}
