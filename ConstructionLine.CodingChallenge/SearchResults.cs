using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchResults
    {
        public List<Shirt> Shirts { get; set; }

        public List<SizeCount> SizeCounts { get; set; }

        public List<ColorCount> ColorCounts { get; set; }

        public SizeCount GetSizeCount(Size size)
        {
            return SizeCounts.Single(sc => sc.Size == size);
        }

        public ColorCount GetColorCount(Color color)
        {
            return ColorCounts.Single(cc => cc.Color == color);
        }
    }

    public class SizeCount
    {
        public Size Size { get; set; }

        public int Count { get; set; }
    }

    public class ColorCount
    {
        public Color Color { get; set; }

        public int Count { get; set; }
    }
}