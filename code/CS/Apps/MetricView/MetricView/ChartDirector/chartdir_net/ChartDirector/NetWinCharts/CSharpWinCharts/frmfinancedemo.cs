using System;
using System.Collections;
using System.Windows.Forms;
using ChartDirector;

namespace CSharpChartExplorer
{
  public partial class FrmFinanceDemo : Form
  {
    public FrmFinanceDemo()
    {
      InitializeComponent();
    }

    /// <summary>
    /// A utility class for adding items to ComboBox
    /// </summary>
    private class ListItem
    {
      string m_key;
      string m_value;

      public ListItem(string key, string val)
      {
        m_key = key;
        m_value = val;
      }

      public string Key
      {
        get {
          return m_key;
        }
      }

      public override string ToString()
      {
        return m_value;
      }
    }

    // The ticker symbol, timeStamps, volume, high, low, open and close data
    string tickerKey = "";
    private DateTime[] timeStamps;
    private double[] volData;
    private double[] highData;
    private double[] lowData;
    private double[] openData;
    private double[] closeData;

    // An extra data series to compare with the close data
    private string compareKey = "";
    private double[] compareData = null;

    // The resolution of the data in seconds. 1 day = 86400 seconds.
    private int resolution = 86400;

    // Will set to true at the end of initialization - prevents events from firing before the
    // controls are properly initialized.
    private bool hasFinishedInitialization = false;

