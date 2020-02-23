using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class IndexBySize
    {
        private readonly Dictionary<Size, List<Shirt>> _data;

        public IndexBySize(List<Shirt> shirts)
        {
            _data = new Dictionary<Size, List<Shirt>>();

            foreach (var size in Size.All)
            {
                _data[size] = new List<Shirt>();
            }

            foreach (var shirt in shirts)
            {
                _data[shirt.Size].Add(shirt);
            }
        }

        public List<Shirt> GetShirts(Size size)
        {
            return _data[size];
        }
    }
}
