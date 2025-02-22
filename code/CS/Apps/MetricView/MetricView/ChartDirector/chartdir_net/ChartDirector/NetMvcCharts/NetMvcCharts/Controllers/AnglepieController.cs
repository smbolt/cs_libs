using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
  public class AnglepieController : Controller
  {
    //
    // Default Action
    //
    public ActionResult Index()
    {
      ViewBag.Title = "Start Angle and Direction";

      // This example contains 2 charts.
      ViewBag.Viewer = new RazorChartViewer[2];
      for (int i = 0; i < ViewBag.Viewer.Length; ++i)
        createChart(ViewBag.Viewer[i] = new RazorChartViewer(HttpContext, "chart" + i), i);

      return View("~/Views/Shared/ChartView.cshtml");
    }

    //
    // Create chart
    //
    private void createChart(RazorChartViewer viewer, int chartIndex)
    {
      // The data for the pie chart
      double[] data = {25, 18, 15, 12, 8, 30, 35};

      // The labels for the pie chart
      string[] labels = {"Labor", "Licenses", "Taxes", "Legal", "Insurance", "Facilities",
                         "Production"
                        };

      // Create a PieChart object of size 280 x 240 pixels
      PieChart c = new PieChart(280, 240);

      // Set the center of the pie at (140, 130) and the radius to 80 pixels
      c.setPieSize(140, 130, 80);

      // Add a title to the pie to show the start angle and direction
      if (chartIndex == 0) {
        c.addTitle("Start Angle = 0 degrees\nDirection = Clockwise");
      } else {
        c.addTitle("Start Angle = 90 degrees\nDirection = AntiClockwise");
        c.setStartAngle(90, false);
      }

      // Draw the pie in 3D
      c.set3D();

      // Set the pie data and the pie labels
      c.setData(data, labels);

      // Explode the 1st sector (index = 0)
      c.setExplode(0);

      // Output the chart
      viewer.Image = c.makeWebImage(Chart.PNG);

      // Include tool tip for the chart
      viewer.ImageMap = c.getHTMLImageMap("", "", "title='{label}: US${value}K ({percent}%)'");
    }
  }
}

