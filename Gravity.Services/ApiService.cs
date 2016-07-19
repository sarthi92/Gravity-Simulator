using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.Models;
using Gravity.Models.Enums;

namespace Gravity.Services
{
    public class ApiService
    {
        Universe _universe;
        GlobalService _globalService;
        ScriptService _scriptService;
        CommandLogService _cmdLogService;
        ErrorService _errorService;
        ReportingService _reportingService;
        public ApiService(GlobalService globalService, ScriptService scriptService, CommandLogService cmdLogService, ErrorService errorService, ReportingService reportingService)
        {
            _universe = new Universe();
            _universe.StarList = new List<Star>();
            _universe.PlanetList = new List<Planet>();
            _universe.ProbeList = new List<Probe>();
            _globalService = globalService;
            _scriptService = scriptService;
            _cmdLogService = cmdLogService;
            _errorService = errorService;
            _reportingService = reportingService;
        }

        public void Post(ObjType type, Celestial obj, object cmdJSON)
        {
            switch(type)
            {
                case ObjType.STAR: _universe.StarList.Add(obj as Star); break;
                case ObjType.PLANET: _universe.PlanetList.Add(obj as Planet); break;
                case ObjType.PROBE: _universe.ProbeList.Add(obj as Probe); break;
                default: return;
            }
            _cmdLogService.SaveLog(cmdJSON);
            _reportingService.ObjectAdded();
        }

        public void Put(string[] cmdParts, object cmdJSON)
        {
            var isObjStar = _universe.StarList.FirstOrDefault(x => x.Name == cmdParts[1]);
            if (isObjStar != null)
            {
                isObjStar = UpdateObject(cmdParts, isObjStar, ref cmdJSON) as Star;
                cmdJSON.GetType().GetProperty("Object").SetValue(cmdJSON, ObjType.STAR);
                _cmdLogService.SaveLog(cmdJSON);
                _reportingService.ObjectUpdated();
                return;
            }
            var isObjPlanet = _universe.PlanetList.FirstOrDefault(x => x.Name == cmdParts[1]);
            if(isObjPlanet != null)
            {
                isObjPlanet = UpdateObject(cmdParts, isObjPlanet, ref cmdJSON) as Planet;
                cmdJSON.GetType().GetProperty("Object").SetValue(cmdJSON, ObjType.PLANET);
                _cmdLogService.SaveLog(cmdJSON);
                _reportingService.ObjectUpdated();
                return;
            }
            var isObjProbe = _universe.ProbeList.FirstOrDefault(x => x.Name == cmdParts[1]);
            if(isObjProbe != null)
            {
                isObjProbe = UpdateObject(cmdParts, isObjProbe, ref cmdJSON) as Probe;
                cmdJSON.GetType().GetProperty("Object").SetValue(cmdJSON, ObjType.PROBE);
                _cmdLogService.SaveLog(cmdJSON);
                _reportingService.ObjectUpdated();
                return;
            }
            _errorService.ThrowObjectNotFoundError();
        }

        internal Celestial UpdateObject(string[] cmdParts, Celestial obj, ref object cmdJSON)
        {
            double dblResult;
            switch (cmdParts[2].ToLower())
            {
                case "mass":
                    if (Double.TryParse(cmdParts[3], out dblResult))
                    { obj.Mass = dblResult; cmdJSON.GetType().GetProperty("Mass").SetValue(cmdJSON, obj.Mass); }
                    else
                    { _errorService.ThrowBadRequestError(); }
                    break;
                case "posx":
                    if (Double.TryParse(cmdParts[3], out dblResult))
                    { obj.PosX = dblResult; cmdJSON.GetType().GetProperty("PosX").SetValue(cmdJSON, obj.PosX); }
                    else
                    { _errorService.ThrowBadRequestError(); }
                    break;
                case "posy":
                    if (Double.TryParse(cmdParts[3], out dblResult))
                    { obj.PosY = dblResult; cmdJSON.GetType().GetProperty("PosY").SetValue(cmdJSON, obj.PosY); }
                    else
                    { _errorService.ThrowBadRequestError(); }
                    break;
                case "velox":
                    if (Double.TryParse(cmdParts[3], out dblResult))
                    { obj.VeloX = dblResult; cmdJSON.GetType().GetProperty("VeloX").SetValue(cmdJSON, obj.VeloX); }
                    else
                    { _errorService.ThrowBadRequestError(); }
                    break;
                case "veloy":
                    if (Double.TryParse(cmdParts[3], out dblResult))
                    { obj.VeloY = dblResult; cmdJSON.GetType().GetProperty("VeloY").SetValue(cmdJSON, obj.VeloY); break; }
                    else
                    { _errorService.ThrowBadRequestError(); }
                    break;
                default:
                    _errorService.ThrowBadRequestError();
                    break;
            }
            return obj;
        }

        public void Delete(string name, object cmdJSON)
        {
            var isObjStar = _universe.StarList.FirstOrDefault(x => x.Name == name);
            if (isObjStar != null)
            {
                _universe.StarList.Remove(isObjStar);
                cmdJSON.GetType().GetProperty("Object").SetValue(cmdJSON, ObjType.STAR);
                _cmdLogService.SaveLog(cmdJSON);
                _reportingService.ObjectDeleted();
                return;
            }
            var isObjPlanet = _universe.PlanetList.FirstOrDefault(x => x.Name == name);
            if (isObjPlanet != null)
            {
                _universe.PlanetList.Remove(isObjPlanet);
                cmdJSON.GetType().GetProperty("Object").SetValue(cmdJSON, ObjType.PLANET);
                _cmdLogService.SaveLog(cmdJSON);
                _reportingService.ObjectDeleted();
                return;
            }
            var isObjProbe = _universe.ProbeList.FirstOrDefault(x => x.Name == name);
            if (isObjProbe != null)
            {
                _universe.ProbeList.Remove(isObjProbe);
                cmdJSON.GetType().GetProperty("Object").SetValue(cmdJSON, ObjType.PROBE);
                _cmdLogService.SaveLog(cmdJSON);
                _reportingService.ObjectDeleted();
                return;
            }
            _errorService.ThrowObjectNotFoundError();
        }

        public void SaveUniverse(object cmdJSON)
        {
            _globalService.Save(_universe);
            _cmdLogService.SaveLog(cmdJSON);
            _reportingService.UniverseSaved();
        }

        public void LoadUniverse(object cmdJSON)
        {
            _universe =_globalService.Load();
            _cmdLogService.SaveLog(cmdJSON);
            _reportingService.TempLoaded();
        }

        public void SaveScript(object cmdJSON)
        {
            _scriptService.SaveScript(_universe);
            _cmdLogService.SaveLog(cmdJSON);
            _reportingService.ScriptSaved();
        }

        public void LoadScript(string path, object cmdJSON)
        {
            _universe = _scriptService.LoadScript(path);
            _cmdLogService.SaveLog(cmdJSON);
            _reportingService.ScriptLoaded();
        }

        public void Reset(object cmdJSON)
        {
            _universe = new Universe();
            _universe.StarList = new List<Star>();
            _universe.PlanetList = new List<Planet>();
            _universe.ProbeList = new List<Probe>();
            _cmdLogService.SaveLog(cmdJSON);
            _reportingService.TempReset();
        }

        public void RecordUniverse(object cmdJSON)
        {
            _globalService.MODE = CurrentMode.RECORD;
            _cmdLogService.SaveLog(cmdJSON);
            _reportingService.RecordMode();
        }
    }
}
