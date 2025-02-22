using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
  public class JsclickController : Controller
  {
    //
    // Default Action
    //
    public ActionResult Index()
    {
      createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
      return View();
    }

    //
    // Create chart
    //
    private void createChart(RazorChartViewer viewer)
    {
      //
      // For demo purpose, we use hard coded data. In real life, the following data could come from
      // a database.
      //
      double[] revenue = {4500, 5600, 6300, 8000, 12000, 14000, 16000, 20000, 24000, 28000};
      double[] grossMargin = {62, 65, 63, 60, 55, 56, 57, 53, 52, 50};
      double[] backLog = {563, 683, 788, 941, 1334, 1522, 1644, 1905, 2222, 2544};
      string[] labels = {"1996", "1997", "1998", "1999", "2000", "2001", "2002", "2003", "2004",
                         "2005"
                        };

      // Create a XYChart object of size 600 x 360 pixels
      XYChart c = new XYChart(600, 360);

      // Add a title to the chart using 18pt Times Bold Italic font
      c.addTitle("Annual Revenue for Star Tech", "Times New Roman Bold Italic", 18);

      // Set the plotarea at (60, 40) and of size 480 x 280 pixels. Use a vertical gradient color
      // from light green (eeffee) to dark green (008800) as background. Set border and grid lines
      // to white (ffffff).
      c.setPlotArea(60, 40, 480, 280, c.linearGradientColor(60, 40, 60, 280, 0xeeffee, 0x008800),
                    -1, 0xffffff, 0xffffff);

      // Add a multi-color bar chart layer using the revenue data.
      BarLayer layer = c.addBarLayer3(revenue);

      // Set cylinder bar shape
      layer.setBarShape(Chart.CircleShape);

      // Add extra field to the bars. These fields are used for showing additional information.
      layer.addExtraField2(grossMargin);
      layer.addExtraField2(backLog);

      // Set the labels on the x axis.
      c.xAxis().setLabels(labels);

      // In this example, we show the same scale using both axes
      c.syncYAxis();

      // Set the axis line to transparent
      c.xAxis().setColors(Chart.Transparent);
      c.yAxis().setColors(Chart.Transparent);
      c.yAxis2().setColors(Chart.Transparent);

      // Set the axis label to using 8pt Arial Bold as font
      c.yAxis().setLabelStyle("Arial Bold", 8);
      c.yAxis2().setLabelStyle("Arial Bold", 8);
      c.xAxis().setLabelStyle("Arial Bold", 8);

      // Add title to the y axes
      c.yAxis().setTitle("USD (millions)", "Arial Bold", 10);
      c.yAxis2().setTitle("USD (millions)", "Arial Bold", 10);

      // Create the image and save it in a temporary location
      viewer.Image = c.makeWebImage(Chart.PNG);

      // Client side Javascript to show detail information "onmouseover"
      string showText = "onmouseover='showInfo(\"{xLabel}\", {value}, {field0}, {field1});' ";

      // Client side Javascript to hide detail information "onmouseout"
      string hideText = "onmouseout='showInfo(null);' ";

      // "title" attribute to show tool tip
      string toolTip = "title='{xLabel}: US$ {value|0}M'";

      // Create an image map for the chart
      viewer.ImageMap = c.getHTMLImageMap(Url.Action("", "xystub"), "", showText + hideText +
                                          toolTip);
    }
  }
}

