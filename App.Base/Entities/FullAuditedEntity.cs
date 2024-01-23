#nullable enable
using System;
using App.Base.Constants;

namespace App.Base.Entities
{
    public abstract class FullAuditedEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public char RecStatus { get; set; } = Status.Active;
        public TKey CreatedBy { get; set; }
        public TKey UpdatedBy { get; set; }
    }
}