    /// <summary>
    /// Form Load event handler - initialize the form
    /// </summary>
    private void FrmFinanceDemo_Load(object sender, System.EventArgs e)
    {
      hasFinishedInitialization = false;

      timeRange.Items.Add(new ListItem("1", "1 day"));
      timeRange.Items.Add(new ListItem("2", "2 days"));
      timeRange.Items.Add(new ListItem("5", "5 days"));
      timeRange.Items.Add(new ListItem("10", "10 days"));
      timeRange.Items.Add(new ListItem("30", "1 month"));
      timeRange.Items.Add(new ListItem("60", "2 months"));
      timeRange.Items.Add(new ListItem("90", "3 months"));
      timeRange.SelectedIndex = timeRange.Items.Add(new ListItem("180", "6 months"));
      timeRange.Items.Add(new ListItem("360", "1 year"));
      timeRange.Items.Add(new ListItem("720", "2 years"));
      timeRange.Items.Add(new ListItem("1080", "3 years"));
      timeRange.Items.Add(new ListItem("1440", "4 years"));
      timeRange.Items.Add(new ListItem("1800", "5 years"));
      timeRange.Items.Add(new ListItem("3600", "10 years"));

      chartSize.Items.Add(new ListItem("S", "Small"));
      chartSize.Items.Add(new ListItem("M", "Medium"));
      chartSize.SelectedIndex = chartSize.Items.Add(new ListItem("L", "Large"));
      chartSize.Items.Add(new ListItem("H", "Huge"));

      chartType.Items.Add(new ListItem("None", "None"));
      chartType.SelectedIndex = chartType.Items.Add(new ListItem("CandleStick", "CandleStick"));
      chartType.Items.Add(new ListItem("Close", "Closing Price"));
      chartType.Items.Add(new ListItem("Median", "Median Price"));
      chartType.Items.Add(new ListItem("OHLC", "OHLC"));
      chartType.Items.Add(new ListItem("TP", "Typical Price"));
      chartType.Items.Add(new ListItem("WC", "Weighted Close"));

      priceBand.Items.Add(new ListItem("None", "None"));
      priceBand.SelectedIndex = priceBand.Items.Add(new ListItem("BB", "Bollinger Band"));
      priceBand.Items.Add(new ListItem("DC", "Donchain Channel"));
      priceBand.Items.Add(new ListItem("Envelop", "Envelop (SMA 20 +/- 10%)"));

      avgType1.Items.Add(new ListItem("None", "None"));
      avgType1.SelectedIndex = avgType1.Items.Add(new ListItem("SMA", "Simple"));
      avgType1.Items.Add(new ListItem("EMA", "Exponential"));
      avgType1.Items.Add(new ListItem("TMA", "Triangular"));
      avgType1.Items.Add(new ListItem("WMA", "Weighted"));

      avgType2.Items.Add(new ListItem("None", "None"));
      avgType2.SelectedIndex = avgType2.Items.Add(new ListItem("SMA", "Simple"));
      avgType2.Items.Add(new ListItem("EMA", "Exponential"));
      avgType2.Items.Add(new ListItem("TMA", "Triangular"));
      avgType2.Items.Add(new ListItem("WMA", "Weighted"));

      ListItem[] indicators =
      {
        new ListItem("None", "None"),
        new ListItem("AccDist", "Accumulation/Distribution"),
        new ListItem("AroonOsc", "Aroon Oscillator"),
        new ListItem("Aroon", "Aroon Up/Down"),
        new ListItem("ADX", "Avg Directional Index"),
        new ListItem("ATR", "Avg True Range"),
        new ListItem("BBW", "Bollinger Band Width"),
        new ListItem("CMF", "Chaikin Money Flow"),
        new ListItem("COscillator", "Chaikin Oscillator"),
        new ListItem("CVolatility", "Chaikin Volatility"),
        new ListItem("CLV", "Close Location Value"),
        new ListItem("CCI", "Commodity Channel Index"),
        new ListItem("DPO", "Detrended Price Osc"),
        new ListItem("DCW", "Donchian Channel Width"),
        new ListItem("EMV", "Ease of Movement"),
        new ListItem("FStoch", "Fast Stochastic"),
        new ListItem("MACD", "MACD"),
        new ListItem("MDX", "Mass Index"),
        new ListItem("Momentum", "Momentum"),
        new ListItem("MFI", "Money Flow Index"),
        new ListItem("NVI", "Neg Volume Index"),
        new ListItem("OBV", "On Balance Volume"),
        new ListItem("Performance", "Performance"),
        new ListItem("PPO", "% Price Oscillator"),
        new ListItem("PVO", "% Volume Oscillator"),
        new ListItem("PVI", "Pos Volume Index"),
        new ListItem("PVT", "Price Volume Trend"),
        new ListItem("ROC", "Rate of Change"),
        new ListItem("RSI", "RSI"),
        new ListItem("SStoch", "Slow Stochastic"),
        new ListItem("StochRSI", "StochRSI"),
        new ListItem("TRIX", "TRIX"),
        new ListItem("UO", "Ultimate Oscillator"),
        new ListItem("Vol", "Volume"),
        new ListItem("WilliamR", "William's %R")
      };

      indicator1.Items.AddRange(indicators);
      indicator2.Items.AddRange(indicators);
      indicator3.Items.AddRange(indicators);
      indicator4.Items.AddRange(indicators);

      for (int i = 0; i < indicators.Length; ++i)
      {
        if (indicators[i].Key == "RSI")
          indicator1.SelectedIndex = i;
        else if (indicators[i].Key == "MACD")
          indicator2.SelectedIndex = i;
      }
      indicator3.SelectedIndex = 0;
      indicator4.SelectedIndex = 0;

      hasFinishedInitialization = true;
      drawChart(winChartViewer1);
    }

