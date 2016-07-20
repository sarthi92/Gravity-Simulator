using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.Models;

namespace Gravity.Services
{
    public class DataService
    {
        Universe _imagverse;
        Universe _realverse;
        GlobalService _globalService;
        public DataService(GlobalService globalService)
        {
            _globalService = globalService;
            _imagverse = new Universe();
            _realverse = new Universe();
            _imagverse.StarList = new List<Star>();
            _imagverse.PlanetList = new List<Planet>();
            _imagverse.ProbeList = new List<Probe>();
            _realverse.StarList = new List<Star>();
            _realverse.PlanetList = new List<Planet>();
            _realverse.ProbeList = new List<Probe>();
        }

        public bool AddStar(Star star)
        {
            switch (_globalService.UNIV)
            {
                case Models.Enums.CurrentUniverse.IMAGVERSE:
                    _imagverse.StarList.Add(star);
                    return true;
                case Models.Enums.CurrentUniverse.REALVERSE:
                    _realverse.StarList.Add(star);
                    return true;
                default:
                    return false;
            }
        }

        public bool AddPlanet(Planet planet)
        {
            switch (_globalService.UNIV)
            {
                case Models.Enums.CurrentUniverse.IMAGVERSE:
                    _imagverse.PlanetList.Add(planet);
                    return true;
                case Models.Enums.CurrentUniverse.REALVERSE:
                    _realverse.PlanetList.Add(planet);
                    return true;
                default:
                    return false;
            }
        }

        public bool AddProbe(Probe probe)
        {
            switch (_globalService.UNIV)
            {
                case Models.Enums.CurrentUniverse.IMAGVERSE:
                    _imagverse.ProbeList.Add(probe);
                    return true;
                case Models.Enums.CurrentUniverse.REALVERSE:
                    _realverse.ProbeList.Add(probe);
                    return true;
                default:
                    return false;
            }
        }

        public bool UpdateUniverse(Universe universe)
        {
            switch (_globalService.UNIV)
            {
                case Models.Enums.CurrentUniverse.IMAGVERSE:
                    _imagverse = universe;
                    return true;
                case Models.Enums.CurrentUniverse.REALVERSE:
                    _realverse = universe;
                    return true;
                default:
                    return false;
            }
        }

        public Universe GetUniverse()
        {
            switch (_globalService.UNIV)
            {
                case Models.Enums.CurrentUniverse.IMAGVERSE:
                    return _imagverse;
                case Models.Enums.CurrentUniverse.REALVERSE:
                    return _realverse;
                default:
                    return null;
            }
        }
    }
}
