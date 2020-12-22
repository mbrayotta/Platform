using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Operation
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int FirstArgument { get; set; }
        public int SecondArgument { get; set; }
        public int total { get; set; }

    }
}
