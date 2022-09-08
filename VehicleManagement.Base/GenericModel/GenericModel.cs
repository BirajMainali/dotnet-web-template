#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using Base.Constants;
using Base.GenericModel.Interfaces;

namespace Base.GenericModel
{
    public class GenericModel : IGenericModel
    {
        public long Id { get; set; }
        public DateTime RecDate { get; set; } = DateTime.Now;
        public DateTime? ChangeAt { get; set; }
        [MaxLength(1)] public char RecStatus { get; set; } = Status.Active;
    }
}