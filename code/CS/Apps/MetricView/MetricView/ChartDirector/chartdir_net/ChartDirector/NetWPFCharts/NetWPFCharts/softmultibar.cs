using System;
using ChartDirector;

namespace CSharpWPFDemo
{
  public class softmultibar : DemoModule
  {
    //Name of demo module
    public string getName() {
      return "Soft Multi-Bar Chart";
    }

    //Number of charts produced in this demo module
    public int getNoOfCharts() {
      return 1;
    }

    //Main code for creating chart.
    //Note: the argument chartIndex is unused because this demo only has 1 chart.
    public void createChart(WPFChartViewer viewer, int chartIndex)
    {
      // The data for the bar chart
      double[] data0 = {100, 125, 245, 147, 67};
      double[] data1 = {85, 156, 179, 211, 123};
      double[] data2 = {97, 87, 56, 267, 157};
      string[] labels = {"Mon", "Tue", "Wed", "Thur", "Fri"};

      // Create a XYChart object of size 540 x 375 pixels
      XYChart c = new XYChart(540, 375);

      // Add a title to the chart using 18pt Times Bold Italic font
      c.addTitle("Average Weekly Network Load", "Times New Roman Bold Italic", 18);

      // Set the plotarea at (50, 55) and of 440 x 280 pixels in size. Use a vertical gradient
      // color from light red (ffdddd) to dark red (880000) as background. Set border and grid
      // lines to white (ffffff).
      c.setPlotArea(50, 55, 440, 280, c.linearGradientColor(0, 55, 0, 335, 0xffdddd, 0x880000
                                                           ), -1, 0xffffff, 0xffffff);

      // Add a legend box at (50, 25) using horizontal layout. Use 10pt Arial Bold as font,
      // with transparent background.
      c.addLegend(50, 25, false, "Arial Bold", 10).setBackground(Chart.Transparent);

      // Set the x axis labels
      c.xAxis().setLabels(labels);

      // Draw the ticks between label positions (instead of at label positions)
      c.xAxis().setTickOffset(0.5);

      // Set axis label style to 8pt Arial Bold
      c.xAxis().setLabelStyle("Arial Bold", 8);
      c.yAxis().setLabelStyle("Arial Bold", 8);

      // Set axis line width to 2 pixels
      c.xAxis().setWidth(2);
      c.yAxis().setWidth(2);

      // Add axis title
      c.yAxis().setTitle("Throughput (MBytes Per Hour)");

      // Add a multi-bar layer with 3 data sets and 4 pixels 3D depth
      BarLayer layer = c.addBarLayer2(Chart.Side, 4);
      layer.addDataSet(data0, 0xffff00, "Server #1");
      layer.addDataSet(data1, 0x00ff00, "Server #2");
      layer.addDataSet(data2, 0x9999ff, "Server #3");

      // Set bar border to transparent. Use soft lighting effect with light direction from
      // top.
      layer.setBorderColor(Chart.Transparent, Chart.softLighting(Chart.Top));

      // Configure the bars within a group to touch each others (no gap)
      layer.setBarGap(0.2, Chart.TouchBar);

      // Output the chart
      viewer.Chart = c;

      //include tool tip for the chart
      viewer.ImageMap = c.getHTMLImageMap("clickable", "",
                                          "title='{dataSetName} on {xLabel}: {value} MBytes/hour'");
    }
  }
}

