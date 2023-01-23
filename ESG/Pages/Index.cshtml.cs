using ESG.Models;
using ESG.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Formats.Asn1;

namespace ESG.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnGetChartData()
        {
            var dataset = new DataSets
            {
                PressureData = ReadFile("pumpdata.csv", 0, 1, 3),
                EventData = ReadFile("eventData.csv", 0, 1, 3)
            };

            return new JsonResult(dataset);
        }

        public IActionResult OnGetRegressionData()
        {
            var dataset = ReadFile("eventData.csv", 0, 1, 3);

            (double slope, double intercept) = LinearRegressionService.CalculateRegression(dataset);

            Console.WriteLine(slope + ", " + intercept);

            var x1 = dataset.Min(x => x.x);
            var x2 = dataset.Max(x => x.x);

            var x1delta = (x1 - DateTime.MinValue).TotalSeconds;
            var x2delta = (x2 - DateTime.MinValue).TotalSeconds;

            var y1 = slope * x1delta + intercept;
            var y2 = slope * x2delta + intercept;

            var result = new List<DataPoint>
            {
                new DataPoint(x1, y1),
                new DataPoint(x2, y2)
            };

            return new JsonResult(result);
        }

        public IActionResult OnGetRegressionDataShifted()
        {
            var dataset = ReadFile("eventData.csv", 0, 1, 3);

            (double slope, double intercept) = LinearRegressionService.CalculateRegressionShifted(dataset);

            Console.WriteLine(slope + ", " + intercept);

            var x1 = dataset.Min(x => x.x);
            var x2 = dataset.Max(x => x.x);

            var xdelta = (x2 - x1).TotalSeconds;

            var y1 = intercept;
            var y2 = slope * xdelta + intercept;

            var result = new List<DataPoint>
            {
                new DataPoint(x1, y1),
                new DataPoint(x2, y2)
            };

            return new JsonResult(result);
        }

        private List<DataPoint> ReadFile(string fileName, int dateIndex, int timeIndex, int valueIndex)
        {
            using StreamReader sr = new StreamReader(fileName);
            List<DataPoint> data = new List<DataPoint>();

            string currentLine;
            string headerLine = sr.ReadLine();

            while((currentLine = sr.ReadLine()) != null)
            {
                var delimitedLine = currentLine.Split(',');

                data.Add(new DataPoint(delimitedLine[dateIndex], delimitedLine[timeIndex], double.Parse(delimitedLine[valueIndex])));
            }

            return data;
        }
    }
}