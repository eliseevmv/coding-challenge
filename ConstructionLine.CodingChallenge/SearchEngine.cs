using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly IndexByColor _indexByColor;
        private readonly IndexBySize _indexBySize;
        private readonly IndexBySizeAndColor _indexBySizeAndColor;
        private readonly IndexByColorAndSize _indexByColorAndSize; 

        public SearchEngine(List<Shirt> shirts) 
        {
            _shirts = shirts ?? new List<Shirt>();

            _indexByColor = new IndexByColor(_shirts);
            _indexBySize = new IndexBySize(_shirts);

            _indexBySizeAndColor = new IndexBySizeAndColor(_shirts);                  
            _indexByColorAndSize = new IndexByColorAndSize(_shirts);    
        }

        public SearchResults Search(SearchOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            CleanseSearchOptions(options);

            var results = new SearchResults()
            {
                Shirts = new List<Shirt>(),
                ColorCounts = Color.All.Select(c=> new ColorCount(){Color = c, Count = 0}).ToList(), 
                SizeCounts =  Size.All.Select(s => new SizeCount() {Size = s, Count =0}).ToList(),
            };
            
            if (!options.Colors.Any() && !options.Sizes.Any())
            {
                ProcessWithNoFilters(results);
            }

            if (options.Colors.Any() && !options.Sizes.Any())
            {
                ProcessWithColorFilter(options, results);
            }

            if (!options.Colors.Any() && options.Sizes.Any())
            {
                ProcessWithSizeFilter(options, results);
            }

            if (options.Colors.Any() && options.Sizes.Any())
            {
                ProcessWithColorAndSizeFilters(options, results);
            }

            // This can be refactored to command pattern eg
            //
            // foreach (var command in _commands)
            // {
            //    if (command.CanExecute(options))
            //    {
            //       command.Execute(options,results); 
            //    }
            // }

            return results;
        }

        private void CleanseSearchOptions(SearchOptions options)
        {
            options.Colors = options.Colors.Distinct().ToList();
            options.Sizes = options.Sizes.Distinct().ToList();
        }

        private void ProcessWithNoFilters(SearchResults results)
        {
            results.Shirts = _shirts;
            foreach (var size in Size.All)
            {
                results.GetSizeCount(size).Count = _indexBySize.GetShirts(size).Count;
            }
            foreach (var color in Color.All)
            {
                results.GetColorCount(color).Count = _indexByColor.GetShirts(color).Count;
            }
        }

        private void ProcessWithColorFilter(SearchOptions options, SearchResults results)
        {
            foreach (var color in options.Colors)
            {
                var shirtsFilteredByColor = _indexByColorAndSize.GetShirts(color);
                foreach (var size in shirtsFilteredByColor.Keys)  
                {
                    var shirts = shirtsFilteredByColor[size];
                    results.Shirts.AddRange(shirts);
                    results.GetSizeCount(size).Count += shirts.Count();
                    results.GetColorCount(color).Count += shirts.Count();
                }
            }
        }

        private void ProcessWithSizeFilter(SearchOptions options, SearchResults results)
        {
            foreach (var size in options.Sizes)
            {
                var shirtsFilteredBySize = _indexBySizeAndColor.GetShirtsSplitBySize(size);
                foreach (var color in shirtsFilteredBySize.Keys) 
                {
                    var shirts = shirtsFilteredBySize[color];
                    results.GetSizeCount(size).Count += shirts.Count();
                    results.GetColorCount(color).Count += shirts.Count();
                }
            }
        }

        private void ProcessWithColorAndSizeFilters(SearchOptions options, SearchResults results)
        {
            foreach (var size in options.Sizes)
            {
                foreach (var color in options.Colors)
                {
                    var shirts = _indexByColorAndSize.GetShirts(color, size);

                    results.Shirts.AddRange(shirts);
                    results.GetSizeCount(size).Count += shirts.Count();
                    results.GetColorCount(color).Count += shirts.Count();
                }
            }
        }
    }
}