using System;
using System.ComponentModel.DataAnnotations;

namespace Sprout.Exam.DataAccess.Abstracts
{
    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
