using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Gravity.Services
{
    public class CommandLogService
    {
        string _path;
        ErrorService _errorService;
        public CommandLogService(string path, ErrorService errorService)
        {
            _path = path;
            _errorService = errorService;
        }

        public void SaveLog(object obj)
        {
            if (_path == string.Empty || _path == null) { _errorService.ThrowLogFileNotFoundError(); return; }
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(obj);
                writer = new StreamWriter(_path, true);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
