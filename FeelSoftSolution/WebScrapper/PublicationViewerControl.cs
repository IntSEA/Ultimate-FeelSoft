using Microsoft.VisualBasic;
using SocialNetworkConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Tweetinvi;
using Tweetinvi.Streaming;
using TwitterConnection;

namespace WebScrapper
{
    public partial class PublicationViewerControl : UserControl
    {
        public static List<IPublication> publications2 = new List<IPublication>();
        public static Thread threadStream;
        public static IFilteredStream stream;
        public static Thread threadShow;

        public PublicationViewerControl()
        {
            InitializeComponent();
            this.publications = new List<IPublication>();
        }

        public void SetMain(WebScrapperViewer main)
        {
            this.main = main;

        }

        private void InitStreams()
        {
            //borrar despues          

            List<IQueryConfiguration> configs = main.GetCurrentsConfigurations();



            if (configs.Count > 0 && threadStream == null)
            {
                stream = Stream.CreateFilteredStream();
                stream.TweetMode = TweetMode.Extended;
                foreach (var config in configs)
                {
                    foreach (var key in config.Keywords)
                    {
                        stream.AddTrack(key);
                    }

                }

                threadStream = new Thread(() =>
                {
                    stream.MatchingTweetReceived += (sender, args) =>
                    {
                        IPublication publication = TwitterSearcher.ParseTweetToPublication(args.Tweet, configs[0]);
                        lock (this)
                        {
                            publications2.Add(publication);
                        }

                    };
                    stream.StartStreamMatchingAllConditions();
                });
                threadStream.Start();



                threadShow = new Thread(() =>
                {

                    while (publications2.Count >= 0)
                    {
                        if (publications2.Count % 10 == 0)
                        {
                            show sho = new show(ShowPublications);
                            this.Invoke(sho, publications2);
                        }
                        else
                        {
                            Thread.Sleep(500);
                        }
                    }
                });

                threadShow.Start();
                //Recordar las privacidades queries, y configurations, quitar static de parse tweet y quitar
                //delegado
            }
            else
            {
                StopStreams();
            }

        }

        private void StopStreams()
        {
            if (threadStream != null && stream != null)
            {

                threadStream.Abort();
                threadShow.Abort();
                threadShow = null;
                threadStream = null;
                stream.PauseStream();
                stream = null;
            }
        }

        delegate void show(IList<IPublication> publications);

        internal void ShowPublications(IList<IPublication> publications)
        {
            if (publications == null)
            {
                //MessageBox.Show("No se encontraron publicaciones");
            }
            else if (publications.Count == 0)
            {
                //MessageBox.Show("No se encontraron publicaciones.");
            }
            else if (publications.Count > 0)
            {
                //MessageBox.Show("Publicaciones encontradas");
                lock (this)
                {
                    
                    this.publications.AddRange((IPublication[])publications2.ToArray().Clone());
                    publications2.Clear();
                    SetDefaultViewConfigToPublications();
                }
            }

        }

        private void SetDefaultViewConfigToPublications()
        {
            ShowPublication(indexCurrentPublications);
            lblTotalPublications.Text = "Publicaciones : " + this.publications.Count;
        }


        private void ShowPublication(int indexCurrentPublication)
        {
            if (publications.Count > 0)
            {
                lock (this)
                {
                    if (indexCurrentPublication < 0 || indexCurrentPublication > publications.Count)
                    {
                        indexCurrentPublication = 0;
                    }
                    IPublication publication = publications.ElementAt(indexCurrentPublication);
                    string id = publication.Id;
                    string wroteBy = publication.WroteBy;
                    string createDate = publication.CreateDate.ToShortDateString();
                    string message = publication.Message;
                    string location = publication.Location.ToString();
                    string language = publication.Language.ToString();
                    string info = id + "\r\n" + wroteBy + "\r\n" + createDate + "\r\n" + message + "\r\n" + location + "\r\n" + language + "\r\n";

                    tbxPublication.Text = info;
                    numericUpDown.Value = indexCurrentPublication + 1;

                    bool isTwitter = IsTwitterPublication(publications[indexCurrentPublications]);
                    ToEnableFullText(isTwitter);
                }

            }
        }

