using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class World
    {
        private List<Location> _locations = new List<Location>();
        internal void AddLocation(int xCoord,int yCoord,string name,string description,string imageName)
        {
            Location loc = new Location();
            loc.XCoordinate = xCoord;
            loc.YCoordinate = yCoord;
            loc.Name = name;
            loc.Description = description;
            loc.ImageName = imageName;
            _locations.Add(loc);
        }
        public Location LocationAt(int xCoord,int yCoord)
        {
            foreach(Location loc in _locations)
            {
                if(loc.XCoordinate==xCoord&&loc.YCoordinate==yCoord)
                {
                    return loc;
                }
            }
            return null;
        }
    }
}
