using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
  public class SimplebarController : Controller
  {
    //
    // Default Action
    //
    public ActionResult Index()
    {
      ViewBag.Title = "Simple Bar Chart (1)";
      createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
      return View("~/Views/Shared/ChartView.cshtml");
    }

    //
    // Create chart
    //
    private void createChart(RazorChartViewer viewer)
    {
      // The data for the bar chart
      double[] data = {85, 156, 179.5, 211, 123};

      // The labels for the bar chart
      string[] labels = {"Mon", "Tue", "Wed", "Thu", "Fri"};

      // Create a XYChart object of size 250 x 250 pixels
      XYChart c = new XYChart(250, 250);

      // Set the plotarea at (30, 20) and of size 200 x 200 pixels
      c.setPlotArea(30, 20, 200, 200);

      // Add a bar chart layer using the given data
      c.addBarLayer(data);

      // Set the labels on the x axis.
      c.xAxis().setLabels(labels);

      // Output the chart
      viewer.Image = c.makeWebImage(Chart.PNG);

      // Include tool tip for the chart
      viewer.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}: {value} GBytes'");
    }
  }
}

