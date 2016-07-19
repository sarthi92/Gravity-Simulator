using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Models
{
    public class Celestial
    {
        public string Name { get; set; }
        public double Mass { get; set; }
        public double PosX { get; set; }
        public int? ConsolePosX { get; set; }
        public int? ConsolePosY { get; set; }
        public double PosY { get; set; }
        public double VeloX { get; set; }
        public double VeloY { get; set; }
    }
}
