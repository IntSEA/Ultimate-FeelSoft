using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;
using ViewQualifier;
using TextualProcessor;
using SocialNetworkConnection;
using WebScrapper;

namespace MainView
{
    public partial class MainFrame : Form, IScrapperHandler
    {
        public MainFrame()
        {
            InitializeComponent();
            InitializeControls();
            Controller = new Controller.Controller();
            ShowFormHome();
            

        }

        private void ShowFormHome()
        {
            Controller.Update();
            containerPanel.Tag = home;
            home.Show();
            this.WindowState = FormWindowState.Normal;
        }

        public void ShowFormVisualization()
        {
            this.WindowState = FormWindowState.Maximized;
            visualization = new Visualization(this);
            containerPanel.Controls.Clear();
            containerPanel.Controls.Add(visualization);
            containerPanel.Tag = visualization;
            visualization.Show();
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            containerPanel.Controls.Clear();
            ReportPane report = new ReportPane(controller);
            containerPanel.Controls.Add(report);
            containerPanel.Tag = report;
            report.Show();

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            containerPanel.Controls.Clear();
            InitializeControls();
            ShowFormHome();
            webScrapperViewer = new WebScrapperViewer();
            webScrapperViewer.AddHandler(this);
            webScrapperViewer.Show();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            
            containerPanel.Controls.Clear();
            InitializeControls();
            ShowFormHome();
        }


        private void InitializeControls()
        {
            home = new HomePanel(this)
            {
                Dock = DockStyle.Fill,
                TopLevel = false
            };
            containerPanel.Controls.Add(home);
        }

        private void BtnQualification_Click(object sender, EventArgs e)
        {
            viewQualifier = new Calificador();
            viewQualifier.Show();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {

        }

      

        public void ExportEventHandler()
        {
            ISearchDataSet dataset = webScrapperViewer.GetSearchDataSet();
            ToLemmatizedPublications(dataset);
            ToExportPublications(dataset);

        }

        public void ToLemmatizedPublications(ISearchDataSet dataset)
        {

            //TODO (CREATE A METHOD WHERE PUBlICATIONS'LL LEMMATIZE AND EXPORT IN ANY FORMAT)
            IProcessor processor = new Processor();
            var publications = processor.LemmatizedPublications(dataset);

            dataset.AddOrReplacePublications(publications);


        }

        public void ToExportPublications(ISearchDataSet dataset)
        {
           
                dataset.ExportDataSet(-1);
            
        }
    }
}
