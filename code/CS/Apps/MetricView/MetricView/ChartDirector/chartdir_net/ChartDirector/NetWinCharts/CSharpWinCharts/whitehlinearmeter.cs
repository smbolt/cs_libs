using System;
using ChartDirector;

namespace CSharpChartExplorer
{
  public class whitehlinearmeter : DemoModule
  {
    //Name of demo module
    public string getName() {
      return "White Horizontal Linear Meters";
    }

    //Number of charts produced in this demo module
    public int getNoOfCharts() {
      return 4;
    }

    //Main code for creating charts
    public void createChart(WinChartViewer viewer, int chartIndex)
    {
      // The value to display on the meter
      double value = 74.25;

      // Create a LinearMeter object of size 250 x 75 pixels with very light grey (0xeeeeee)
      // backgruond and a light grey (0xccccccc) 3-pixel thick rounded frame
      LinearMeter m = new LinearMeter(250, 75, 0xeeeeee, 0xcccccc);
      m.setRoundedFrame(Chart.Transparent);
      m.setThickFrame(3);

      // Set the scale region top-left corner at (14, 23), with size of 218 x 20 pixels. The
      // scale labels are located on the top (implies horizontal meter)
      m.setMeter(14, 23, 218, 20, Chart.Top);

      // Set meter scale from 0 - 100, with a tick every 10 units
      m.setScale(0, 100, 10);

      // Demostrate different types of color scales and putting them at different positions
      double[] smoothColorScale = {0, 0x6666ff, 25, 0x00bbbb, 50, 0x00ff00, 75, 0xffff00, 100,
                                   0xff0000
                                  };
      double[] stepColorScale = {0, 0x33ff33, 50, 0xffff33, 80, 0xff3333, 100};
      double[] highLowColorScale = {0, 0x6666ff, 70, Chart.Transparent, 100, 0xff0000};

      if (chartIndex == 0) {
        // Add the smooth color scale at the default position
        m.addColorScale(smoothColorScale);
      } else if (chartIndex == 1) {
        // Add the high low scale at the default position
        m.addColorScale(highLowColorScale);
      } else if (chartIndex == 2) {
        // Add the smooth color scale starting at y = 43 (bottom of scale) with zero width
        // and ending at y = 23 with 20 pixels width
        m.addColorScale(smoothColorScale, 43, 0, 23, 20);
      } else {
        // Add the step color scale at the default position
        m.addColorScale(stepColorScale);
      }

      // Add a blue (0x0000cc) pointer at the specified value
      m.addPointer(value, 0x0000cc);

      // Add a label left aligned to (10, 61) using 8pt Arial Bold font
      m.addText(10, 61, "Temperature C", "Arial Bold", 8, Chart.TextColor, Chart.Left);

      // Add a text box right aligned to (235, 61). Display the value using white (0xffffff)
      // 8pt Arial Bold font on a black (0x000000) background with depressed rounded border.
      ChartDirector.TextBox t = m.addText(235, 61, m.formatValue(value, "2"), "Arial Bold", 8,
                                          0xffffff, Chart.Right);
      t.setBackground(0x000000, 0x000000, -1);
      t.setRoundedCorners(3);

      // Output the chart
      viewer.Chart = m;
    }
  }
}