    /// <summary>
    /// Get the timeStamps, highData, lowData, openData, closeData and volData.
    /// </summary>
    /// <param name="ticker">The ticker symbol for the data series.</param>
    /// <param name="startDate">The starting date/time for the data series.</param>
    /// <param name="endDate">The ending date/time for the data series.</param>
    /// <param name="durationInDays">The number of trading days to get.</param>
    /// <param name="extraPoints">The extra leading data points needed in order to
    /// compute moving averages.</param>
    /// <returns>True if successfully obtain the data, otherwise false.</returns>
    protected bool getData(string ticker, DateTime startDate, DateTime endDate,
                           int durationInDays, int extraPoints)
    {
      // This method should return false if the ticker symbol is invalid. In this
      // sample code, as we are using a random number generator for the data, all
      // ticker symbol is allowed, but we still assumed an empty symbol is invalid.
      if (ticker == "")
        return false;

      // In this demo, we can get 15 min, daily, weekly or monthly data depending on
      // the time range.
      resolution = 86400;
      if (durationInDays <= 10)
      {
        // 10 days or less, we assume 15 minute data points are available
        resolution = 900;

        // We need to adjust the startDate backwards for the extraPoints. We assume
        // 6.5 hours trading time per day, and 5 trading days per week.
        double dataPointsPerDay = 6.5 * 3600 / resolution;
        DateTime adjustedStartDate = startDate.AddDays(-Math.Ceiling(extraPoints /
                                     dataPointsPerDay * 7 / 5) - 2);

        // Get the required 15 min data
        get15MinData(ticker, adjustedStartDate, endDate);

      }
      else if (durationInDays >= 4.5 * 360)
      {
        // 4 years or more - use monthly data points.
        resolution = 30 * 86400;

        // Adjust startDate backwards to cater for extraPoints
        DateTime adjustedStartDate = startDate.Date.AddMonths(-extraPoints);

        // Get the required monthly data
        getMonthlyData(ticker, adjustedStartDate, endDate);

      }
      else if (durationInDays >= 1.5 * 360)
      {
        // 1 year or more - use weekly points.
        resolution = 7 * 86400;

        // Adjust startDate backwards to cater for extraPoints
        DateTime adjustedStartDate = startDate.Date.AddDays(-extraPoints * 7 - 6);

        // Get the required weekly data
        getWeeklyData(ticker, adjustedStartDate, endDate);

      }
      else
      {
        // Default - use daily points
        resolution = 86400;

        // Adjust startDate backwards to cater for extraPoints. We multiply the days
        // by 7/5 as we assume 1 week has 5 trading days.
        DateTime adjustedStartDate = startDate.Date.AddDays(-Math.Ceiling(extraPoints
                                     * 7.0 / 5) - 2);

        // Get the required daily data
        getDailyData(ticker, adjustedStartDate, endDate);
      }

      return true;
    }

    /// <summary>
    /// Get 15 minutes data series for timeStamps, highData, lowData, openData, closeData
    /// and volData.
    /// </summary>
    /// <param name="ticker">The ticker symbol for the data series.</param>
    /// <param name="startDate">The starting date/time for the data series.</param>
    /// <param name="endDate">The ending date/time for the data series.</param>
    private void get15MinData(string ticker, DateTime startDate, DateTime endDate)
    {
      //
      //In this demo, we use a random number generator to generate the data. In practice,
      //you may get the data from a database or by other means. If you do not have 15
      //minute data, you may modify the "drawChart" method below to not using 15 minute
      //data.
      //
      generateRandomData(ticker, startDate, endDate, 900);
    }

    /// <summary>
    /// Get daily data series for timeStamps, highData, lowData, openData, closeData
    /// and volData.
    /// </summary>
    /// <param name="ticker">The ticker symbol for the data series.</param>
    /// <param name="startDate">The starting date/time for the data series.</param>
    /// <param name="endDate">The ending date/time for the data series.</param>
    private void getDailyData(string ticker, DateTime startDate, DateTime endDate)
    {
      //
      //In this demo, we use a random number generator to generate the data. In practice,
      //you may get the data from a database or by other means. A typical database code
      //example is like below. (This only shows a general idea. The exact details may differ
      //depending on your database brand and schema. The SQL, in particular the date format,
      //may be different depending on which brand of database you use.)
      //
      //	 //Open the database connection to MS SQL
      //	 System.Data.IDbConnection dbconn = new System.Data.SqlClient.SqlConnection(
      //	      "..... put your database connection string here .......");
      //   dbconn.Open();
      //
      //   //SQL statement to get the data
      //   System.Data.IDbCommand sqlCmd = dbconn.CreateCommand();
      //   sqlCmd.CommandText = "Select recordDate, highData, lowData, openData, " +
      //         "closeData, volData From dailyFinanceTable Where ticker = '" + ticker +
      //         "' And recordDate >= '" + startDate.ToString("yyyyMMdd") + "' And " +
      //         "recordDate <= '" + endDate.ToString("yyyyMMdd") + "' Order By recordDate";
      //
      //   //The most convenient way to read the SQL result into arrays is to use the
      //   //ChartDirector DBTable utility.
      //   DBTable table = new DBTable(sqlCmd.ExecuteReader());
      //   dbconn.Close();
      //
      //   //Now get the data into arrays
      //   timeStamps = table.getColAsDateTime(0);
      //   highData = table.getCol(1);
      //   lowData = table.getCol(2);
      //   openData = table.getCol(3);
      //   closeData = table.getCol(4);
      //   volData = table.getCol(5);
      //
      generateRandomData(ticker, startDate, endDate, 86400);
    }

