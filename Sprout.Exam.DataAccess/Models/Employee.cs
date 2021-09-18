using Sprout.Exam.DataAccess.Abstracts;
using System;

namespace Sprout.Exam.DataAccess.Models
{
    public class Employee : Entity<int>
    {
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Tin { get; set; }
        public int EmployeeTypeId { get; set; }
    }
}
