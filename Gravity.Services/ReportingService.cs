using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.Models.Messages;

namespace Gravity.Services
{
    public class ReportingService
    {
        public void ObjectAdded()
        {
            Console.WriteLine(Models.Messages.Action.OBJECT_ADDED);
        }

        public void ObjectUpdated()
        {
            Console.WriteLine(Models.Messages.Action.OBJECT_UPDATED);
        }

        public void ObjectDeleted()
        {
            Console.WriteLine(Models.Messages.Action.OBJECT_DELETED);
        }

        public void ScriptLoaded()
        {
            Console.WriteLine(Models.Messages.Action.SCRIPT_LOADED);
        }

        public void TempLoaded()
        {
            Console.WriteLine(Models.Messages.Action.TEMP_LOADED);
        }

        public void TempReset()
        {
            Console.WriteLine(Models.Messages.Action.TEMP_RESET);
        }

        public void UniverseSaved()
        {
            Console.WriteLine(Models.Messages.Action.SAVE_UNIVERSE);
        }

        public void ScriptSaved()
        {
            Console.WriteLine(Models.Messages.Action.SAVE_SCRIPT);
        }

        public void DataMode()
        {
            Console.WriteLine(Models.Messages.Action.DATA_MODE);
        }

        public void RecordMode()
        {
            Console.WriteLine(Models.Messages.Action.RECORD_MODE);
        }
    }
}
