using System;

namespace Sprout.Exam.DataAccess
{
    public interface IEntity
    {
        bool IsDeleted { get; set; }
    }

    public interface INonViewEntity : IEntity
    {
        //public int CreatedBy { get; set; }
        //public DateTime DateCreated { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? DateUpdated { get; set; }
    }

    public interface IEntity<T> : INonViewEntity
    {
        T Id { get; set; }
    }

    public interface IEntityVM<T> : IEntityVM
    {
        T Id { get; set; }
    }
    public interface IEntityVM
    {
        int Status { get; set; }
    }
    public interface IEntityTypeVM
    {
        bool Type { get; set; }
    }

    public interface IEntityTypeVM<T> : IEntityTypeVM
    {
        T ID { get; set; }
    }
}
