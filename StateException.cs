using System;
using System.Collections.Generic;
using System.Text;

namespace StatePattern
{
    public class StateException : ApplicationException
    {
        public StateException() { }
        public StateException(string message) : base(message) { }
    }
}
