using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.Models;

namespace Gravity.Services
{
    public class ErrorService
    {
        public void ThrowBadRequestError()
        {
            Console.WriteLine(Gravity.Models.Errors.ErrorMessages.COMMAND_INVALID);
        }

        public void ThrowScriptNotFoundError()
        {
            Console.WriteLine(Gravity.Models.Errors.ErrorMessages.SCRIPT_NOT_FOUND);
        }

        public void ThrowLogFileNotFoundError()
        {
            Console.WriteLine(Gravity.Models.Errors.ErrorMessages.LOGFILE_ABSENT);
        }

        public void ThrowObjectNotFoundError()
        {
            Console.WriteLine(Gravity.Models.Errors.ErrorMessages.OBJECT_NOT_FOUND);
        }
    }
}
