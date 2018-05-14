﻿using SocialNetworkConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterConnection;

namespace WebScrapper
{
    public partial class WebScrapperViewer : Form
    {

        public WebScrapperViewer()
        {
            InitializeComponent();
            InitializeSocialNetworks();
            InitializeDataset();
            IntializeControls();
            InitializeHandlerList();

        }

        private void InitializeHandlerList()
        {
            handlers = new List<IScrapperHandler>();
        }

        private void InitializeDataset()
        {
            dataset = new SearchDataSet();
        }

        private void IntializeControls()
        {
            queriesControl.SetMain(this);
            publicationViewerControl.SetMain(this);

        }

        private void InitializeSocialNetworks()
        {

#pragma warning disable CS4014 // Ya que no se esperaba esta llamada, la ejecución del método actual continúa antes de que se complete la llamada
            InitializeFacebook();
#pragma warning restore CS4014 // Ya que no se esperaba esta llamada, la ejecución del método actual continúa antes de que se complete la llamada
            InitializeTwitter();


        }

        private void InitializeTwitter()
        {
            twitter = new TwitterConnection.Twitter();
            //ParseTwitterCredentials(twitter.Credential, out string consumerKey, out string consumerSecret, out string accessToken, out string secretToken);

        }

        public void AddHandler(IScrapperHandler handler)
        {
            handlers.Add(handler);
        }

        //private void ParseTwitterCredentials(string credential, out string consumerKey, out string consumerSecret, out string accessToken, out string secretToken)
        //{
        //    string[] info = credential.Split('|');
        //    consumerKey = info[0].Trim();
        //    consumerSecret = info[1].Trim();
        //    accessToken = info[2].Trim();
        //    secretToken = info[3].Trim();


        //    Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, secretToken);

        //}

#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
        internal async void Search(IList<IQueryConfiguration> queryConfigurations)
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
        {
            Task task = Task.Run(() =>
            {
                RealizeSearch(queryConfigurations);

            });

            ThreadStart tStart = SearchProccess(task);
            Thread thread = new Thread(tStart);
            thread.Start();

        }

        private void RealizeSearch(IList<IQueryConfiguration> queryConfigurations)
        {
            IList<IPublication> publications = GetSelectedSocialNetwork().Search(queryConfigurations);
            DialogResult result = MessageBox.Show("¿Desea guardar las publicaciones  de twitter con el contenido completo? Puede demorar un poco mas...", "Export content", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ReadHtmlContents(publications);
            }
            this.dataset.AddPublications(publications);
            publications = this.dataset.GetPublications();
            ShowInPublicationViewer delegateMethod = new ShowInPublicationViewer(ShowPublicationsInPublicationViewer);
            this.Invoke(delegateMethod, publications);
        }

        private ThreadStart SearchProccess(Task task)
        {
            return () => { SearchProcessData(task); };
        }

        private void SearchProcessData(Task task)
        {
            Loading del = new Loading(Load);
            int i = 1;
            while (!task.IsCompleted)
            {
                if (i == 4)
                {
                    i = 1;
                }
                this.Invoke(del, i);
                Thread.Sleep(500);
                i++;

            }

            this.Invoke(del, -1);

        }

        private delegate void Loading(int load);

#pragma warning disable CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        public void Load(int load)
#pragma warning restore CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
        {
            if (load == 1)
            {
                lblLoad.Text = "Cargando.";
            }
            else if (load == 2)
            {
                lblLoad.Text = "Cargando..";
            }
            else if (load == 3)
            {
                lblLoad.Text = "Cargando...";
            }
            else if (load == -1)
            {
                lblLoad.Text = "";
            }
        }

        public delegate void ShowInPublicationViewer(IList<IPublication> publications);

        internal void ImportQueryConfiguration()
        {
            Thread thread = new Thread(ShowImportDialog());
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        private delegate void AddQueryConfiguration(IQueryConfiguration queryConfig);

        private void AddQueryConfigurationToQueryControl(IQueryConfiguration queryConfig)
        {
            queriesControl.AddQueryConfigurations(queryConfig);
        }

        private ThreadStart ShowImportDialog()
        {
            return () => { ImportFromDialog(); };
        }

        private ThreadStart ShowExportDialog(IQueryConfiguration queryConfiguration)
        {
            return () => { ExportToDialog(queryConfiguration); };
        }

        private void ExportToDialog(IQueryConfiguration queryConfiguration)
        {
            if (queryConfiguration != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "qcn files(.qnc)|*.qnc",
                    InitialDirectory = "..//..//..//SocialNetworkConnection/Resources/",
                    DefaultExt = "qnc",
                    ValidateNames = true,
                };

                if (DialogResult.OK == saveDialog.ShowDialog())
                {
                    StreamWriter sw = new StreamWriter(saveDialog.OpenFile());
                    sw.Write(queryConfiguration.ToExportFormat());
                    sw.Close();
                }
            }
        }
        private void ImportFromDialog()
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "qcn files (.qnc)|*.qnc",
                InitialDirectory = "..//..//..//SocialNetworkConnection/Resources/",
                DefaultExt = "qnc",
                ValidateNames = true,

            };



