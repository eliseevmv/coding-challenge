using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class IndexBySizeAndColor
    {
        private Dictionary<Size, Dictionary<Color, List<Shirt>>> _data;

        public IndexBySizeAndColor(List<Shirt> shirts)
        {
            _data = new Dictionary<Size, Dictionary<Color, List<Shirt>>>();

            foreach (var size in Size.All)
            {
                _data[size] = new Dictionary<Color, List<Shirt>>();

                foreach (var color in Color.All)
                {
                    _data[size][color] = new List<Shirt>();
                }
            }

            foreach (var shirt in shirts)
            {
                _data[shirt.Size][shirt.Color].Add(shirt);
            }
        }

        public Dictionary<Color, List<Shirt>> GetShirtsSplitBySize(Size size)
        {
            return _data[size];
        }
    }
}
