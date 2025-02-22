using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
  public class ClicklineController : Controller
  {
    //
    // Default Action
    //
    public ActionResult Index()
    {
      ViewBag.Title = "Simple Clickable Line Chart";
      createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
      return View("~/Views/Shared/ChartView.cshtml");
    }

    //
    // Create chart
    //
    private void createChart(RazorChartViewer viewer)
    {
      // Get the selected year.
      string selectedYear = Request["xLabel"];

      //
      // In real life, the data may come from a database based on selectedYear. In this example, we
      // just use a random number generator.
      //
      int seed = 50 + (int.Parse(selectedYear) - 1996) * 15;
      RanTable rantable = new RanTable(seed, 1, 12);
      rantable.setCol2(0, seed, -seed * 0.25, seed * 0.33, seed * 0.1, seed * 3);

      double[] data = rantable.getCol(0);

      //
      // Now we obtain the data into arrays, we can start to draw the chart using ChartDirector
      //

      // Create a XYChart object of size 600 x 320 pixels
      XYChart c = new XYChart(600, 360);

      // Add a title to the chart using 18pt Times Bold Italic font
      c.addTitle("Month Revenue for Star Tech for " + selectedYear, "Times New Roman Bold Italic",
                 18);

      // Set the plotarea at (60, 40) and of size 500 x 280 pixels. Use a vertical gradient color
      // from light blue (eeeeff) to deep blue (0000cc) as background. Set border and grid lines to
      // white (ffffff).
      c.setPlotArea(60, 40, 500, 280, c.linearGradientColor(60, 40, 60, 280, 0xeeeeff, 0x0000cc),
                    -1, 0xffffff, 0xffffff);

      // Add a red line (ff0000) chart layer using the data
      ChartDirector.DataSet dataSet = c.addLineLayer().addDataSet(data, 0xff0000, "Revenue");

      // Set the line width to 3 pixels
      dataSet.setLineWidth(3);

      // Use a 13 point circle symbol to plot the data points
      dataSet.setDataSymbol(Chart.CircleSymbol, 13);

      // Set the labels on the x axis. In this example, the labels must be Jan - Dec.
      string[] labels = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct",
                         "Nov", "Dec"
                        };
      c.xAxis().setLabels(labels);

      // When auto-scaling, use tick spacing of 40 pixels as a guideline
      c.yAxis().setTickDensity(40);

      // Add a title to the x axis to reflect the selected year
      c.xAxis().setTitle("Year " + selectedYear, "Times New Roman Bold Italic", 12);

      // Add a title to the y axis
      c.yAxis().setTitle("USD (millions)", "Times New Roman Bold Italic", 12);

      // Set axis label style to 8pt Arial Bold
      c.xAxis().setLabelStyle("Arial Bold", 8);
      c.yAxis().setLabelStyle("Arial Bold", 8);

      // Set axis line width to 2 pixels
      c.xAxis().setWidth(2);
      c.yAxis().setWidth(2);

      // Create the image and save it in a temporary location
      viewer.Image = c.makeWebImage(Chart.PNG);

      // Create an image map for the chart
      viewer.ImageMap = c.getHTMLImageMap(Url.Action("", "clickpie", new { year = selectedYear }),
                                          "", "title='{xLabel}: US$ {value|0}M'");
    }
  }
}

