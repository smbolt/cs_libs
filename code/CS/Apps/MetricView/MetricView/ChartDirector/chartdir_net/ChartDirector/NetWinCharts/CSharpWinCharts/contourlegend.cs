using System;
using ChartDirector;

namespace CSharpChartExplorer
{
  public class contourlegend : DemoModule
  {
    //Name of demo module
    public string getName() {
      return "Contour Color Legend";
    }

    //Number of charts produced in this demo module
    public int getNoOfCharts() {
      return 1;
    }

    //Main code for creating chart.
    //Note: the argument chartIndex is unused because this demo only has 1 chart.
    public void createChart(WinChartViewer viewer, int chartIndex)
    {
      // The x and y coordinates of the grid
      double[] dataX = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
      double[] dataY = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

      // Use random numbers for the z values on the XY grid
      RanSeries r = new RanSeries(999);
      double[] dataZ = r.get2DSeries(dataX.Length, dataY.Length, -0.9, 1.15);

      // Create a XYChart object of size 640 x 460 pixels
      XYChart c = new XYChart(640, 460);

      // Set default text color to dark grey (0x333333)
      c.setColor(Chart.TextColor, 0x333333);

      // Set the plotarea at (30, 25) and of size 400 x 400 pixels. Use semi-transparent grey
      // (0xdd000000) horizontal and vertical grid lines
      c.setPlotArea(50, 25, 400, 400, -1, -1, Chart.Transparent, unchecked((int)0xdd000000),
                    -1);

      // Set the x and y axis stems to transparent and the label font to 12pt Arial
      c.xAxis().setColors(Chart.Transparent);
      c.yAxis().setColors(Chart.Transparent);
      c.xAxis().setLabelStyle("Arial", 12);
      c.yAxis().setLabelStyle("Arial", 12);

      // Set the x-axis and y-axis scale
      c.xAxis().setLinearScale(0, 10, 1);
      c.yAxis().setLinearScale(0, 10, 1);

      // Add a contour layer using the given data
      ContourLayer layer = c.addContourLayer(dataX, dataY, dataZ);

      // Move the grid lines in front of the contour layer
      c.getPlotArea().moveGridBefore(layer);

      // Define the color scale
      double[] colorScale = {-0.8, 0x0066ff, -0.5, 0x66ccff, -0.3, 0x66ffff, 0, 0x88ff88, 0.4,
                             0x00ff00, 0.7, 0xffff00, 0.9, 0xff6600, 1.0, 0xcc0000, 1.1
                            };
      // Apply the color scale, and specify the underflow and overflow colors for regions
      // exceeding the color scale
      layer.colorAxis().setColorScale(colorScale, 0x0000cc, 0x000000);

      //
      // Instead of displaying the color axis, we use a legend box to display the colors. This
      // is useful for colors that are unevenly spaced on the color axis.
      //

      // Add a legend box at (460, 25) with vertical layout, with 12pt Arial font, transparent
      // background and border, icon size of 15 x 15 pixels, and line spacing of 8 pixels.
      LegendBox b = c.addLegend(460, 25, true, "Arial", 12);
      b.setBackground(Chart.Transparent, Chart.Transparent);
      b.setKeySize(15, 15);
      b.setKeySpacing(0, 8);

      // Add the legend box entries
      b.addKey("> 1.1 (Critical)", 0x000000);
      b.addKey("1.0 to 1.1 (Alert)", 0xcc0000);
      b.addKey("0.9 to 1.0", 0xff6600);
      b.addKey("0.7 to 0.9", 0xffff00);
      b.addKey("0.4 to 0.7", 0x00ff00);
      b.addKey("0.0 to 0.4", 0x88ff88);
      b.addKey("-0.3 to 0.0", 0x66ffff);
      b.addKey("-0.5 to -0.3", 0x66ccff);
      b.addKey("-0.8 to -0.5", 0x0066ff);
      b.addKey("< -0.8", 0x0000cc);

      // Output the chart
      viewer.Chart = c;
    }
  }
}

