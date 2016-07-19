using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.Models;
using Gravity.Services;

namespace Gravity.Runtime
{
    class Program
    {
        static void Main(string[] args)
        {
            ErrorService _errorService = new ErrorService();
            ReportingService _reportingService = new ReportingService();
            GlobalService _globalService = new GlobalService();
            ScriptService _scriptService = new ScriptService(_errorService);
            CommandLogService _cmdLogService = new CommandLogService("log.txt", _errorService);
            ApiService _apiService = new ApiService(_globalService, _scriptService, _cmdLogService, _errorService, _reportingService);
            CommandService _cmdService = new CommandService(_globalService, _apiService, _errorService);

            string str;
            for(;;)
            {
                Console.Write(">");
                str = Console.ReadLine();
                _cmdService.ExtractCommand(str);
            }
        }
    }
}
