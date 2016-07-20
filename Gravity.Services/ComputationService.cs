using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.Models;

namespace Gravity.Services
{
    public class ComputationService
    {
        DataService _dataService;
        BufferService _bufferService;
        GlobalService _globalService;
        Universe _currentUniverse;
        public ComputationService(DataService dataService, BufferService bufferService, GlobalService globalService)
        {
            _dataService = dataService;
            _bufferService = bufferService;
            _globalService = globalService;
            _currentUniverse = _dataService.GetUniverse();
        }

        public Universe ComputeUniverse()
        {
            _currentUniverse = CalculateUniverseNextState(_currentUniverse);
            return _currentUniverse;
        }

        internal Universe CalculateUniverseNextState(Universe universe)
        {
            var probeList = new List<Probe>();
            foreach(var eachProbe in universe.ProbeList)
            {
                var deltaVX = 0.0;
                var deltaVY = 0.0;
                foreach(var eachCelestial in universe.ProbeList.Where(x=>x.Name != eachProbe.Name).ToList().Concat<Celestial>(universe.PlanetList).Concat(universe.StarList))
                {
                    var yDiff = eachProbe.PosY - eachCelestial.PosY;
                    var xDiff = eachProbe.PosX - eachCelestial.PosX;
                    var theta = Math.Atan2(yDiff, xDiff);
                    var sineTheta = Math.Sin(theta);
                    var cosineTheta = Math.Cos(theta);
                    deltaVX += _globalService.G * eachCelestial.Mass * cosineTheta / (xDiff * xDiff + yDiff * yDiff);
                    deltaVY += _globalService.G * eachCelestial.Mass * sineTheta / (xDiff * xDiff + yDiff * yDiff);
                }
                var mappedProbe = eachProbe;
                mappedProbe.PosX += mappedProbe.VeloX;
                mappedProbe.PosY += mappedProbe.VeloY;
                mappedProbe.VeloX += deltaVX;
                mappedProbe.VeloY += deltaVY;
                probeList.Add(mappedProbe);
            }
            var planetList = new List<Planet>();
            foreach (var eachPlanet in universe.PlanetList)
            {
                var deltaVX = 0.0;
                var deltaVY = 0.0;
                foreach (var eachCelestial in universe.ProbeList.Concat<Celestial>(universe.PlanetList.Where(x => x.Name != eachPlanet.Name).ToList()).Concat(universe.StarList))
                {
                    var yDiff = eachPlanet.PosY - eachCelestial.PosY;
                    var xDiff = eachPlanet.PosX - eachCelestial.PosX;
                    var theta = Math.Atan2(yDiff, xDiff);
                    var sineTheta = Math.Sin(theta);
                    var cosineTheta = Math.Cos(theta);
                    deltaVX += _globalService.G * eachCelestial.Mass * cosineTheta / (xDiff * xDiff + yDiff * yDiff);
                    deltaVY += _globalService.G * eachCelestial.Mass * sineTheta / (xDiff * xDiff + yDiff * yDiff);
                }
                var mappedPlanet = eachPlanet;
                mappedPlanet.PosX += mappedPlanet.VeloX;
                mappedPlanet.PosY += mappedPlanet.VeloY;
                mappedPlanet.VeloX += deltaVX;
                mappedPlanet.VeloY += deltaVY;
                planetList.Add(mappedPlanet);
            }
            var starList = new List<Star>();
            foreach (var eachStar in universe.StarList)
            {
                var deltaVX = 0.0;
                var deltaVY = 0.0;
                foreach (var eachCelestial in universe.ProbeList.Concat<Celestial>(universe.PlanetList).Concat(universe.StarList.Where(x=>x.Name != eachStar.Name).ToList()))
                {
                    var yDiff = eachStar.PosY - eachCelestial.PosY;
                    var xDiff = eachStar.PosX - eachCelestial.PosX;
                    var theta = Math.Atan2(yDiff, xDiff);
                    var sineTheta = Math.Sin(theta);
                    var cosineTheta = Math.Cos(theta);
                    deltaVX += _globalService.G * eachCelestial.Mass * cosineTheta / (xDiff * xDiff + yDiff * yDiff);
                    deltaVY += _globalService.G * eachCelestial.Mass * sineTheta / (xDiff * xDiff + yDiff * yDiff);
                }
                var mappedStar = eachStar;
                mappedStar.PosX += mappedStar.VeloX;
                mappedStar.PosY += mappedStar.VeloY;
                mappedStar.VeloX += deltaVX;
                mappedStar.VeloY += deltaVY;
                starList.Add(mappedStar);
            }
            universe.ProbeList = probeList;
            universe.PlanetList = planetList;
            universe.StarList = starList;
            return universe;
        }
    }
}
