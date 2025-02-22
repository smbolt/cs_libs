using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
  public class SimpleareaController : Controller
  {
    //
    // Default Action
    //
    public ActionResult Index()
    {
      ViewBag.Title = "Simple Area Chart";
      createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
      return View("~/Views/Shared/ChartView.cshtml");
    }

    //
    // Create chart
    //
    private void createChart(RazorChartViewer viewer)
    {
      // The data for the area chart
      double[] data = {30, 28, 40, 55, 75, 68, 54, 60, 50, 62, 75, 65, 75, 89, 60, 55, 53, 35, 50,
                       66, 56, 48, 52, 65, 62
                      };

      // The labels for the area chart
      string[] labels = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13",
                         "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"
                        };

      // Create a XYChart object of size 250 x 250 pixels
      XYChart c = new XYChart(250, 250);

      // Set the plotarea at (30, 20) and of size 200 x 200 pixels
      c.setPlotArea(30, 20, 200, 200);

      // Add an area chart layer using the given data
      c.addAreaLayer(data);

      // Set the labels on the x axis.
      c.xAxis().setLabels(labels);

      // Display 1 out of 3 labels on the x-axis.
      c.xAxis().setLabelStep(3);

      // Output the chart
      viewer.Image = c.makeWebImage(Chart.PNG);

      // Include tool tip for the chart
      viewer.ImageMap = c.getHTMLImageMap("", "", "title='Hour {xLabel}: Traffic {value} GBytes'");
    }
  }
}