        private void BtnNextClick(object sender, EventArgs e)
        {
            SetNextPublication();

        }

        private void SetNextPublication()
        {
            if (publications != null)
            {
                if (indexCurrentPublications + 1 < publications.Count)
                {
                    ++indexCurrentPublications;
                    ShowPublication(indexCurrentPublications);
                }
            }
        }

        private void BtnBeforeClick(object sender, EventArgs e)
        {
            SetBeforePublication();

        }

        private void SetBeforePublication()
        {
            if (publications != null)
            {
                if (indexCurrentPublications - 1 >= 0)
                {
                    --indexCurrentPublications;
                    ShowPublication(indexCurrentPublications);
                }
            }
        }

        private void NumericUpDownValueChanged(object sender, EventArgs e)
        {
            decimal indexValue = numericUpDown.Value;
            if (!TryShowPublicationInIndex(indexValue))
            {
                numericUpDown.Value = indexCurrentPublications;
            }
        }

        private bool TryShowPublicationInIndex(decimal indexValue)
        {
            bool showed = indexValue <= publications.Count && indexValue >= 0;

            if (showed)
            {
                indexCurrentPublications = (int)indexValue - 1;
                ShowPublication(indexCurrentPublications);
            }
            else
            {
                MessageBox.Show("Ingrese un dato valido, mayor a 1 y menor al total de publicaciones");
            }

            bool isTwitter = IsTwitterPublication(publications[indexCurrentPublications]);
            ToEnableFullText(showed && isTwitter);

            return showed;

        }

        private bool IsTwitterPublication(IPublication publication)
        {
            if (publication != null)
            {
                return Publication.IsTweet(publication);
            }
            return false;

        }

        private void ToEnableFullText(bool showed)
        {
            btnViewFullText.Visible = showed;
        }

        internal IPublication[] GetPublications()
        {
            return publications.ToArray();
        }

        private void BtnSavePublications_Click(object sender, EventArgs e)
        {
            StopStreams();
            //AQUI
            if (publications2.Count > 0)
            {
                lock (this)
                {
                    this.publications.AddRange(publications2);
                }

            }
            lock (this)
            {
                main.SavePublications((IPublication[])this.publications.ToArray().Clone());
            }
        }

        private void BtnImportPublications_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                main.ImportPublications();

            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }



        private void BtnExportPublications_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea guardar las publicaciones por paquetes?", "Exportar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                string quantity = Interaction.InputBox("Ingrese cantidad");
                int.TryParse(quantity, out int q);

                main.ExportPublications(q);
            }
            else if (result == DialogResult.No)
            {
                main.ExportPublications(-1);
            }
        }

        private void BtnViewFullText_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(InitHtmlProcess(indexCurrentPublications));
            thread.Start();

        }

        private ThreadStart InitHtmlProcess(int indexCurrentPublications)
        {
            return () => { ReadHtmlInfo(indexCurrentPublications); };
        }

        private void ReadHtmlInfo(int i)
        {
            string strIdPublication = publications[i].Id.Split(':')[1];
            long idTweet = long.Parse(strIdPublication);
            publications[i].Message = Twitter.ReadHtmlContent(idTweet);
            RefreshViewer del = new RefreshViewer(RefreshTextBox);
            this.Invoke(del);
        }

        public delegate void RefreshViewer();

        public void RefreshTextBox()
        {
            ShowPublication(indexCurrentPublications);
        }

        private void ButtonStreams_Click(object sender, EventArgs e)
        {
            InitStreams();
        }

        internal void ActivateSecret()
        {
            if (btnStreams.Enabled)
            {
                StopStreams();
            }
            btnStreams.Enabled = !btnStreams.Enabled;
            btnStreams.Visible = !btnStreams.Visible;
        }
    }
}
