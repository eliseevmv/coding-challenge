using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchEngineTestsBase
    {
        // I have updated this method because it did not fail the tests even when the results were wrong.
        protected static void AssertResults(List<Shirt> shirts, SearchOptions options, List<Shirt> results)
        {
            Assert.That(shirts, Is.Not.Null);

            var resultingShirtIds = results.Select(s => s.Id).ToList();
            var sizeIds = options.Sizes.Select(s => s.Id).ToList();
            var colorIds = options.Colors.Select(c => c.Id).ToList();

            foreach (var shirt in shirts)
            {
                if (sizeIds.Contains(shirt.Size.Id)
                    && colorIds.Contains(shirt.Color.Id)
                    && !resultingShirtIds.Contains(shirt.Id))
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size.Name}' and Color '{shirt.Color.Name}' not found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }

            foreach (var result in results)
            {
                if (
                    (colorIds.Any() && !colorIds.Contains(result.Color.Id)) ||
                    (sizeIds.Any() && !sizeIds.Contains(result.Size.Id)) )
                {
                    Assert.Fail($"Unexpected shirt '{result.Name}' with Size '{result.Size.Name}' and Color '{result.Color.Name}' found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }
        }


        protected static void AssertSizeCounts(List<Shirt> shirts, SearchOptions searchOptions, List<SizeCount> sizeCounts)
        {
            Assert.That(sizeCounts, Is.Not.Null);

            foreach (var size in Size.All)
            {
                var sizeCount = sizeCounts.SingleOrDefault(s => s.Size.Id == size.Id);
                Assert.That(sizeCount, Is.Not.Null, $"Size count for '{size.Name}' not found in results");

                // I have updated this logic because the original implementation was not correct. 
                var expectedSizeCount = shirts
                    .Count(shirt => shirt.Size.Id == size.Id
                            && (!searchOptions.Sizes.Any() || searchOptions.Sizes.Select(s => s.Id).Contains(shirt.Size.Id))    
                            && (!searchOptions.Colors.Any() || searchOptions.Colors.Select(c => c.Id).Contains(shirt.Color.Id)));

                Assert.That(sizeCount.Count, Is.EqualTo(expectedSizeCount),
                    $"Size count for '{sizeCount.Size.Name}' showing '{sizeCount.Count}' should be '{expectedSizeCount}'");
            }
        }


        protected static void AssertColorCounts(List<Shirt> shirts, SearchOptions searchOptions, List<ColorCount> colorCounts)
        {
            Assert.That(colorCounts, Is.Not.Null);
            
            foreach (var color in Color.All)
            {
                var colorCount = colorCounts.SingleOrDefault(s => s.Color.Id == color.Id);
                Assert.That(colorCount, Is.Not.Null, $"Color count for '{color.Name}' not found in results");

                // I have updated this logic because the original implementation was not correct. 
                // According to the requirements
                //
                //      The search specifies a range of sizes and colors in SearchOptions.cs. 
                //      For example, for small, medium and red the search engine should return shirts 
                //      that are either small or medium in size AND are red in color.
                //
                // As far as I understand, if the full list of shirts has no red shirts at all, 
                // then for "small, medium and red" search options the search engine should return 0 results.
                // However the origiinal version of this code worked in a different way and would return value > 0 

                var expectedColorCount = shirts
                    .Count(shirt => shirt.Color.Id == color.Id
                                    && ( !searchOptions.Colors.Any() || searchOptions.Colors.Select(c => c.Id).Contains(shirt.Color.Id)) 
                                    && ( !searchOptions.Sizes.Any() || searchOptions.Sizes.Select(s => s.Id).Contains(shirt.Size.Id) ));

                Assert.That(colorCount.Count, Is.EqualTo(expectedColorCount),
                    $"Color count for '{colorCount.Color.Name}' showing '{colorCount.Count}' should be '{expectedColorCount}'");
            }
        }
    }
}