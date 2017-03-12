using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Exceptions
{
    public class ScormException : Exception
    {
        public ScormException(string message) : base(message)
        {

        }
    }
}
