using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace MainView
{
    public partial class ReportPane : UserControl
    {
        public ReportPane(Controller.Controller contr)
        {
            controller = contr;

            InitializeComponent();
            PaintGraphics();
        }

        private void ReportPane_Load(object sender, EventArgs e)
        {

        }
        private void PaintGraphics()
        {
           // controller.UpdateDictionary();
            words.Series.Clear();
            sentences.Series.Clear();
            IDictionary<int, double> data = controller.DataWords(out int allWords);
            Series seri = new Series("Total palabras :" + allWords);
            seri.Color = Color.Red;
            List<int> keys = data.Keys.OrderBy(x => x).ToList();
            foreach (var item in keys)
            {
                data.TryGetValue(item, out double y);
                seri.Points.AddXY(item, y*100);
            }
            words.Series.Add(seri);
            IDictionary<int, double> pub = controller.DataPublications(out int allS);
            keys = pub.Keys.OrderBy(x => x).ToList();
            Series sente = new Series("Total publicaciones :" + allS);

            foreach (var item in keys)
            {
                pub.TryGetValue(item, out double y);
                sente.Points.AddXY(item, y*100);
            }
            sentences.Series.Add(sente);
            failDecided.Text = controller.FailDecided();
            failTraining.Text = controller.FailTrainig();

            words.GetToolTipText += words_GetToolTipText;
            sentences.GetToolTipText += words_GetToolTipText;
        }


        private void words_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            // Check selected chart element and set tooltip text for it
            switch (e.HitTestResult.ChartElementType)
            {

                case ChartElementType.DataPoint:

                    var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    e.Text = String.Format("{0:00%}", dataPoint.YValues[0]/100);
                    break;
            }
        }

        private void sentences_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            // Check selected chart element and set tooltip text for it
            switch (e.HitTestResult.ChartElementType)
            {

                case ChartElementType.DataPoint:

                    var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    e.Text = String.Format("{0:00%}", dataPoint.YValues[0]/100);
                    break;
            }
        }
    }

}