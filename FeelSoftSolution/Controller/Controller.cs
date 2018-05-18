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
        
        private string path = "..//..//..//LemmatizedPublications";
        private IProcessor processor;
        private ISearchDataSet dataSet;
        private NaiveAnalytic naiveBayes;
        private DictionaryAn dictionaryAn;

        private Dictionary<string, Candidate> candidates;

        public Dictionary<string, Candidate> Candidates { get => candidates; set => candidates = value; }

        public Controller()
        {
           
            processor = new Processor();
            dataSet = new SearchDataSet();
            dictionaryAn = new DictionaryAn();
            naiveBayes = new NaiveAnalytic();
            candidates = new Dictionary<string, Candidate>();
            LoadPublications();
        }

        public IDictionary<int, double> DataWords(out int allWords)
        {
            IDictionary<string, int> words = naiveBayes.GetWordbank();
            IDictionary<int, double> reto = new Dictionary<int, double>();
            allWords = words.Count;
            var group = words.Values.GroupBy(x=>x);
            foreach (var item in group)
            {
                reto.Add(item.Key, (1.0 * item.Count()) / allWords);
            }

            return reto;
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

        public string FailTrainig()
        {
            return (100 * naiveBayes.FailTrainig).ToString("#.##") + "%";
        }

        public string FailDecided()
        {
            return (100 * naiveBayes.FailDecided).ToString("#.##") + "%";
        }

        public IDictionary<int, double> DataPublications(out int allS)
        {
            int[] outPut = naiveBayes.DataTestOutputTrainig;
            allS = outPut.Length;
            var group = outPut.GroupBy(x=>x);
            IDictionary<int, double> retorno = new Dictionary<int, double>();
            foreach (var item in group)
            {
                retorno.Add(item.Key, (1.0*item.Count()/allS));
            }
            return retorno;
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

            foreach (IPublication publication in candidates[candidate].Publications)
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

                double[] favorsAndNum = new double[3];

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
                    foreach (string[] words in toQualification)
                    {
                        int qualification = naiveBayes.Decided(words);

                        if (qualification ==1)
                        {
                            countPositive++;
                        }
                        else if (qualification == -1)
                        {
                            countNegative++;
                        }
                    } 
                }

                favorability = (countPositive / toQualification.Count);
                desfavorability = (countNegative / toQualification.Count);
                favorsAndNum[0] = favorability;
                favorsAndNum[1] = desfavorability;
                favorsAndNum[2] = group.Count();

                dateAndCalification.Add(group.Key, favorsAndNum);
            }

            return dateAndCalification;
        }






    }
}
