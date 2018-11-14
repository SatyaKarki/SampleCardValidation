using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Card.Helper
{
  public  class ResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public dynamic output { get; set; }
    }
}
