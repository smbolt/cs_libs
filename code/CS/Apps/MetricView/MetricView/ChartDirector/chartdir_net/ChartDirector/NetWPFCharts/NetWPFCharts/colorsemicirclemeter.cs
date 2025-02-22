using System;
using ChartDirector;

namespace CSharpWPFDemo
{
  public class colorsemicirclemeter : DemoModule
  {
    //Name of demo module
    public string getName() {
      return "Color Semicircle Meters";
    }

    //Number of charts produced in this demo module
    public int getNoOfCharts() {
      return 6;
    }

    //Main code for creating charts
    public void createChart(WPFChartViewer viewer, int chartIndex)
    {
      // The value to display on the meter
      double value = 72.55;

      // The background and border colors of the meters
      int[] bgColor = {0x88ccff, 0xffdddd, 0xffddaa, 0xffccff, 0xdddddd, 0xccffcc};
      int[] borderColor = {0x000077, 0x880000, 0xee6600, 0x440088, 0x000000, 0x006000};

      // Create an AngularMeter object of size 300 x 180 pixels with transparent background
      AngularMeter m = new AngularMeter(300, 180, Chart.Transparent);

      // Center at (150, 150), scale radius = 124 pixels, scale angle -90 to +90 degrees
      m.setMeter(150, 150, 124, -90, 90);

      // Background gradient color with brighter color at the center
      double[] bgGradient = {0, m.adjustBrightness(bgColor[chartIndex], 3), 0.75, bgColor[
                               chartIndex]
                            };

      // Add a scale background of 148 pixels radius using the background gradient, with a 13
      // pixel thick border
      m.addScaleBackground(148, m.relativeRadialGradient(bgGradient), 13, borderColor[
                             chartIndex]);

      // Meter scale is 0 - 100, with major tick every 20 units, minor tick every 10 units,
      // and micro tick every 5 units
      m.setScale(0, 100, 20, 10, 5);

      // Set the scale label style to 15pt Arial Italic. Set the major/minor/micro tick
      // lengths to 16/16/10 pixels pointing inwards, and their widths to 2/1/1 pixels.
      m.setLabelStyle("Arial Italic", 16);
      m.setTickLength(-16, -16, -10);
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
        // Add the smooth color scale starting at radius 124 with zero width and ending at
        // radius 124 with 16 pixels inner width
        m.addColorScale(smoothColorScale, 124, 0, 124, -16);
      } else if (chartIndex == 2) {
        // Add the smooth color scale starting at radius 65 with zero width and ending at
        // radius 55 with 20 pixels outer width
        m.addColorScale(smoothColorScale, 65, 0, 55, 20);
      } else if (chartIndex == 3) {
        // Add the high/low color scale at the default position
        m.addColorScale(highLowColorScale);
      } else if (chartIndex == 4) {
        // Add the step color scale at the default position
        m.addColorScale(stepColorScale);
      } else {
        // Add the smooth color scale at radius 55 with 20 pixels outer width
        m.addColorScale(smoothColorScale, 55, 20);
      }

      // Add a text label centered at (150, 125) with 15pt Arial Italic font
      m.addText(150, 125, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.BottomCenter);

      // Demonstrate two different types of pointers - thin triangular pointer (the default)
      // and line pointer
      if (chartIndex % 2 == 0) {
        m.addPointer2(value, 0xff0000);
      } else {
        m.addPointer2(value, 0xff0000, -1, Chart.LinePointer2);
      }

      // Output the chart
      viewer.Chart = m;
    }
  }
}

