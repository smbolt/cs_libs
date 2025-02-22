using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
  public class XyzoomscrollController : Controller
  {
    //
    // Initialize the WebChartViewer when the page is first loaded
    //
    private void initViewer(RazorChartViewer viewer)
    {
      //
      // This example assumes the initial chart is the full chart and we can auto-detect the full
      // data range in the drawChart code. So we do not need to configure the full range here.
      //
    }

    //
    // Draw the main chart
    //
    private void drawChart(RazorChartViewer viewer)
    {
      //
      // For simplicity, in this demo, the data arrays are filled with hard coded data. In a real
      // application, you may use a database or other data source to load up the arrays, and only
      // visible data (data within the view port) need to be loaded.
      //
      double[] dataX0 = {10, 15, 6, -12, 14, -8, 13, -3, 16, 12, 10.5, -7, 3, -10, -5, 2, 5};
      double[] dataY0 = {130, 150, 80, 110, -110, -105, -130, -15, -170, 125, 125, 60, 25, 150,
                         150, 15, 120
                        };
      double[] dataX1 = {6, 7, -4, 3.5, 7, 8, -9, -10, -12, 11, 8, -3, -2, 8, 4, -15, 15};
      double[] dataY1 = {65, -40, -40, 45, -70, -80, 80, 10, -100, 105, 60, 50, 20, 170, -25, 50,
                         75
                        };
      double[] dataX2 = {-10, -12, 11, 8, 6, 12, -4, 3.5, 7, 8, -9, 3, -13, 16, -7.5, -10, -15};
      double[] dataY2 = {65, -80, -40, 45, -70, -80, 80, 90, -100, 105, 60, -75, -150, -40, 120,
                         -50, -30
                        };

      // Create an XYChart object 500 x 480 pixels in size, with light blue (c0c0ff) background
      XYChart c = new XYChart(500, 480, 0xc0c0ff);

      // Set the plotarea at (50, 40) and of size 400 x 400 pixels. Use light grey (c0c0c0)
      // horizontal and vertical grid lines. Set 4 quadrant coloring, where the colors alternate
      // between lighter and deeper grey (dddddd/eeeeee).
      c.setPlotArea(50, 40, 400, 400, -1, -1, -1, 0xc0c0c0, 0xc0c0c0).set4QBgColor(0xdddddd,
          0xeeeeee, 0xdddddd, 0xeeeeee, 0x000000);

      // As the data can lie outside the plotarea in a zoomed chart, we need enable clipping
      c.setClipping();

      // Set 4 quadrant mode, with both x and y axes symetrical around the origin
      c.setAxisAtOrigin(Chart.XYAxisAtOrigin, Chart.XAxisSymmetric + Chart.YAxisSymmetric);

      // Add a legend box at (450, 40) (top right corner of the chart) with vertical layout and 8pt
      // Arial Bold font. Set the background color to semi-transparent grey (40dddddd).
      LegendBox legendBox = c.addLegend(450, 40, true, "Arial Bold", 8);
      legendBox.setAlignment(Chart.TopRight);
      legendBox.setBackground(0x40dddddd);

      // Add titles to axes
      c.xAxis().setTitle("Alpha Index");
      c.yAxis().setTitle("Beta Index");

      // Set axes line width to 2 pixels
      c.xAxis().setWidth(2);
      c.yAxis().setWidth(2);

      // The default ChartDirector settings has a denser y-axis grid spacing and less-dense x-axis
      // grid spacing. In this demo, we want the tick spacing to be symmetrical. We use around 40
      // pixels between major ticks and 20 pixels between minor ticks.
      c.xAxis().setTickDensity(40, 20);
      c.yAxis().setTickDensity(40, 20);

      //
      // In this example, we represent the data by scatter points. You may modify the code below to
      // use other layer types (lines, areas, etc).
      //

      // Add scatter layer, using 11 pixels red (ff33333) X shape symbols
      c.addScatterLayer(dataX0, dataY0, "Group A", Chart.Cross2Shape(), 11, 0xff3333);

      // Add scatter layer, using 11 pixels green (33ff33) circle symbols
      c.addScatterLayer(dataX1, dataY1, "Group B", Chart.CircleShape, 11, 0x33ff33);

      // Add scatter layer, using 11 pixels blue (3333ff) triangle symbols
      c.addScatterLayer(dataX2, dataY2, "Group C", Chart.TriangleSymbol, 11, 0x3333ff);

      //
      // In this example, we have not explicitly configured the full x and y range. In this case,
      // the first time syncLinearAxisWithViewPort is called, ChartDirector will auto-scale the
      // axis and assume the resulting range is the full range. In subsequent calls, ChartDirector
      // will set the axis range based on the view port and the full range.
      //
      viewer.syncLinearAxisWithViewPort("x", c.xAxis());
      viewer.syncLinearAxisWithViewPort("y", c.yAxis());

      // Output the chart
      viewer.Image = c.makeWebImage(Chart.PNG);

      // Include tool tip for the chart
      viewer.ImageMap = c.getHTMLImageMap("", "",
                                          "title='[{dataSetName}] Alpha = {x|G}, Beta = {value|G}'");
    }

    //
    // Draw the thumbnail chart in the WebViewPortControl
    //
    private void drawFullChart(RazorViewPortControl vp, RazorChartViewer viewer)
    {
      //
      // For simplicity, in this demo, the data arrays are filled with hard coded data. In a real
      // application, you may use a database or other data source to load up the arrays. As this is
      // a small thumbnail chart, complete data may not be needed. For exmaple, if there are a
      // million points, a random sample may already be sufficient for the thumbnail chart.
      //
      double[] dataX0 = {10, 15, 6, -12, 14, -8, 13, -3, 16, 12, 10.5, -7, 3, -10, -5, 2, 5};
      double[] dataY0 = {130, 150, 80, 110, -110, -105, -130, -15, -170, 125, 125, 60, 25, 150,
                         150, 15, 120
                        };
      double[] dataX1 = {6, 7, -4, 3.5, 7, 8, -9, -10, -12, 11, 8, -3, -2, 8, 4, -15, 15};
      double[] dataY1 = {65, -40, -40, 45, -70, -80, 80, 10, -100, 105, 60, 50, 20, 170, -25, 50,
                         75
                        };
      double[] dataX2 = {-10, -12, 11, 8, 6, 12, -4, 3.5, 7, 8, -9, 3, -13, 16, -7.5, -10, -15};
      double[] dataY2 = {65, -80, -40, 45, -70, -80, 80, 90, -100, 105, 60, -75, -150, -40, 120,
                         -50, -30
                        };

      // Create an XYChart object 120 x 120 pixels in size
      XYChart c = new XYChart(120, 120);

      // Set the plotarea to cover the entire chart and with no grid lines. Set 4 quadrant
      // coloring, where the colors alternate between lighter and deeper grey (d8d8d8/eeeeee).
      c.setPlotArea(0, 0, c.getWidth() - 1, c.getHeight() - 1, -1, -1, -1, Chart.Transparent
                   ).set4QBgColor(0xd8d8d8, 0xeeeeee, 0xd8d8d8, 0xeeeeee, 0x000000);

      // Set 4 quadrant mode, with both x and y axes symetrical around the origin
      c.setAxisAtOrigin(Chart.XYAxisAtOrigin, Chart.XAxisSymmetric + Chart.YAxisSymmetric);

      // The x and y axis scales reflect the full range of the view port
      c.xAxis().setLinearScale(viewer.getValueAtViewPort("x", 0), viewer.getValueAtViewPort("x", 1
                                                                                           ), Chart.NoValue);
      c.yAxis().setLinearScale(viewer.getValueAtViewPort("y", 0), viewer.getValueAtViewPort("y", 1
                                                                                           ), Chart.NoValue);

      // Add scatter layer, using 5 pixels red (ff33333) X shape symbols
      c.addScatterLayer(dataX0, dataY0, "Group A", Chart.Cross2Shape(), 5, 0xff3333);

      // Add scatter layer, using 5 pixels green (33ff33) circle symbols
      c.addScatterLayer(dataX1, dataY1, "Group B", Chart.CircleShape, 5, 0x33ff33);

      // Add scatter layer, using 5 pixels blue (3333ff) triangle symbols
      c.addScatterLayer(dataX2, dataY2, "Group C", Chart.TriangleSymbol, 5, 0x3333ff);

      // Output the chart
      vp.Image = c.makeWebImage(Chart.PNG);
    }

    public ActionResult Index()
    {
      RazorChartViewer viewer = ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1");
      RazorViewPortControl viewPortCtrl = ViewBag.ViewPortControl = new RazorViewPortControl(HttpContext, "chart2");

      //
      // This script handles both the full page request, as well as the subsequent partial updates
      // (AJAX chart updates). We need to determine the type of request first before we processing
      // it.
      //
      if (RazorChartViewer.IsPartialUpdateRequest(Request)) {
        // Is a partial update request.
        drawChart(viewer);
        return Content(viewer.PartialUpdateChart());
      }

      //
      // If the code reaches here, it is a full page request.
      //

      // Initialize the WebChartViewer and draw the chart.
      initViewer(viewer);
      drawChart(viewer);

      // Draw a thumbnail chart representing the full range in the WebViewPortControl
      drawFullChart(viewPortCtrl, viewer);
      return View();
    }
  }
}

