using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Models
{
    public class Universe
    {
        public List<Star> StarList { get; set; }
        public List<Planet> PlanetList { get; set; }
        public List<Probe> ProbeList { get; set; }
    }
}
