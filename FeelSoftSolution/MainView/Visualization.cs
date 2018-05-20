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
using Controller;

namespace MainView
{
    public partial class Visualization : UserControl
    {
        public Visualization(MainFrame frame)
        {
            main = frame;
            InitializeComponent();
            createChecksButtons();

        }

        private void Visualization_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        public void createChecksButtons()
        {

            foreach(string candidate in main.Controller.Candidates.Keys)
            {
                chListBox.Items.Add(candidate);
                
            }
        }

        public void loadGraphic()
        {

            DateTime startDate = dtpStart.Value;
            DateTime finishDate = dtpEnd.Value;
            chart1.Series.Clear();
            chart2.Series.Clear();
            CheckedListBox.CheckedItemCollection checkboxs = chListBox.CheckedItems;
            if(checkboxs.Count == 0)
            {
                MessageBox.Show("Seleccione algun candidato");
            }
            foreach ( string checkbox in checkboxs)
            {             
                    Boolean anyNetWorkChecked = false;
                    Boolean anyTecniqueChecked = false;
                    

                    if (cbNaive.Checked)
                    {
                        anyTecniqueChecked = true;

                        if (cbTwitter.Checked)
                        {
                            anyNetWorkChecked = true;
                            QueryInformation information = new QueryInformation(TypeSocialNetwork.Twitter, TypeAnalysisTechnique.NaiveBaye, startDate, finishDate);
                            Dictionary<DateTime, double[]> hashCandidate = main.Controller.DailyAnalysis(checkbox, information);
                            createSerie(checkbox, information, hashCandidate);
                            createSerieNumPublications(checkbox, information, hashCandidate);
                        }
                        if (cbFacebook.Checked)
                        {
                            anyNetWorkChecked = true;
                            QueryInformation information = new QueryInformation(TypeSocialNetwork.Facebook, TypeAnalysisTechnique.NaiveBaye, startDate, finishDate);
                            Dictionary<DateTime, double[]> hashCandidate = main.Controller.DailyAnalysis(checkbox, information);
                            createSerie(checkbox, information, hashCandidate);
                            createSerieNumPublications(checkbox, information, hashCandidate);
                    }
                        if (cbBoth.Checked)
                        {
                            anyNetWorkChecked = true;
                            QueryInformation information = new QueryInformation(TypeSocialNetwork.Both, TypeAnalysisTechnique.NaiveBaye, startDate, finishDate);
                            Dictionary<DateTime, double[]> hashCandidate = main.Controller.DailyAnalysis(checkbox, information);
                            createSerie(checkbox, information, hashCandidate);
                            createSerieNumPublications(checkbox, information, hashCandidate);
                        }
                    }

                    if (cbDictionary.Checked)
                    {
                        anyTecniqueChecked = true;
                        if (cbTwitter.Checked)
                        {
                            anyNetWorkChecked = true;
                            QueryInformation information = new QueryInformation(TypeSocialNetwork.Twitter, TypeAnalysisTechnique.Dictionary, startDate, finishDate);
                            Dictionary<DateTime, double[]> hashCandidate = main.Controller.DailyAnalysis(checkbox, information);
                            createSerie(checkbox, information, hashCandidate);
                            createSerieNumPublications(checkbox, information, hashCandidate);
                    }
                        if (cbFacebook.Checked)
                        {
                            anyNetWorkChecked = true;
                            QueryInformation information = new QueryInformation(TypeSocialNetwork.Facebook, TypeAnalysisTechnique.Dictionary, startDate, finishDate);
                            Dictionary<DateTime, double[]> hashCandidate = main.Controller.DailyAnalysis(checkbox, information);
                            createSerie(checkbox, information, hashCandidate);
                            createSerieNumPublications(checkbox, information, hashCandidate);
                    }
                        if (cbBoth.Checked)
                        {
                            anyNetWorkChecked = true;
                            QueryInformation information = new QueryInformation(TypeSocialNetwork.Both, TypeAnalysisTechnique.Dictionary, startDate, finishDate);
                            Dictionary<DateTime, double[]> hashCandidate = main.Controller.DailyAnalysis(checkbox, information);
                            createSerie(checkbox, information, hashCandidate);
                            createSerieNumPublications(checkbox, information, hashCandidate);
                        }
                    }

                    if(!anyNetWorkChecked || !anyTecniqueChecked)
                    {
                        MessageBox.Show("Faltan camposs por sellecionar");
                    }

                    chart1.GetToolTipText += chart1_GetToolTipText;
                    chart2.GetToolTipText += chart2_GetToolTipText;
                
            }
        }


