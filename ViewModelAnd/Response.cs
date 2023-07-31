using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelAnd
{
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public ErrorCodeEnum ErrorCode { get; set; }
        public int IdOfStatus { get; set;
        }
    }
}
