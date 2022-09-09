using System.Collections.Generic;

namespace App.Base.ValueObject
{
    public class PagedResult<T>
    {
        public readonly IEnumerable<T> Collection;
        public readonly int TotalCollectionSize;
        public readonly int CurrentPage;
        public readonly int Limit;

        public PagedResult(IEnumerable<T> collection, int totalCollectionSize, int currentPage, int limit)
        {
            Collection = collection;
            TotalCollectionSize = totalCollectionSize;
            CurrentPage = currentPage;
            Limit = limit;
        }
    }
}