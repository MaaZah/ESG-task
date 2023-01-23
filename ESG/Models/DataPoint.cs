namespace ESG.Models
{
    public class DataPoint
    {
        public DataPoint(DateTime x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public DataPoint(string date, string time, double y1) 
        {
            x = DateTime.Parse(date);
            var timeSpan = TimeSpan.Parse(time);

            x = x.Date + timeSpan;
            y = y1;
        }

        public DateTime x { get; set; }
        public double y { get; set; }
    }
}