            if (DialogResult.OK == openDialog.ShowDialog())
            {
                StreamReader sr = new StreamReader(openDialog.OpenFile());
                string queryFormat = sr.ReadToEnd();
                sr.Close();
                IQueryConfiguration queryConfig = new QueryConfiguration(queryFormat);
                AddQueryConfiguration delegateMethod = new AddQueryConfiguration(AddQueryConfigurationToQueryControl);
                this.Invoke(delegateMethod, queryConfig);
            }
        }

        internal void ShowPublicationsInPublicationViewer(IList<IPublication> publications)
        {
            publicationViewerControl.ShowPublications(publications);

        }

        internal void ExportQueryConfiguration(IQueryConfiguration currentConfiguration)
        {
            Thread thread = new Thread(ShowExportDialog(currentConfiguration));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private ISocialNetwork GetSelectedSocialNetwork()
        {
            if (selectedSocialNetwork == FACEBOOK)
            {
                return facebook;
            }
            else if (selectedSocialNetwork == TWITTER)
            {
                return twitter;
            }
            else
            {
                return null;
            }
        }

        private async Task InitializeFacebook()
        {
            await Task.Run(() => facebook = new FacebookConnection.Facebook());
        }



        private void RdbCheckedChanged(object sender, EventArgs e)
        {
            if (sender == rdbFacebook)
            {
                selectedSocialNetwork = FACEBOOK;
            }
            else if (sender == rdbTwitter)
            {
                selectedSocialNetwork = TWITTER;
            }
        }



        public void SavePublications(IPublication[] publications)
        {
            if (publications != null)
            {
                Task task = Task.Run(() =>
                {
                    dataset.AddPublications(publications);
                });

                Thread thread = new Thread(SearchProccess(task));
                thread.Start();
            }
            else
            {
                MessageBox.Show("Antes de guardar debe hacer una busqueda");
            }
        }


        public void ExportPublications(int quantity)
        {

            if (dataset.TotalPublications > 0 && quantity != 0)
            {
                Thread thread = new Thread(ExportTS(dataset, quantity));
                thread.SetApartmentState(ApartmentState.STA);
                Thread threadProcess = new Thread(ThreadProcessTS(thread));
                threadProcess.SetApartmentState(ApartmentState.STA);
                threadProcess.Start();
            }
            else
            {
                MessageBox.Show("Antes de exportar debe guardar las publicaciones");
            }
        }

        private void Export(ISearchDataSet dataset, int quantity)
        {
            DialogResult result = MessageBox.Show("¿Desea exportar las publicaciones (publicaciones de twitter) con el contenido completo?", "Export content", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                IList<IPublication> publications = dataset.GetPublications();
                ReadHtmlContents(publications);
                dataset.AddOrReplacePublications(publications);

            }

            // FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            // DialogResult resultFolderDialog = folderDialog.ShowDialog();
            // if (resultFolderDialog == DialogResult.OK)
            // {
            //    string folderName = folderDialog.SelectedPath;


            IQueryConfiguration queryConfiguration = queriesControl.GetCurrentQueryConfiguration();
                       string folderName = "..//..//..//Database//LemmatizedPublications";
                dataset.BasePath = folderName + "/";

            InvokeHandlers invokeHandlers = new InvokeHandlers(InvokeScrapperHandlers);
            this.Invoke(invokeHandlers);
            //  }

        }

        public delegate void InvokeHandlers();

        private void InvokeScrapperHandlers()
        {
            handlers.ForEach(x => x.ExportEventHandler());
        }

      

        private void ReadHtmlContents(IList<IPublication> publications)
        {
            for (int i = 0; i < publications.Count; i++)
            {

                if (Publication.IsTweet(publications[i]))
                {
                    string strIdPublication = publications[i].Id.Split(':')[1];
                    long idTweet = long.Parse(strIdPublication);
                    string message = Twitter.ReadHtmlContent(idTweet);
                    if (message != null)
                    {
                        publications[i].Message = message;
                    }
                }



            }
        }

        private ThreadStart ExportTS(ISearchDataSet dataset, int quantity)
        {
            return () => { Export(dataset, quantity); };
        }

        public void ImportPublications()
        {
            Thread thread = new Thread(ImportTS(dataset));
            thread.SetApartmentState(ApartmentState.STA);
            Thread threadProcess = new Thread(ThreadProcessTS(thread));
            threadProcess.SetApartmentState(ApartmentState.STA);
            threadProcess.Start();

        }



        private ThreadStart ImportTS(ISearchDataSet dataset)
        {
            return () => { Import(dataset); };
        }

        private void Import(ISearchDataSet dataset)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderDialog.SelectedPath;
                dataset.BasePath = folderName + "/";
                IList<IPublication> publications = dataset.ImportDataset();

                ShowInPublicationViewer delegateMethod = new ShowInPublicationViewer(ShowPublicationsInPublicationViewer);
                this.Invoke(delegateMethod, publications);
            }


        }

        public ISearchDataSet GetSearchDataSet()
        {

            return dataset;
        }

        private ThreadStart ThreadProcessTS(Thread thread)
        {
            return () => { ThreadProcess(thread); };
        }
        private void ThreadProcess(Thread thread)
        {
            thread.Start();
            Loading del = new Loading(Load);
            int i = 1;
            while (thread.IsAlive)
            {
                if (i == 4)
                {
                    i = 1;
                }
                this.Invoke(del, i);
                Thread.Sleep(500);
                i++;

            }

            this.Invoke(del, -1);
        }


    }
}