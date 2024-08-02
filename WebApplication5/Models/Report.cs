namespace WebApplication5.Models
{
    public class Report
    {
        public Guid Id { get; set; }

        public string WorkerName { get; set; }

        public string Project { get; set; }

        public DateTime FinishDate { get; set; }

        public int HoursSpent { get; set; }
    }
}
