using Sprout.Exam.DataAccess.Abstracts;

namespace Sprout.Exam.DataAccess.Models
{
    public class Payroll : Entity<int>
    {
        public int EmployeeId { get; set; }
        public int TypeId { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal DailyRate { get; set; }
        public decimal DaysAbsent { get; set; }
        public decimal WorkedDays { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
    }
}