    /// <summary>
    /// Get weekly data series for timeStamps, highData, lowData, openData, closeData
    /// and volData.
    /// </summary>
    /// <param name="ticker">The ticker symbol for the data series.</param>
    /// <param name="startDate">The starting date/time for the data series.</param>
    /// <param name="endDate">The ending date/time for the data series.</param>
    private void getWeeklyData(string ticker, DateTime startDate, DateTime endDate)
    {
      //
      //In this demo, we use a random number generator to generate the data. In practice,
      //you may get the data from a database or by other means. If you do not have weekly
      //data, you may call "getDailyData" to get daily data first, and then call
      //"convertDailyToWeeklyData" to convert it to weekly data, like:
      //
      //      getDailyData(startDate, endDate);
      //      convertDailyToWeeklyData();
      //
      generateRandomData(ticker, startDate, endDate, 86400 * 7);
    }

    /// <summary>
    /// Get monthly data series for timeStamps, highData, lowData, openData, closeData
    /// and volData.
    /// </summary>
    /// <param name="ticker">The ticker symbol for the data series.</param>
    /// <param name="startDate">The starting date/time for the data series.</param>
    /// <param name="endDate">The ending date/time for the data series.</param>
    private void getMonthlyData(string ticker, DateTime startDate, DateTime endDate)
    {
      //
      //In this demo, we use a random number generator to generate the data. In practice,
      //you may get the data from a database or by other means. If you do not have monthly
      //data, you may call "getDailyData" to get daily data first, and then call
      //"convertDailyToMonthlyData" to convert it to monthly data, like:
      //
      //      getDailyData(startDate, endDate);
      //      convertDailyToMonthlyData();
      //
      generateRandomData(ticker, startDate, endDate, 86400 * 30);
    }

    /// <summary>
    /// A random number generator designed to generate realistic financial data.
    /// </summary>
    /// <param name="ticker">The ticker symbol for the data series.</param>
    /// <param name="startDate">The starting date/time for the data series.</param>
    /// <param name="endDate">The ending date/time for the data series.</param>
    /// <param name="resolution">The period of the data series.</param>
    private void generateRandomData(string ticker, DateTime startDate, DateTime endDate,
                                    int resolution)
    {
      FinanceSimulator db = new FinanceSimulator(ticker, startDate, endDate, resolution);
      timeStamps = db.getTimeStamps();
      highData = db.getHighData();
      lowData = db.getLowData();
      openData = db.getOpenData();
      closeData = db.getCloseData();
      volData = db.getVolData();
    }

    /// <summary>
    /// A utility to convert daily to weekly data.
    /// </summary>
    private void convertDailyToWeeklyData()
    {
      aggregateData(new ArrayMath(timeStamps).selectStartOfWeek());
    }

    /// <summary>
    /// A utility to convert daily to monthly data.
    /// </summary>
    private void convertDailyToMonthlyData()
    {
      aggregateData(new ArrayMath(timeStamps).selectStartOfMonth());
    }

    /// <summary>
    /// An internal method used to aggregate daily data.
    /// </summary>
    private void aggregateData(ArrayMath aggregator)
    {
      timeStamps = Chart.NTime(aggregator.aggregate(Chart.CTime(timeStamps),
                               Chart.AggregateFirst));
      highData = aggregator.aggregate(highData, Chart.AggregateMax);
      lowData = aggregator.aggregate(lowData, Chart.AggregateMin);
      openData = aggregator.aggregate(openData, Chart.AggregateFirst);
      closeData = aggregator.aggregate(closeData, Chart.AggregateLast);
      volData = aggregator.aggregate(volData, Chart.AggregateSum);
    }

    /// <summary>
    /// In this sample code, the chart updates when the user selection changes. You may
    /// modify the code to update the data and chart periodically for real time charts.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void selectionChanged(object sender, System.EventArgs e)
    {
      if (hasFinishedInitialization)
        drawChart(winChartViewer1);
    }

