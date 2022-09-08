using System;

namespace Base.Exceptions
{
    public class NotFoundException : Exception
    {
        public long Id { get; set; }
        public Type Entity { get; set; }

        public NotFoundException(long id, Type entity) : base(ConfigMessage(id, entity))
        {
            Id = id;
            Entity = entity;
        }

        private static string ConfigMessage(long id, Type entity) 
            => $"Item{entity.Name} # {id} not found";
    }
}