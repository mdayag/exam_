namespace Sprout.Exam.Business.DataTransferObjects
{
    public class PayrollDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeType { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal DailyRate { get; set; }
        public decimal DaysAbsent { get; set; }
        public decimal WorkedDays { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
    }
}
