using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class IndexByColor
    {
        private readonly Dictionary<Color, List<Shirt>> _data;

        public IndexByColor(List<Shirt> shirts)
        {
            _data = new Dictionary<Color, List<Shirt>>();

            foreach (var color in Color.All)
            {
                _data[color] = new List<Shirt>();
            }

            foreach (var shirt in shirts)
            {
                _data[shirt.Color].Add(shirt);
            }
        }

        public List<Shirt> GetShirts(Color color)
        {
            return _data[color];
        }
    }
}
