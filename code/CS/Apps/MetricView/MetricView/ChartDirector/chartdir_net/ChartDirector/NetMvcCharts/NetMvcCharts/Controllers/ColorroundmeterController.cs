using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
  public class ColorroundmeterController : Controller
  {
    //
    // Default Action
    //
    public ActionResult Index()
    {
      ViewBag.Title = "Color Round Meters";

      // This example contains 6 charts.
      ViewBag.Viewer = new RazorChartViewer[6];
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
      double value = 72.3;

      // The background and border colors of the meters
      int[] bgColor = {0x88ccff, 0xffdddd, 0xffddaa, 0xffccff, 0xdddddd, 0xccffcc};
      int[] borderColor = {0x000077, 0x880000, 0xee6600, 0x440088, 0x000000, 0x006000};

      // Create an AngularMeter object of size 250 x 250 pixels with transparent background
      AngularMeter m = new AngularMeter(250, 250, Chart.Transparent);

      // Demonstration two different meter scale angles
      if (chartIndex % 2 == 0) {
        // Center at (125, 125), scale radius = 111 pixels, scale angle -140 to +140 degrees
        m.setMeter(125, 125, 111, -140, 140);
      } else {
        // Center at (125, 125), scale radius = 111 pixels, scale angle -180 to +90 degrees
        m.setMeter(125, 125, 111, -180, 90);
      }

      // Background gradient color with brighter color at the center
      double[] bgGradient = {0, m.adjustBrightness(bgColor[chartIndex], 3), 0.75, bgColor[
                               chartIndex]
                            };
      // Add circle with radius 123 pixels as background using the background gradient
      m.addRing(0, 123, m.relativeRadialGradient(bgGradient));
      // Add a ring between radii 116 and 123 pixels as border
      m.addRing(116, 123, borderColor[chartIndex]);

      // Meter scale is 0 - 100, with major/minor/micro ticks every 10/5/1 units
      m.setScale(0, 100, 10, 5, 1);

      // Set the scale label style to 15pt Arial Italic. Set the major/minor/micro tick lengths to
      // 12/9/6 pixels pointing inwards, and their widths to 2/1/1 pixels.
      m.setLabelStyle("Arial Italic", 15);
      m.setTickLength(-12, -9, -6);
      m.setLineWidth(0, 2, 1, 1);

      // Demostrate different types of color scales and putting them at different positions
      double[] smoothColorScale = {0, 0x3333ff, 25, 0x0088ff, 50, 0x00ff00, 75, 0xdddd00, 100,
                                   0xff0000
                                  };
      double[] stepColorScale = {0, 0x00cc00, 60, 0xffdd00, 80, 0xee0000, 100};
      double[] highLowColorScale = {0, 0x00ff00, 70, Chart.Transparent, 100, 0xff0000};

      if (chartIndex == 0) {
        // Add the smooth color scale at the default position
        m.addColorScale(smoothColorScale);
      } else if (chartIndex == 1) {
        // Add the smooth color scale starting at radius 62 with zero width and ending at radius
        // 40 with 22 pixels outer width
        m.addColorScale(smoothColorScale, 62, 0, 40, 22);
      } else if (chartIndex == 2) {
        // Add green, yellow and red zones between radii 44 and 60
        m.addZone(0, 60, 44, 60, 0x00dd00);
        m.addZone(60, 80, 44, 60, 0xffff00);
        m.addZone(80, 100, 44, 60, 0xff0000);
      } else if (chartIndex == 3) {
        // Add the high/low color scale at the default position
        m.addColorScale(highLowColorScale);
      } else if (chartIndex == 4) {
        // Add the smooth color scale at radius 44 with 16 pixels outer width
        m.addColorScale(smoothColorScale, 44, 16);
      } else {
        // Add the step color scale at the default position
        m.addColorScale(stepColorScale);
      }

      // Add a text label centered at (125, 175) with 15pt Arial Italic font
      m.addText(125, 175, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.Center);

      // Add a readout to some of the charts as demonstration
      if (chartIndex == 0 || chartIndex == 2) {
        // Put the value label center aligned at (125, 232), using white (0xffffff) 14pt Arial
        // font on a black (0x000000) background. Set box width to 50 pixels with 5 pixels
        // rounded corners.
        ChartDirector.TextBox t = m.addText(125, 232, m.formatValue(value,
                                            "<*block,width=50,halign=center*>{value|1}"), "Arial", 14, 0xffffff,
                                            Chart.BottomCenter);
        t.setBackground(0x000000);
        t.setRoundedCorners(5);
      }

      // Add a red (0xff0000) pointer at the specified value
      m.addPointer2(value, 0xff0000);

      // Output the chart
      viewer.Image = m.makeWebImage(Chart.PNG);
    }
  }
}