        public void createSerie(string checkbox, QueryInformation query,Dictionary<DateTime, double[]> hashCandidate)
        {
            List<DateTime> time = hashCandidate.Keys.ToList();
            time.Sort();

            string value = "";
            TypeSocialNetwork querySocial = query.TypeSocialNetwork;
            TypeAnalysisTechnique queryAnalysis = query.TypeAnalysis;
            switch (querySocial)
            {
                case TypeSocialNetwork.Facebook:
                    value = "-FB";
                    break;
                case TypeSocialNetwork.Twitter:
                    value = "-TW";
                    break;
                case TypeSocialNetwork.Both:
                    value = "-AMBAS";
                    break;
            }
            string network = "";
            switch (queryAnalysis)
            {
                case TypeAnalysisTechnique.Dictionary:
                    network = "-DC";
                    break;
                case TypeAnalysisTechnique.NaiveBaye:
                    network = "-NB";
                    break;
            
            }
            Boolean anyShowChecked = false;

            if (cbFavorability.Checked)
            {
                anyShowChecked = true;
                Series serie = new Series();
                serie.Name = checkbox + value + network + " + ";
                serie.BorderDashStyle = ChartDashStyle.Solid;
                serie.ChartArea = "ChartArea1";
                serie.Legend = "Legend1";

                chart1.Series.Add(serie);

                chart1.Series[checkbox + value + network + " + "].ChartType = SeriesChartType.Line;
                chart1.Series[checkbox + value + network + " + "].XValueType = ChartValueType.DateTime;
            }
            if (cbUnfavorable.Checked)
            {
                anyShowChecked = true;
                Series serie2 = new Series();
                serie2.Name = checkbox + value + network + " - ";
                serie2.ChartArea = "ChartArea1";
                serie2.Legend = "Legend1";
                serie2.BorderDashStyle = ChartDashStyle.DashDot;
                

                chart1.Series.Add(serie2);

                chart1.Series[checkbox + value + network + " - "].ChartType = SeriesChartType.Line;
                chart1.Series[checkbox + value + network + " - "].XValueType = ChartValueType.DateTime;
            }
            if (cbNeutro.Checked)
            {
                anyShowChecked = true;
                Series serie3 = new Series();
                serie3.Name = checkbox + value + network;
                serie3.ChartArea = "ChartArea1";
                serie3.Legend = "Legend1";
                serie3.BorderDashStyle = ChartDashStyle.Dot;


                chart1.Series.Add(serie3);

                chart1.Series[checkbox + value + network].ChartType = SeriesChartType.Line;
                chart1.Series[checkbox + value + network].XValueType = ChartValueType.DateTime;
            }
            if (!anyShowChecked)
            {
                MessageBox.Show("Marque algun campo para mostrar (favorabilidad neutro o desfavorabilidad)");
            }
            for (int i = 0; i < hashCandidate.Count; i++)

            {
                if (cbFavorability.Checked)
                {
                    chart1.Series[checkbox + value + network + " + "].Points.AddXY(time[i], hashCandidate[time[i]][0] * 100);
                }

                if (cbUnfavorable.Checked)
                {
                    chart1.Series[checkbox + value + network + " - "].Points.AddXY(time[i], hashCandidate[time[i]][1] * 100);
                }

                if (cbNeutro.Checked)
                {
                    chart1.Series[checkbox + value + network].Points.AddXY(time[i], (1 - hashCandidate[time[i]][0] - hashCandidate[time[i]][1]) * 100);
                }

            }
        }

        public void createSerieNumPublications(string checkbox, QueryInformation query, Dictionary<DateTime, double[]> hashCandidate)
        {
            List<DateTime> time = hashCandidate.Keys.ToList();
            time.Sort();

            string value = "";
            TypeSocialNetwork querySocial = query.TypeSocialNetwork;
            TypeAnalysisTechnique queryAnalysis = query.TypeAnalysis;
            switch (querySocial)
            {
                case TypeSocialNetwork.Facebook:
                    value = "-FB";
                    break;
                case TypeSocialNetwork.Twitter:
                    value = "-TW";
                    break;
                case TypeSocialNetwork.Both:
                    value = "-AMBAS";
                    break;
            }
            string network = "";
            switch (queryAnalysis)
            {
                case TypeAnalysisTechnique.Dictionary:
                    network = "-DC";
                    break;
                case TypeAnalysisTechnique.NaiveBaye:
                    network = "-NB";
                    break;

            }
            
                Series serie = new Series();
                serie.Name = checkbox + value + network;
                serie.BorderDashStyle = ChartDashStyle.Solid;
                serie.ChartArea = "ChartArea1";
                serie.Legend = "Legend1";

            if (!chart2.Series.Contains(serie))
            {
                chart2.Series.Add(serie);
            }
                chart2.Series[checkbox + value + network].ChartType = SeriesChartType.Column;
                chart2.Series[checkbox + value + network].XValueType = ChartValueType.DateTime;
           

            for (int i = 0; i < hashCandidate.Count; i++)

            {           
                chart2.Series[checkbox + value + network].Points.AddXY(time[i], hashCandidate[time[i]][2]);
            }
        }

        private void chart1_GetToolTipText(object sender, ToolTipEventArgs e)
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

        private void chart2_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            // Check selected chart element and set tooltip text for it
            switch (e.HitTestResult.ChartElementType)
            {

                case ChartElementType.DataPoint:

                    var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    e.Text = String.Format(""+dataPoint.YValues[0]);
                    break;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            loadGraphic();

        }
    }
}
