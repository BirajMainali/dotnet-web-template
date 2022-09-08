using System.Collections.Generic;

namespace Base.Application
{
    public class Pagination<T>
    {
        public readonly IEnumerable<T> Collection;
        public readonly int TotalCollectionSize;
        public readonly int CurrentPage;
        public readonly int Limit;

        public Pagination(IEnumerable<T> collection, int totalCollectionSize, int currentPage, int limit)
        {
            Collection = collection;
            TotalCollectionSize = totalCollectionSize;
            CurrentPage = currentPage;
            Limit = limit;
        }
    }
}