using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class CustomException : Exception
    {
        public CustomException(string message): base(message) { }
    }
     public class CustomNullException : Exception
    {
        public CustomNullException(string message): base(message) { }
    }
}
