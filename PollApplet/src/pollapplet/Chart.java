/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package pollapplet;

import java.awt.BasicStroke;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.GradientPaint;

import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import javax.swing.JFrame;
import org.jfree.chart.ChartFactory;
import org.jfree.chart.ChartPanel;
import org.jfree.chart.JFreeChart;
import org.jfree.chart.axis.CategoryAxis;
import org.jfree.chart.axis.CategoryLabelPositions;
import org.jfree.chart.axis.NumberAxis;
import org.jfree.chart.plot.CategoryPlot;
import org.jfree.chart.plot.PiePlot;
import org.jfree.chart.plot.PlotOrientation;
import org.jfree.chart.renderer.category.BarRenderer;
import org.jfree.chart.renderer.category.LineAndShapeRenderer;
import org.jfree.data.category.CategoryDataset;
import org.jfree.data.general.DefaultPieDataset;
import org.jfree.data.general.PieDataset;
import org.jfree.ui.ApplicationFrame;

/**
 * A simple demonstration application showing how to create a bar chart.
 *
 */
public class Chart extends JFrame {

    /**
     * Creates a new demo instance.
     *
     * @param title  the frame title.
     */
    public Chart(final String title, String graphTitle, CategoryDataset dataset, int chartType) {

        super(title);
        

        //final CategoryDataset dataset = createDataset();
        final JFreeChart chart;
        if(chartType == 1){
            chart = createHorizontalBarChart(dataset, graphTitle);
            
        }else if(chartType == 3){
            chart = createLineChart(dataset, graphTitle);
            
        //}else if(chartType == 4){
        //    chart = createPieChart(graphTitle, dataset);
            
        }else{
            chart = createVerticalBarChart(dataset, graphTitle);
            
        }
        //final JFreeChart chart = createVerticalBarChart(dataset, graphTitle);
        final ChartPanel chartPanel = new ChartPanel(chart);
        chartPanel.setPreferredSize(new Dimension(800, 600));
        

     
        
        setContentPane(chartPanel);

    }

    
    
    private JFreeChart createHorizontalBarChart(final CategoryDataset dataset, String title) {

        // create the chart...
        final JFreeChart chart = ChartFactory.createBarChart(
                title, // chart title
                "Category", // domain axis label
                "Value", // range axis label
                dataset, // data
                PlotOrientation.HORIZONTAL, // orientation
                true, // include legend
                true, // tooltips?
                false // URLs?
                );

        // NOW DO SOME OPTIONAL CUSTOMISATION OF THE CHART...

        // set the background color for the chart...
        chart.setBackgroundPaint(Color.white);


        final CategoryPlot plot = chart.getCategoryPlot();
        final BarRenderer renderer = (BarRenderer) plot.getRenderer();
        renderer.setDrawBarOutline(false);

        plot.setBackgroundPaint(Color.lightGray);
        plot.setDomainGridlinePaint(Color.white);
        plot.setRangeGridlinePaint(Color.white);

        // set the range axis to display integers only...
        final NumberAxis rangeAxis = (NumberAxis) plot.getRangeAxis();
        rangeAxis.setStandardTickUnits(NumberAxis.createIntegerTickUnits());

        // disable bar outlines...


        // set up gradient paints for series...
        final GradientPaint gp0 = new GradientPaint(
                0.0f, 0.0f, Color.BLUE,
                0.0f, 0.0f, Color.BLUE);
        final GradientPaint gp1 = new GradientPaint(
                0.0f, 0.0f, Color.green,
                0.0f, 0.0f, Color.lightGray);
        final GradientPaint gp2 = new GradientPaint(
                0.0f, 0.0f, Color.red,
                0.0f, 0.0f, Color.lightGray);
        renderer.setSeriesPaint(0, gp0);
        renderer.setSeriesPaint(1, gp1);
        renderer.setSeriesPaint(2, gp2);

        final CategoryAxis domainAxis = plot.getDomainAxis();
        domainAxis.setCategoryLabelPositions(
                CategoryLabelPositions.createUpRotationLabelPositions(Math.PI / 6.0));
        // OPTIONAL CUSTOMISATION COMPLETED.

        return chart;

    }
    
    private static PieDataset createPieDataset(CategoryDataset data) {
        DefaultPieDataset dataset = new DefaultPieDataset();
        
        int rows = data.getRowCount();
        System.out.println("row count = "+rows);
        for(int i = 0; i < rows; i++){
            System.out.println("row name "+data.getValue(i, 2).toString()+" data: "+new Double(data.getValue(i, 0).toString()));
            dataset.setValue(data.getValue(i, 1).toString(), new Double(data.getValue(i, 0).toString())); 
        }
        /*
        dataset.setValue("One", new Double(43.2));
        dataset.setValue("Two", new Double(10.0));
        dataset.setValue("Three", new Double(27.5));
        dataset.setValue("Four", new Double(17.5));
        dataset.setValue("Five", new Double(11.0));
        dataset.setValue("Six", new Double(19.4));*/
        return dataset;        
    }
    
    private static JFreeChart createPieChart(String title, CategoryDataset dset) {
        
        PieDataset dataset = createPieDataset(dset);
        
        JFreeChart chart = ChartFactory.createPieChart(
            title,  // chart title
            dataset,             // data
            true,               // include legend
            true,
            false
        );

        PiePlot plot = (PiePlot) chart.getPlot();
        plot.setLabelFont(new Font("SansSerif", Font.PLAIN, 12));
        plot.setNoDataMessage("No data available");
        plot.setCircular(false);
        plot.setLabelGap(0.02);
        return chart;
        
    }
    
