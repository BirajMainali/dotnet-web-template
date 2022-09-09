#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using App.Base.Constants;
using App.Base.GenericModel.Interfaces;

namespace App.Base.GenericModel
{
    public class GenericModel : IGenericModel
    {
        public long Id { get; set; }
        public DateTime RecDate { get; set; } = DateTime.Now;
        [MaxLength(1)] public char RecStatus { get; set; } = Status.Active;

        public DateTime? UpdatedDate { get; set; }
    }
}