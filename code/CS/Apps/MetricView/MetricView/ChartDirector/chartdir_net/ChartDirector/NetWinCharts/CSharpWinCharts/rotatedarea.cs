using System;
using ChartDirector;

namespace CSharpChartExplorer
{
  public class rotatedarea : DemoModule
  {
    //Name of demo module
    public string getName() {
      return "Rotated Area Chart";
    }

    //Number of charts produced in this demo module
    public int getNoOfCharts() {
      return 1;
    }

    //Main code for creating chart.
    //Note: the argument chartIndex is unused because this demo only has 1 chart.
    public void createChart(WinChartViewer viewer, int chartIndex)
    {
      // The data for the area chart
      double[] data = {30, 28, 40, 55, 75, 68, 54, 60, 50, 62, 75, 65, 75, 89, 60, 55, 53, 35,
                       50, 66, 56, 48, 52, 65, 62
                      };

      // The labels for the area chart
      double[] labels = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
                         20, 21, 22, 23, 24
                        };

      // Create a XYChart object of size 320 x 320 pixels
      XYChart c = new XYChart(320, 320);

      // Swap the x and y axis to become a rotated chart
      c.swapXY();

      // Set the y axis on the top side (right + rotated = top)
      c.setYAxisOnRight();

      // Reverse the x axis so it is pointing downwards
      c.xAxis().setReverse();

      // Set the plotarea at (50, 50) and of size 200 x 200 pixels. Enable horizontal and
      // vertical grids by setting their colors to grey (0xc0c0c0).
      c.setPlotArea(50, 50, 250, 250).setGridColor(0xc0c0c0, 0xc0c0c0);

      // Add a line chart layer using the given data
      c.addAreaLayer(data, c.gradientColor(50, 0, 300, 0, 0xffffff, 0x0000ff));

      // Set the labels on the x axis. Append "m" after the value to show the unit.
      c.xAxis().setLabels2(labels, "{value} m");

      // Display 1 out of 3 labels.
      c.xAxis().setLabelStep(3);

      // Add a title to the x axis
      c.xAxis().setTitle("Depth");

      // Add a title to the y axis
      c.yAxis().setTitle("Carbon Dioxide Concentration (ppm)");

      // Output the chart
      viewer.Chart = c;

      //include tool tip for the chart
      viewer.ImageMap = c.getHTMLImageMap("clickable", "",
                                          "title='Carbon dioxide concentration at {xLabel}: {value} ppm'");
    }
  }
}

