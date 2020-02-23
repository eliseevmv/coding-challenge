using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class IndexByColorAndSize
    {
        private Dictionary<Color, Dictionary<Size, List<Shirt>>> _data;

        public IndexByColorAndSize(List<Shirt> shirts)
        {
            _data = new Dictionary<Color, Dictionary<Size, List<Shirt>>>();

            foreach (var color in Color.All)
            {
                _data[color] = new Dictionary<Size, List<Shirt>>();
                foreach (var size in Size.All)
                {
                    _data[color][size] = new List<Shirt>();
                }
            }

            foreach (var shirt in shirts)
            {
                _data[shirt.Color][shirt.Size].Add(shirt);
            }
        }

        public Dictionary<Size, List<Shirt>> GetShirts(Color color)
        {
            return _data[color];
        }

        public List<Shirt> GetShirts(Color color, Size size) 
        {
            return _data[color][size];
        }
    }
}
