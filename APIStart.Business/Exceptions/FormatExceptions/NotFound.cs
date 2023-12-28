using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace APIStart.Business.Exceptions.FormatExceptions
{
    public class NotFound : Exception
    {
        public NotFound()
        {
        }

        public NotFound(string? message) : base(message)
        {
        }


    }
}
