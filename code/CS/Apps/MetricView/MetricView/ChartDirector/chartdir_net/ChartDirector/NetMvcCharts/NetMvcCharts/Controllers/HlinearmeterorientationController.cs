using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
  public class HlinearmeterorientationController : Controller
  {
    //
    // Default Action
    //
    public ActionResult Index()
    {
      ViewBag.Title = "H-Linear Meter Orientation";

      // This example contains 4 charts.
      ViewBag.Viewer = new RazorChartViewer[4];
      for (int i = 0; i < ViewBag.Viewer.Length; ++i)
        createChart(ViewBag.Viewer[i] = new RazorChartViewer(HttpContext, "chart" + i), i);

      return View("~/Views/Shared/ChartView.cshtml");
    }

    //
    // Create chart
    //
    private void createChart(RazorChartViewer viewer, int chartIndex)
    {
      // The value to display on the meter
      double value = 74.25;

      // Create a LinearMeter object of size 250 x 75 pixels with very light grey (0xeeeeee)
      // backgruond and a light grey (0xccccccc) 3-pixel thick rounded frame
      LinearMeter m = new LinearMeter(250, 75, 0xeeeeee, 0xcccccc);
      m.setRoundedFrame(Chart.Transparent);
      m.setThickFrame(3);

      // This example demonstrates putting the text labels at the top or bottom. This is by setting
      // the label alignment, scale position and label position.
      int[] alignment = {Chart.Top, Chart.Top, Chart.Bottom, Chart.Bottom};
      int[] meterYPos = {23, 23, 34, 34};
      int[] labelYPos = {61, 61, 15, 15};

      // Set the scale region
      m.setMeter(14, meterYPos[chartIndex], 218, 20, alignment[chartIndex]);

      // Set meter scale from 0 - 100, with a tick every 10 units
      m.setScale(0, 100, 10);

      // Add a smooth color scale at the default position
      double[] smoothColorScale = {0, 0x6666ff, 25, 0x00bbbb, 50, 0x00ff00, 75, 0xffff00, 100,
                                   0xff0000
                                  };
      m.addColorScale(smoothColorScale);

      // Add a blue (0x0000cc) pointer at the specified value
      m.addPointer(value, 0x0000cc);

      //
      // In this example, some charts have the "Temperauture" label on the left side and the value
      // readout on the right side, and some charts have the reverse
      //

      if (chartIndex % 2 == 0) {
        // Add a label on the left side using 8pt Arial Bold font
        m.addText(10, labelYPos[chartIndex], "Temperature C", "Arial Bold", 8, Chart.TextColor,
                  Chart.Left);

        // Add a text box on the right side. Display the value using white (0xffffff) 8pt Arial
        // Bold font on a black (0x000000) background with depressed rounded border.
        ChartDirector.TextBox t = m.addText(235, labelYPos[chartIndex], m.formatValue(value, "2"
                                                                                     ), "Arial Bold", 8, 0xffffff, Chart.Right);
        t.setBackground(0x000000, 0x000000, -1);
        t.setRoundedCorners(3);
      } else {
        // Add a label on the right side using 8pt Arial Bold font
        m.addText(237, labelYPos[chartIndex], "Temperature C", "Arial Bold", 8, Chart.TextColor,
                  Chart.Right);

        // Add a text box on the left side. Display the value using white (0xffffff) 8pt Arial
        // Bold font on a black (0x000000) background with depressed rounded border.
        ChartDirector.TextBox t = m.addText(11, labelYPos[chartIndex], m.formatValue(value, "2"),
                                            "Arial Bold", 8, 0xffffff, Chart.Left);
        t.setBackground(0x000000, 0x000000, -1);
        t.setRoundedCorners(3);
      }

      // Output the chart
      viewer.Image = m.makeWebImage(Chart.PNG);
    }
  }
}