    private JFreeChart createLineChart(final CategoryDataset dataset, String title) {
        
        // create the chart...
        final JFreeChart chart = ChartFactory.createLineChart(
            "title",       // chart title
            "Type",                    // domain axis label
            "Value",                   // range axis label
            dataset,                   // data
            PlotOrientation.VERTICAL,  // orientation
            true,                      // include legend
            true,                      // tooltips
            false                      // urls
        );

        // NOW DO SOME OPTIONAL CUSTOMISATION OF THE CHART...
//        final StandardLegend legend = (StandardLegend) chart.getLegend();
  //      legend.setDisplaySeriesShapes(true);
    //    legend.setShapeScaleX(1.5);
      //  legend.setShapeScaleY(1.5);
        //legend.setDisplaySeriesLines(true);

        chart.setBackgroundPaint(Color.white);

        final CategoryPlot plot = (CategoryPlot) chart.getPlot();
        plot.setBackgroundPaint(Color.lightGray);
        plot.setRangeGridlinePaint(Color.white);

        // customise the range axis...
        final NumberAxis rangeAxis = (NumberAxis) plot.getRangeAxis();
        rangeAxis.setStandardTickUnits(NumberAxis.createIntegerTickUnits());
        rangeAxis.setAutoRangeIncludesZero(true);

        // ****************************************************************************
        // * JFREECHART DEVELOPER GUIDE                                               *
        // * The JFreeChart Developer Guide, written by David Gilbert, is available   *
        // * to purchase from Object Refinery Limited:                                *
        // *                                                                          *
        // * http://www.object-refinery.com/jfreechart/guide.html                     *
        // *                                                                          *
        // * Sales are used to provide funding for the JFreeChart project - please    * 
        // * support us so that we can continue developing free software.             *
        // ****************************************************************************
        
        // customise the renderer...
        final LineAndShapeRenderer renderer = (LineAndShapeRenderer) plot.getRenderer();
//        renderer.setDrawShapes(true);

        renderer.setSeriesStroke(
            0, new BasicStroke(
                2.0f, BasicStroke.CAP_ROUND, BasicStroke.JOIN_ROUND,
                1.0f, new float[] {10.0f, 6.0f}, 0.0f
            )
        );
        renderer.setSeriesStroke(
            1, new BasicStroke(
                2.0f, BasicStroke.CAP_ROUND, BasicStroke.JOIN_ROUND,
                1.0f, new float[] {6.0f, 6.0f}, 0.0f
            )
        );
        renderer.setSeriesStroke(
            2, new BasicStroke(
                2.0f, BasicStroke.CAP_ROUND, BasicStroke.JOIN_ROUND,
                1.0f, new float[] {2.0f, 6.0f}, 0.0f
            )
        );
        // OPTIONAL CUSTOMISATION COMPLETED.
        
        return chart;
    }
    
    
    /**
     * Returns a sample dataset.
     * 
     * @return The dataset.
     */
    /**
     * Creates a sample chart.
     * 
     * @param dataset  the dataset.
     * 
     * @return The chart.
     * 
     */
    private JFreeChart createVerticalBarChart(final CategoryDataset dataset, String title) {

        // create the chart...
        final JFreeChart chart = ChartFactory.createBarChart(
                title, // chart title
                "Category", // domain axis label
                "Value", // range axis label
                dataset, // data
                PlotOrientation.VERTICAL, // orientation
                true, // include legend
                true, // tooltips?
                false // URLs?
                );

        // NOW DO SOME OPTIONAL CUSTOMISATION OF THE CHART...

        // set the background color for the chart...
        chart.setBackgroundPaint(Color.white);


        final CategoryPlot plot = chart.getCategoryPlot();
        final BarRenderer renderer = (BarRenderer) plot.getRenderer();
        renderer.setDrawBarOutline(false);

        plot.setBackgroundPaint(Color.lightGray);
        plot.setDomainGridlinePaint(Color.white);
        plot.setRangeGridlinePaint(Color.white);

        // set the range axis to display integers only...
        final NumberAxis rangeAxis = (NumberAxis) plot.getRangeAxis();
        rangeAxis.setStandardTickUnits(NumberAxis.createIntegerTickUnits());

        // disable bar outlines...


        // set up gradient paints for series...
        final GradientPaint gp0 = new GradientPaint(
                0.0f, 0.0f, Color.GREEN.darker(),
                0.0f, 0.0f, Color.lightGray);
        final GradientPaint gp1 = new GradientPaint(
                0.0f, 0.0f, Color.green,
                0.0f, 0.0f, Color.lightGray);
        final GradientPaint gp2 = new GradientPaint(
                0.0f, 0.0f, Color.red,
                0.0f, 0.0f, Color.lightGray);
        renderer.setSeriesPaint(0, gp0);
        renderer.setSeriesPaint(1, gp1);
        renderer.setSeriesPaint(2, gp2);

        final CategoryAxis domainAxis = plot.getDomainAxis();
        domainAxis.setCategoryLabelPositions(
                CategoryLabelPositions.createUpRotationLabelPositions(Math.PI / 6.0));
        // OPTIONAL CUSTOMISATION COMPLETED.

        return chart;

    }
}
