using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
  public class AxisscaleController : Controller
  {
    //
    // Default Action
    //
    public ActionResult Index()
    {
      ViewBag.Title = "Y-Axis Scaling";

      // This example contains 5 charts.
      ViewBag.Viewer = new RazorChartViewer[5];
      for (int i = 0; i < ViewBag.Viewer.Length; ++i)
        createChart(ViewBag.Viewer[i] = new RazorChartViewer(HttpContext, "chart" + i), i);

      return View("~/Views/Shared/ChartView.cshtml");
    }

    //
    // Create chart
    //
    private void createChart(RazorChartViewer viewer, int chartIndex)
    {
      // The data for the chart
      double[] data = {5.5, 3.5, -3.7, 1.7, -1.4, 3.3};
      string[] labels = {"Jan", "Feb", "Mar", "Apr", "May", "Jun"};

      // Create a XYChart object of size 200 x 190 pixels
      XYChart c = new XYChart(200, 190);

      // Set the plot area at (30, 20) and of size 140 x 140 pixels
      c.setPlotArea(30, 20, 140, 140);

      // Configure the axis as according to the input parameter
      if (chartIndex == 0) {
        c.addTitle("No Axis Extension", "Arial", 8);
      } else if (chartIndex == 1) {
        c.addTitle("Top/Bottom Extensions = 0/0", "Arial", 8);
        // Reserve 20% margin at top of plot area when auto-scaling
        c.yAxis().setAutoScale(0, 0);
      } else if (chartIndex == 2) {
        c.addTitle("Top/Bottom Extensions = 0.2/0.2", "Arial", 8);
        // Reserve 20% margin at top and bottom of plot area when auto-scaling
        c.yAxis().setAutoScale(0.2, 0.2);
      } else if (chartIndex == 3) {
        c.addTitle("Axis Top Margin = 15", "Arial", 8);
        // Reserve 15 pixels at top of plot area
        c.yAxis().setMargin(15);
      } else {
        c.addTitle("Manual Scale -5 to 10", "Arial", 8);
        // Set the y axis to scale from -5 to 10, with ticks every 5 units
        c.yAxis().setLinearScale(-5, 10, 5);
      }

      // Set the labels on the x axis
      c.xAxis().setLabels(labels);

      // Add a color bar layer using the given data. Use a 1 pixel 3D border for the bars.
      c.addBarLayer3(data).setBorderColor(-1, 1);

      // Output the chart
      viewer.Image = c.makeWebImage(Chart.PNG);

      // Include tool tip for the chart
      viewer.ImageMap = c.getHTMLImageMap("", "", "title='ROI for {xLabel}: {value}%'");
    }
  }
}