    ///
    /// For the ticker symbols, the chart will update when the user enters a new symbol,
    /// and then press the enter button or leave the text box.
    ///

    private void tickerSymbol_Leave(object sender, System.EventArgs e)
    {
      // User leave ticker symbol text box - redraw chart if symbol has changed
      if (tickerSymbol.Text != tickerKey)
        drawChart(winChartViewer1);
    }

    private void tickerSymbol_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      // User press enter key - same action as leaving the text box.
      if (e.KeyChar == '\r')
        tickerSymbol_Leave(sender, e);
    }

    private void compareWith_Leave(object sender, System.EventArgs e)
    {
      // User leave compare symbol text box - redraw chart if symbol has changed
      if (compareWith.Text != compareKey)
        drawChart(winChartViewer1);
    }

    private void compareWith_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      // User press enter key - same action as leaving the text box.
      if (e.KeyChar == '\r')
        compareWith_Leave(sender, e);
    }

    /// <summary>
    /// Draw the chart according to user selection and display it in the WinChartViewer.
    /// </summary>
    /// <param name="viewer">The WinChartViewer object to display the chart.</param>
    private void drawChart(WinChartViewer viewer)
    {
      // Use InvariantCulture to draw the chart. This ensures the chart will look the
      // same on any computer.
      System.Threading.Thread.CurrentThread.CurrentCulture =
        System.Globalization.CultureInfo.InvariantCulture;

      // In this demo, we just assume we plot up to the latest time. So endDate is now.
      DateTime endDate = DateTime.Now;

      // If the trading day has not yet started (before 9:30am), or if the end date is
      // on on Sat or Sun, we set the end date to 4:00pm of the last trading day
      while ((endDate.TimeOfDay.CompareTo(new TimeSpan(9, 30, 0)) < 0) || (
               endDate.DayOfWeek == DayOfWeek.Sunday) || (endDate.DayOfWeek ==
                   DayOfWeek.Saturday))
      {
        endDate = endDate.Date.AddDays(-1).Add(new TimeSpan(16, 0, 0));
      }

      // The duration selected by the user
      int durationInDays = int.Parse(((ListItem)timeRange.SelectedItem).Key);

      // Compute the start date by subtracting the duration from the end date.
      DateTime startDate;
      if (durationInDays >= 30)
      {
        // More or equal to 30 days - so we use months as the unit
        startDate = new DateTime(endDate.Year, endDate.Month, 1).AddMonths(
          -durationInDays / 30);
      }
      else
      {
        // Less than 30 days - use day as the unit. Note that we use trading days
        // below. For less than 30 days, the starting point of the axis is always at
        // the start of the day.
        startDate = endDate.Date;
        for (int i = 1; i < durationInDays; ++i)
          startDate = startDate.AddDays(
                        (startDate.DayOfWeek == DayOfWeek.Monday) ? -3 : -1);
      }

      // The first moving average period selected by the user.
      int avgPeriod1;
      try {
        avgPeriod1 = int.Parse(movAvg1.Text);
      }
      catch {
        avgPeriod1 = 0;
      }
      avgPeriod1 = Math.Max(0, Math.Min(300, avgPeriod1));

      // The second moving average period selected by the user.
      int avgPeriod2;
      try {
        avgPeriod2 = int.Parse(movAvg2.Text);
      }
      catch {
        avgPeriod2 = 0;
      }
      avgPeriod2 = Math.Max(0, Math.Min(300, avgPeriod2));

      // We need extra leading data points in order to compute moving averages.
      int extraPoints = Math.Max(20, Math.Max(avgPeriod1, avgPeriod2));

      // Get the data series to compare with, if any.
      compareKey = compareWith.Text.Trim();
      compareData = null;
      if (getData(compareKey, startDate, endDate, durationInDays, extraPoints))
        compareData = closeData;

      // The data series we want to get.
      tickerKey = tickerSymbol.Text.Trim();
      if (!getData(tickerKey, startDate, endDate, durationInDays, extraPoints))
      {
        errMsg(viewer, "Please enter a valid ticker symbol");
        return;
      }

      // We now confirm the actual number of extra points (data points that are before
      // the start date) as inferred using actual data from the database.
      extraPoints = timeStamps.Length;
      for (int i = 0; i < timeStamps.Length; ++i)
      {
        if (timeStamps[i] >= startDate)
        {
          extraPoints = i;
          break;
        }
      }

      // Check if there is any valid data
      if (extraPoints >= timeStamps.Length)
      {
        // No data - just display the no data message.
        errMsg(viewer, "No data available for the specified time period");
        return;
      }

      // In some finance chart presentation style, even if the data for the latest day
      // is not fully available, the axis for the entire day will still be drawn, where
      // no data will appear near the end of the axis.
      if (resolution < 86400)
      {
        // Add extra points to the axis until it reaches the end of the day. The end
        // of day is assumed to be 4:00pm (it depends on the stock exchange).
        DateTime lastTime = timeStamps[timeStamps.Length - 1];
        int extraTrailingPoints = (int)(new TimeSpan(16, 0, 0).Subtract(
                                          lastTime.TimeOfDay).TotalSeconds / resolution);

        if (extraTrailingPoints > 0)
        {
          DateTime[] extendedTimeStamps = new DateTime[timeStamps.Length +
              extraTrailingPoints];
          Array.Copy(timeStamps, 0, extendedTimeStamps, 0, timeStamps.Length);
          for (int i = 0; i < extraTrailingPoints; ++i)
            extendedTimeStamps[i + timeStamps.Length] = lastTime.AddSeconds(
                  resolution * i);
          timeStamps = extendedTimeStamps;
        }
      }

      //
      // At this stage, all data is available. We can draw the chart as according to
      // user input.
      //

      //
      // Determine the chart size. In this demo, user can select 4 different chart
      // sizes. Default is the large chart size.
      //
      int width = 780;
      int mainHeight = 255;
      int indicatorHeight = 80;

      string selectedSize = ((ListItem)chartSize.SelectedItem).Key;
      if (selectedSize == "S")
      {
        // Small chart size
        width = 450;
        mainHeight = 160;
        indicatorHeight = 60;
      }
      else if (selectedSize == "M")
      {
        // Medium chart size
        width = 620;
        mainHeight = 215;
        indicatorHeight = 70;
      }
      else if (selectedSize == "H")
      {
        // Huge chart size
        width = 1000;
        mainHeight = 320;
        indicatorHeight = 90;
      }

      // Create the chart object using the selected size
      FinanceChart m = new FinanceChart(width);

      // Set the data into the chart object
      m.setData(timeStamps, highData, lowData, openData, closeData, volData, extraPoints);

      //
      // We configure the title of the chart. In this demo chart design, we put the
      // company name as the top line of the title with left alignment.
      //
      m.addPlotAreaTitle(Chart.TopLeft, tickerKey);

      // We displays the current date as well as the data resolution on the next line.
      string resolutionText = "";
      if (resolution == 30 * 86400)
        resolutionText = "Monthly";
      else if (resolution == 7 * 86400)
        resolutionText = "Weekly";
      else if (resolution == 86400)
        resolutionText = "Daily";
      else if (resolution == 900)
        resolutionText = "15-min";

      m.addPlotAreaTitle(Chart.BottomLeft, "<*font=Arial,size=8*>" + m.formatValue(
                           DateTime.Now, "mmm dd, yyyy") + " - " + resolutionText + " chart");

      // A copyright message at the bottom left corner the title area
      m.addPlotAreaTitle(Chart.BottomRight,
                         "<*font=arial.ttf,size=8*>(c) Advanced Software Engineering");

      //
      // Add the first techical indicator according. In this demo, we draw the first
      // indicator on top of the main chart.
      //
      addIndicator(m, ((ListItem)indicator1.SelectedItem).Key, indicatorHeight);

      //
      // Add the main chart
      //
      m.addMainChart(mainHeight);

      //
      // Set log or linear scale according to user preference
      //
      m.setLogScale(logScale.Checked);

      //
      // Set axis labels to show data values or percentage change to user preference
      //
      if (percentageScale.Checked)
        m.setPercentageAxis();

      //
      // Draw any price line the user has selected
      //
      string mainType = ((ListItem)chartType.SelectedItem).Key;
      if (mainType == "Close")
        m.addCloseLine(0x000040);
      else if (mainType == "TP")
        m.addTypicalPrice(0x000040);
      else if (mainType == "WC")
        m.addWeightedClose(0x000040);
      else if (mainType == "Median")
        m.addMedianPrice(0x000040);

      //
      // Add comparison line if there is data for comparison
      //
      if ((compareData != null) && (compareData.Length > extraPoints))
        m.addComparison(compareData, 0x0000ff, compareKey);

      //
      // Add moving average lines.
      //
      addMovingAvg(m, ((ListItem)avgType1.SelectedItem).Key, avgPeriod1, 0x663300);
      addMovingAvg(m, ((ListItem)avgType2.SelectedItem).Key, avgPeriod2, 0x9900ff);

      //
      // Draw candlesticks or OHLC symbols if the user has selected them.
      //
      if (mainType == "CandleStick")
        m.addCandleStick(0x33ff33, 0xff3333);
      else if (mainType == "OHLC")
        m.addHLOC(0x008800, 0xcc0000);

      //
      // Add parabolic SAR if necessary
      //
      if (parabolicSAR.Checked)
        m.addParabolicSAR(0.02, 0.02, 0.2, Chart.DiamondShape, 5, 0x008800, 0x000000);

      //
      // Add price band/channel/envelop to the chart according to user selection
      //
      string selectedBand = ((ListItem)priceBand.SelectedItem).Key;
      if (selectedBand == "BB")
        m.addBollingerBand(20, 2, 0x9999ff, unchecked((int)(0xc06666ff)));
      else if (selectedBand == "DC")
        m.addDonchianChannel(20, 0x9999ff, unchecked((int)(0xc06666ff)));
      else if (selectedBand == "Envelop")
        m.addEnvelop(20, 0.1, 0x9999ff, unchecked((int)(0xc06666ff)));

      //
      // Add volume bars to the main chart if necessary
      //
      if (volumeBars.Checked)
        m.addVolBars(indicatorHeight, 0x99ff99, 0xff9999, 0xc0c0c0);

      //
      // Add additional indicators as according to user selection.
      //
      addIndicator(m, ((ListItem)indicator2.SelectedItem).Key, indicatorHeight);
      addIndicator(m, ((ListItem)indicator3.SelectedItem).Key, indicatorHeight);
      addIndicator(m, ((ListItem)indicator4.SelectedItem).Key, indicatorHeight);

      //
      // output the chart
      //
      viewer.Chart = m;

      //
      // tooltips for the chart
      //
      viewer.ImageMap = m.getHTMLImageMap("", "", "title='" + m.getToolTipDateFormat()
                                          + " {value|P}'");
    }

    /// <summary>
    /// Add a moving average line to the FinanceChart object.
    /// </summary>
    /// <param name="m">The FinanceChart object to add the line to.</param>
    /// <param name="avgType">The moving average type (SMA/EMA/TMA/WMA).</param>
    /// <param name="avgPeriod">The moving average period.</param>
    /// <param name="color">The color of the line.</param>
    /// <returns>The LineLayer object representing line layer created.</returns>
    protected LineLayer addMovingAvg(FinanceChart m, string avgType, int avgPeriod, int color)
    {
      if (avgPeriod > 1)
      {
        if (avgType == "SMA")
          return m.addSimpleMovingAvg(avgPeriod, color);
        else if (avgType == "EMA")
          return m.addExpMovingAvg(avgPeriod, color);
        else if (avgType == "TMA")
          return m.addTriMovingAvg(avgPeriod, color);
        else if (avgType == "WMA")
          return m.addWeightedMovingAvg(avgPeriod, color);
      }
      return null;
    }

    /// <summary>
    /// Add an indicator chart to the FinanceChart object. In this demo example, the indicator
    /// parameters (such as the period used to compute RSI, colors of the lines, etc.) are hard
    /// coded to commonly used values. You are welcome to design a more complex user interface
    /// to allow users to set the parameters.
    /// </summary>
    /// <param name="m">The FinanceChart object to add the line to.</param>
    /// <param name="indicator">The selected indicator.</param>
    /// <param name="height">Height of the chart in pixels</param>
    /// <returns>The XYChart object representing indicator chart.</returns>
    protected XYChart addIndicator(FinanceChart m, string indicator, int height)
    {
      if (indicator == "RSI")
        return m.addRSI(height, 14, 0x800080, 20, 0xff6666, 0x6666ff);
      else if (indicator == "StochRSI")
        return m.addStochRSI(height, 14, 0x800080, 30, 0xff6666, 0x6666ff);
      else if (indicator == "MACD")
        return m.addMACD(height, 26, 12, 9, 0xff, 0xff00ff, 0x8000);
      else if (indicator == "FStoch")
        return m.addFastStochastic(height, 14, 3, 0x6060, 0x606000);
      else if (indicator == "SStoch")
        return m.addSlowStochastic(height, 14, 3, 0x6060, 0x606000);
      else if (indicator == "ATR")
        return m.addATR(height, 14, 0x808080, 0xff);
      else if (indicator == "ADX")
        return m.addADX(height, 14, 0x8000, 0x800000, 0x80);
      else if (indicator == "DCW")
        return m.addDonchianWidth(height, 20, 0xff);
      else if (indicator == "BBW")
        return m.addBollingerWidth(height, 20, 2, 0xff);
      else if (indicator == "DPO")
        return m.addDPO(height, 20, 0xff);
      else if (indicator == "PVT")
        return m.addPVT(height, 0xff);
      else if (indicator == "Momentum")
        return m.addMomentum(height, 12, 0xff);
      else if (indicator == "Performance")
        return m.addPerformance(height, 0xff);
      else if (indicator == "ROC")
        return m.addROC(height, 12, 0xff);
      else if (indicator == "OBV")
        return m.addOBV(height, 0xff);
      else if (indicator == "AccDist")
        return m.addAccDist(height, 0xff);
      else if (indicator == "CLV")
        return m.addCLV(height, 0xff);
      else if (indicator == "WilliamR")
        return m.addWilliamR(height, 14, 0x800080, 30, 0xff6666, 0x6666ff);
      else if (indicator == "Aroon")
        return m.addAroon(height, 14, 0x339933, 0x333399);
      else if (indicator == "AroonOsc")
        return m.addAroonOsc(height, 14, 0xff);
      else if (indicator == "CCI")
        return m.addCCI(height, 20, 0x800080, 100, 0xff6666, 0x6666ff);
      else if (indicator == "EMV")
        return m.addEaseOfMovement(height, 9, 0x6060, 0x606000);
      else if (indicator == "MDX")
        return m.addMassIndex(height, 0x800080, 0xff6666, 0x6666ff);
      else if (indicator == "CVolatility")
        return m.addChaikinVolatility(height, 10, 10, 0xff);
      else if (indicator == "COscillator")
        return m.addChaikinOscillator(height, 0xff);
      else if (indicator == "CMF")
        return m.addChaikinMoneyFlow(height, 21, 0x8000);
      else if (indicator == "NVI")
        return m.addNVI(height, 255, 0xff, 0x883333);
      else if (indicator == "PVI")
        return m.addPVI(height, 255, 0xff, 0x883333);
      else if (indicator == "MFI")
        return m.addMFI(height, 14, 0x800080, 30, 0xff6666, 0x6666ff);
      else if (indicator == "PVO")
        return m.addPVO(height, 26, 12, 9, 0xff, 0xff00ff, 0x8000);
      else if (indicator == "PPO")
        return m.addPPO(height, 26, 12, 9, 0xff, 0xff00ff, 0x8000);
      else if (indicator == "UO")
        return m.addUltimateOscillator(height, 7, 14, 28, 0x800080, 20, 0xff6666, 0x6666ff);
      else if (indicator == "Vol")
        return m.addVolIndicator(height, 0x99ff99, 0xff9999, 0xc0c0c0);
      else if (indicator == "TRIX")
        return m.addTRIX(height, 12, 0xff);
      return null;
    }

    /// <summary>
    /// Creates a dummy chart to show an error message.
    /// </summary>
    /// <param name="viewer">The WinChartViewer to display the error message.</param>
    /// <param name="msg">The error message</param>
    protected void errMsg(WinChartViewer viewer, string msg)
    {
      MultiChart m = new MultiChart(400, 200);
      m.addTitle2(Chart.Center, msg, "Arial", 10).setMaxWidth(m.getWidth());
      viewer.Image = m.makeImage();
    }
  }
}