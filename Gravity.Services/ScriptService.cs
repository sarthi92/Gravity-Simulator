using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Gravity.Models;
using Newtonsoft.Json;

namespace Gravity.Services
{
    public class ScriptService
    {
        string _path;
        ErrorService _errorService;
        public ScriptService(ErrorService errorService)
        {
            _errorService = errorService;
        }

        public Universe LoadScript(string path)
        {
            _path = path;
            TextReader reader = null;
            try
            {
                reader = new StreamReader(_path);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Universe>(fileContents);
            }
            catch
            {
                File.CreateText(_path);
                return new Universe();
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public void SaveScript(Universe universe)
        {
            if(_path == string.Empty || _path == null) { _errorService.ThrowScriptNotFoundError(); return; }
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(universe);
                writer = new StreamWriter(_path, true);
                writer.Write(contentsToWriteToFile);
                _path = string.Empty;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
