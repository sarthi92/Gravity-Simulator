using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.Models;
using Gravity.Models.Enums;

namespace Gravity.Services
{ 
    public class CommandService
    {
        GlobalService _globalService;
        ApiService _apiService;
        ErrorService _errorService;
        public CommandService(GlobalService globalService, ApiService apiService, ErrorService errorService)
        {
            _globalService = globalService;
            _apiService = apiService;
            _errorService = errorService;
        }

        public void ExtractCommand(string rawCmd)
        {
            string[] cmdParts = rawCmd.Split(' ');
            switch (cmdParts[0].ToLower())
            {
                case "add":
                    if (cmdParts.Length == 8) AddCelestial(cmdParts);
                    else _errorService.ThrowBadRequestError();
                    break;
                case "upd":
                    if (cmdParts.Length == 4) UpdateCelestial(cmdParts);
                    else _errorService.ThrowBadRequestError();
                    break;
                case "del":
                    if (cmdParts.Length == 2) DeleteCelestial(cmdParts);
                    else _errorService.ThrowBadRequestError();
                    break;
                case "lod":
                    if (cmdParts.Length == 2) LoadScript(cmdParts);
                    else _errorService.ThrowBadRequestError();
                    break;
                case "unv":
                    if (cmdParts.Length == 1) LoadUniverse(cmdParts);
                    else _errorService.ThrowBadRequestError();
                    break;
                case "cls":
                    if (cmdParts.Length == 1) Reset(cmdParts);
                    else _errorService.ThrowBadRequestError();
                    break;
                case "sav":
                    if (cmdParts.Length == 1) SaveScript(cmdParts);
                    else _errorService.ThrowBadRequestError();
                    break;
                case "exe":
                    if (cmdParts.Length == 1) SaveUniverse(cmdParts);
                    else _errorService.ThrowBadRequestError();
                    break;
                case "rec":
                    if (cmdParts.Length == 1) RecordUniverse(cmdParts);
                    else _errorService.ThrowBadRequestError();
                    break;
                default:
                    _errorService.ThrowBadRequestError();
                    break;
            }
        }

        internal void AddCelestial(string[] cmdParts)
        {
            Celestial obj;
            ObjType type;
            switch(cmdParts[1].ToLower())
            {
                case "star":
                    obj = new Star(); type = ObjType.STAR; break;
                case "planet":
                    obj = new Planet(); type = ObjType.PLANET; break;
                case "probe":
                    obj = new Probe(); type = ObjType.PROBE; break;
                default:
                    _errorService.ThrowBadRequestError(); return;
            }
            double dblResult;
            obj.Name = cmdParts[2];
            if (Double.TryParse(cmdParts[3], out dblResult))
                obj.Mass = dblResult;
            else
                _errorService.ThrowBadRequestError();
            if (Double.TryParse(cmdParts[4], out dblResult))
                obj.PosX = dblResult;
            else
                _errorService.ThrowBadRequestError();
            if (Double.TryParse(cmdParts[5], out dblResult))
                obj.PosY = dblResult;
            else
                _errorService.ThrowBadRequestError();
            if (Double.TryParse(cmdParts[6], out dblResult))
                obj.VeloX = dblResult;
            else
                _errorService.ThrowBadRequestError();
            if (Double.TryParse(cmdParts[7], out dblResult))
                obj.VeloY = dblResult;
            else
                _errorService.ThrowBadRequestError();
            _apiService.Post(type, obj, new {
                CreatedOn = DateTime.UtcNow,
                Command = CmdType.ADD,
                Object = type,
                Name = obj.Name,
                Mass = obj.Mass,
                PosX = obj.PosX,
                PosY = obj.PosY,
                VeloX = obj.VeloX,
                VeloY = obj.VeloY
            });
        }

        internal void UpdateCelestial(string[] cmdParts)
        {
            _apiService.Put(cmdParts, new {
                CreatedOn = DateTime.UtcNow,
                Command = CmdType.UPD,
                Object = ObjType.EMPTY,
                Name = cmdParts[1],
                Mass = "NAN",
                PosX = "NAN",
                PosY = "NAN",
                VeloX = "NAN",
                VeloY = "NAN"
            });
        }

        internal void DeleteCelestial(string[] cmdParts)
        {
            _apiService.Delete(cmdParts[1], new {
                CreatedOn = DateTime.UtcNow,
                Command = CmdType.DEL,
                Object = ObjType.EMPTY,
                Name = cmdParts[1]
            });
        }

        internal void SaveScript(string[] cmdParts)
        {
            _apiService.SaveScript(new {
                CreatedOn = DateTime.UtcNow,
                Command = CmdType.SAV,
            });
        }

        internal void LoadScript(string[] cmdParts)
        {
            _apiService.LoadScript(cmdParts[1], new
            {
                CreatedOn = DateTime.UtcNow,
                Command = CmdType.LOD,
                Path = cmdParts[1]
            });
        }

        internal void SaveUniverse(string[] cmdParts)
        {
            _apiService.SaveUniverse(new
            {
                CreatedOn = DateTime.UtcNow,
                Command = CmdType.EXE,
            });
        }

        internal void LoadUniverse(string[] cmdParts)
        {
            _apiService.LoadUniverse(new
            {
                CreatedOn = DateTime.UtcNow,
                Command = CmdType.UNV,
            });
        }

        internal void Reset(string[] cmdParts)
        {
            _apiService.Reset(new
            {
                CreatedOn = DateTime.UtcNow,
                Command = CmdType.CLS,
            });
        }

        internal void RecordUniverse(string[] cmdParts)
        {
            _apiService.RecordUniverse(new
            {
                CreatedOn = DateTime.UtcNow,
                Command = CmdType.REC,
            });
        }
    }
}
