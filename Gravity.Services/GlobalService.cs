using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.Models;
using Gravity.Models.Enums;

namespace Gravity.Services
{
    public class GlobalService
    {
        Universe _universe;
        public double G;
        public CurrentMode MODE;

        public GlobalService()
        {
            _universe = new Universe();
            _universe.StarList = new List<Star>();
            _universe.PlanetList = new List<Planet>();
            _universe.ProbeList = new List<Probe>();
            G = 0.0000000001;
            MODE = CurrentMode.DATA;
        }

        public void Save(Universe universe)
        {
            _universe = universe;
        }

        public Universe Load()
        {
            return _universe;
        }
    }
}
