using System;
using System.Collections.Generic;
using TextualProcessor;
using SocialNetworkConnection;
using System.Linq;
using Analytics;
using AnalyticDictionary;
using NaiveBayes;



namespace Controller
{
    public class Controller
    {
        
        private string path = "..//..//..//Database//LemmatizedPublications";
        private IProcessor processor;
        private ISearchDataSet dataSet;
        private ISocialNetwork twitter;
        private ISocialNetwork facebook;
       // private IAnalityc naiveBayes;
        private DictionaryAn dictionaryAn;
        private Dictionary<string, Candidate> candidates;

        public Dictionary<string, Candidate> Candidates { get => candidates; set => candidates = value; }

        public Controller()
        {
           
            processor = new Processor();
            dataSet = new SearchDataSet();
            dictionaryAn = new DictionaryAn();
           // naiveBayes = new NaiveAnalytic();
            candidates = new Dictionary<string, Candidate>();
            LoadPublications();
        }

      

        public void LoadPublications()
        {
            dataSet.BasePath = path;
            IList<IPublication> publications = dataSet.ImportDataset();

            foreach(IPublication publication in publications)
            {
                if (Candidates.ContainsKey(publication.ConfigurationName))
                {
                    Candidates[publication.ConfigurationName].Publications.Add(publication);
                }
                else
                {
                    Candidate novel = new Candidate(publication.ConfigurationName);
                    novel.Publications.Add(publication);
                    Candidates.Add(novel.Name, novel);
                }
            }
        }

        
        public void AutomaticSearch()
        {

        }

        /// <summary>
        /// Retorna el una lista con el numero comentarios en cada fecha
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public List<string[]> ListPublicationsOnEachDate(string candidate, QueryInformation query)
        {
            
            IList<IPublication> publicationsBetweenDates = new List<IPublication>();
            Dictionary<DateTime, double[]> dateAndCalification = new Dictionary<DateTime, double[]>();

            foreach (IPublication publication in Candidates[candidate].Publications)
            {
                if (query.SatisfiesQuery(publication))
                {
                    publicationsBetweenDates.Add(publication);
                }
            }

            var groupsOfDates = publicationsBetweenDates.OrderBy(publica => publica.CreateDate).GroupBy(publica => publica.CreateDate);
            List<string[]> numPublicationForDate = new List<string[]>();

            foreach (var group in groupsOfDates)
            {
                int counter = 0;
                foreach (var item in group)
                {
                    counter++;
                }
                string[] dateAndCali = new string[2];
                dateAndCali[0] = group.Key.ToString().Split(' ')[0];
                dateAndCali[1] = counter+"";
                numPublicationForDate.Add(dateAndCali);
            }
            return numPublicationForDate;
        }

       

        public Dictionary<DateTime,double[]> DailyAnalysis(string candidate, QueryInformation query)
        {     
            IList<IPublication> publicationsBetweenDates = new List<IPublication>();
            Dictionary<DateTime, double[]> dateAndCalification = new Dictionary<DateTime, double[]>();

            foreach (IPublication publication in Candidates[candidate].Publications)
            {
                if(query.SatisfiesQuery(publication))
                {
                    publicationsBetweenDates.Add(publication);                   
                }
            }

            var groupsOfDates = publicationsBetweenDates.GroupBy(publica => publica.CreateDate);

            foreach(var group in groupsOfDates)
            {
                IList<string[]> toQualification = new List<string[]>();
                
                foreach(var publication in group)
                {
                    toQualification.Add(publication.LemmatizedMessage.Split(' '));
                }

                double[] favorAndDesfavor = new double[2];

                double favorability = 0.0;
                double desfavorability = 0.0;

                double countPositive = 0.0;
                double countNegative = 0.0;

                if (query.TypeAnalysis.Equals(TypeAnalysisTechnique.Dictionary))
                {
                    foreach (string[] words in toQualification)
                    {
                        int qualification = dictionaryAn.DecidedSentence(words);

                        if (qualification > 5)
                        {
                            countPositive++;
                        }
                        else if (qualification < -5)
                        {
                            countNegative++;
                        }
                    }
                }
                else
                {
                  /*  foreach (string[] words in toQualification)
                    {
                        int qualification = naiveBayes.Decided(words);

                        if (qualification > 5)
                        {
                            countPositive++;
                        }
                        else if (qualification < -5)
                        {
                            countNegative++;
                        }
                    } */
                }

                favorability = (countPositive / toQualification.Count);
                desfavorability = (countNegative / toQualification.Count);
                favorAndDesfavor[0] = favorability;
                favorAndDesfavor[1] = desfavorability;

                dateAndCalification.Add(group.Key, favorAndDesfavor);
            }

            return dateAndCalification;
        }






    }
}
