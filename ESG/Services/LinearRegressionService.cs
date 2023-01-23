using ESG.Models;

namespace ESG.Services
{
    // followed formula from https://www.mathsisfun.com/data/least-squares-regression.html
    public static class LinearRegressionService
    {
        // First shot at it - I decided to treat the earliest time in the dataset as x=0 and then use the number of seconds since that time as the x values
        public static (double slope, double intercept) CalculateRegressionShifted(List<DataPoint> points)
        {
            var minDateTime = points.Min(x => x.x);

            // sum of seconds since earliest time
            double sumX = 0;

            double sumY = 0;
            double sumXY = 0;
            double sumX2 = 0;
            var count = 0;

            foreach(DataPoint point in points)
            {
                // difference in seconds since earliest point
                double deltaX = (point.x - minDateTime).TotalSeconds;

                sumX += deltaX;
                sumY += point.y;

                sumXY += deltaX * point.y;
                sumX2 += deltaX * deltaX;

                count++;
            }

            double slope = ((count * sumXY) - (sumX * sumY)) / ((count * sumX2) - (sumX * sumX));

            double intercept = (sumY - (slope * sumX)) / count;

            return (slope, intercept);
        }

        // for this one I used DateTime.MinVlaue as my x=0 and used number of seconds since then as the x values
        public static (double slope, double intercept) CalculateRegression(List<DataPoint> points)
        {
            var minDateTime = DateTime.MinValue;

            // sum of seconds since earliest time
            double sumX = 0;

            double sumY = 0;
            double sumXY = 0;
            double sumX2 = 0;
            var count = 0;

            foreach (DataPoint point in points)
            {
                // difference in seconds since earliest point
                double deltaX = (point.x - minDateTime).TotalSeconds;

                sumX += deltaX;
                sumY += point.y;

                sumXY += deltaX * point.y;
                sumX2 += deltaX * deltaX;

                count++;
            }

            double slope = ((count * sumXY) - (sumX * sumY)) / ((count * sumX2) - (sumX * sumX));

            double intercept = (sumY - (slope * sumX)) / count;

            return (slope, intercept);
        }
    }
}
