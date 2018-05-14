using Newtonsoft.Json;
using SocialNetworkConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FacebookConnection
{
    public class FacebookSearcher : PublicationSearcher
    {
        private HttpClient client;
        private const int LIMIT_PUBLICATIONS = 100;
        private IList<string> pages;


        public FacebookSearcher(HttpClient client, string credential) : base(credential)
        {
            this.client = client;
            InitializeDefaultPages();

        }

        private void InitializeDefaultPages()
        {
            pages = new List<string>();
            ReadPages();

        }

        private void ReadPages()
        {
            StreamReader sr = new StreamReader("..//..//..//FacebookConnection/Resources/DefaultPages.txt");
            string line = null;
            while ((line = sr.ReadLine()) != null)
            {
                pages.Add(line);
            }
            sr.Close();
        }

        public FacebookSearcher(HttpClient client) : base()
        {
            this.client = client;
            InitializeDefaultPages();
        }

        public override IList<IPublication> SearchPublications(IList<IQueryConfiguration> queriesConfigurations)
        {
            IList<IPublication> publications = InternalSearch(queriesConfigurations);
            return publications;
        }

        internal IList<IPublication> InternalSearch(IQueryConfiguration queryConfiguration)
        {
            if (queryConfiguration != null)
            {
                IQueryConfiguration myQuery = (IQueryConfiguration)queryConfiguration.Clone();
                IList<IPublication> publications = new List<IPublication>();
                IDictionary<string, string> fields = new Dictionary<string, string>();

                DateTime initDate = myQuery.SinceDate;
                TimeSpan rangeDates = myQuery.UntilDate.Subtract(queryConfiguration.SinceDate);
                int totalDays = (int) (rangeDates.TotalDays == 0 ? 1 : (rangeDates.TotalDays)+1);
                if (totalDays <= 0)
                {
                    throw new ArgumentOutOfRangeException("since date can't be longer than until date");
                }
                
               
                IList<DateTime> datesRange = Enumerable.Range(0, totalDays)
                    .Select(x => initDate.AddDays(x)).ToList();

                int totalPublications = myQuery.MaxPublicationCount;
                

                if (totalPublications > 100)
                {
                    myQuery.MaxPublicationCount = 100;
                }



                int roundSearchesByPage = totalPublications / datesRange.Count;
                roundSearchesByPage = roundSearchesByPage < 100 ? roundSearchesByPage :100 ;
                myQuery.MaxPublicationCount = roundSearchesByPage+15;
                SetQueryFields(myQuery, fields);

                foreach (string page in pages)
                {
                    for (int i = 0; i < datesRange.Count; i++)
                    {
                        myQuery.SinceDate = datesRange[i].AddDays(-1);
                        myQuery.UntilDate = datesRange[i];
                        SetDatesRangeInFields(fields, myQuery);
                       
                        IList<IPublication> partialPublication = RequestFeedToGraph(page, fields, roundSearchesByPage, myQuery);
                        ((List<IPublication>)publications).AddRange(partialPublication);
                        if (publications.Count > (totalPublications * 2))
                        {
                            break;
                        }
                    }
                }
                publications = FilterPublications(publications, myQuery.Keywords);
                publications = ReorganizeSearches(publications, totalPublications);
                myQuery.MaxPublicationCount = totalPublications; // Restore default settings in queryConfiguration

                return publications;

            }
            return null;
        }

        private void SetDatesRangeInFields(IDictionary<string, string> args, IQueryConfiguration queryConfiguration)
        {
            DateTime until = queryConfiguration.UntilDate;
            DateTime since = queryConfiguration.SinceDate;
            string untilRequest = until.ToShortDateString().Replace("/", "-");
            string sinceRequest = since.ToShortDateString().Replace("/", "-");
            args["since"] = sinceRequest;
            args["until"]= untilRequest;
        }

        private IList<IPublication> FilterPublications(IList<IPublication> publications, IList<string> keywords)
        {
            IList<IPublication> pubs = new List<IPublication>();
            foreach (string word in keywords)
            {
                ((List<IPublication>)pubs).AddRange(FilterPublicationsByWord(publications, word));
            }

            return pubs;
        }

        private IList<IPublication> FilterPublicationsByWord(IList<IPublication> publications, string word)
        {
            Regex expresion = CreateRegularExpresion(word);

            return ((List<IPublication>)publications).FindAll(x => expresion.IsMatch(x.Message));
        }

        private Regex CreateRegularExpresion(string word)
        {
            string upperCasePattern = @"[\s|\W|]" + word.ToUpper() + @"+\w?\b";
            string lowerCasePattern = @"[\s|\W|]" + word.ToLower() + @"+\w?\b";
            string normalCasePattern = @"[\s|\W|]" + word + @"+\w?\b";
            return new Regex(normalCasePattern + "|" + lowerCasePattern + "|" + upperCasePattern);
        }

        private IList<IPublication> ReorganizeSearches(IList<IPublication> publications, int maxPublicationCount)
        {
            IDictionary<string, string> selectedPublications = new Dictionary<string, string>();
            IList<IPublication> responsePublications = new List<IPublication>();
            if (publications.Count > maxPublicationCount)
            {
                Random random = new Random();

                for (int i = 0; i < maxPublicationCount; i++)
                {
                    int toAdd = random.Next(0, publications.Count);
                    IPublication publication = publications[toAdd];

                    if (!selectedPublications.ContainsKey(publication.Id))
                    {
                        responsePublications.Add(publication);
                        publications.RemoveAt(toAdd);

                    }
                    else
                    {
                        publications.RemoveAt(toAdd);
                        i--;
                    }




                }
            }
            else
            {
                responsePublications = publications;
            }

            return responsePublications;
        }

        private bool Classify(IPublication publication, IQueryConfiguration queryConfiguration)
        {
            bool clasified = false;
            if (publication != null)
            {
                clasified = ClassifyByDate(publication, queryConfiguration.SinceDate, queryConfiguration.UntilDate);
            }
            return clasified;
        }

        private bool ClassifyByDate(IPublication publication, DateTime sinceDate, DateTime untilDate)
        {
            bool clasified = true;
            if (publication != null)
            {
                if (publication.CreateDate.CompareTo(QueryConfiguration.NONE_DATE) != 0)
                {
                    if (!(publication.CreateDate.CompareTo(sinceDate) >= 0 && publication.CreateDate.CompareTo(untilDate) <= 0))
                    {
                        clasified = false;
                    }
                }
            }

            return clasified;

        }

        internal IList<IPublication> InternalSearch(IList<IQueryConfiguration> queriesConfigurations)
        {

            List<IPublication> publications = new List<IPublication>();
            foreach (var query in queriesConfigurations)
            {
                IList<IPublication> found = InternalSearch(query);
                if (found != null && found.Count > 0)
                {
                    publications.AddRange(found);
                }
            }

            return publications;

        }

        public override IList<IPublication> SearchPublications(IQueryConfiguration queryConfiguration)
        {
            return InternalSearch(queryConfiguration);
        }

        private void SetQueryFields(IQueryConfiguration queryConfiguration, IDictionary<string, string> args)
        {
            string max = (queryConfiguration.MaxPublicationCount > 0 ? queryConfiguration.MaxPublicationCount : 500) + "";
            args.Add("limit", max);
            string fieldsRequest = GetFieldsRequest(queryConfiguration);
            args.Add("fields", fieldsRequest);

           
        }

        private string GetFieldsRequest(IQueryConfiguration queryConfiguration)
        {
            string fields = "description,user_location," +
                    "locale,likes.limit(0).summary(true),type,from," +
                    "created_time,message,id,place";
            if (queryConfiguration.MaxResponsesCount > 0)
            {
                fields += ",comments.limit(" + queryConfiguration.MaxResponsesCount + ").summary(true){type,place,message,created_time,from,id,likes.limit(0).summary(true)}";

            }
            return fields;
        }

        internal IList<IPublication> RequestFeedToGraph(string user, IDictionary<string, string> args, int totalPublications, IQueryConfiguration queryConfiguration)
        {
            var request = CreateFeedRequestToGraph(user, "feed", args);
            IList<IPublication> publications = null;
            var task = MakeRequestToGraphAsync(request);
            int totalPagings = (totalPublications / 5); // I Expected found at most 30 publications for page
            if (task != null)
            {
                publications = new List<IPublication>();
                var jsonResponse = task.Result;

                if (jsonResponse != null)
                {
                    //var before = GetBeforePublicationsRequest(jsonResponse);
                    //var next = GetNextPublicationsRequest(jsonResponse);

                    List<dynamic> responsesPages = new List<dynamic>
                    {
                        jsonResponse,
                        //before,
                        //next,

                     };
                    //totalPagings -= 3; //remove paging of jsonResponse, next and before

                    //int pagings = (totalPagings / 2);
                    //int pagings = 1;
                    //SetPublicationsRequests(responsesPages, pagings, next, before);

                    bool makeRequest = true;
                    while (makeRequest)
                    {
                       var response =  responsesPages.Last();
                        bool isEmpty = IsEmptyData(response.data);
                       if( !isEmpty )
                        {
                            AddPublications(response,publications,queryConfiguration);
                            if (publications.Count > totalPublications * 2)
                            {
                                makeRequest = false;
                            }
                            else
                            {
                                var next = GetNextPublicationsRequest(response);
                                if (responsesPages.Count < 2 && next!=null)
                                {
                                    
                                    responsesPages.Add(next);

                                }
                                else
                                {
                                    makeRequest = false;
                                }
                            }
                        }
                        else
                        {
                            makeRequest = false;
                        }
                    }
                  

                }

            }
            return publications;

        }

        private bool IsEmptyData(dynamic data)
        {
            int count = 0;
            foreach(var item in data)
            {
                
                count++;
                if (count > 0)
                {
                    break;
                }
            }
            return count == 0;
        }

        private void SetPublicationsRequests(List<dynamic> responsesPages, int pagings, dynamic next, dynamic before)
        {
            int beforePagings = 0;
            int nextPagings = 0;
            before = GetBeforePublicationsRequest(before);
            next = GetNextPublicationsRequest(next);

            while (beforePagings < pagings && before != null)
            {
                responsesPages.Add(before);
                before = GetBeforePublicationsRequest(before);
                ++beforePagings;
            }
            int restPagings = pagings - beforePagings;
            if (restPagings > 0)
            {
                pagings = pagings + restPagings;
            }

            while (nextPagings < pagings && next != null)
            {
                responsesPages.Add(next);
                next = GetNextPublicationsRequest(next);
                ++nextPagings;
            }



        }

        private dynamic GetNextPublicationsRequest(dynamic jsonResponse)
        {
            dynamic response = default(dynamic);
            if (jsonResponse != null)
            {
                if (jsonResponse.paging != null)
                {
                    if (jsonResponse.paging.next != null)
                    {
                        string next = (string)jsonResponse.paging.next;
                        next = DecodeHtmlText(next);
                        response = MakeRequestToGraphAsync(next).Result;
                    }
                }
            }


            return response;
        }

        private dynamic GetBeforePublicationsRequest(dynamic jsonResponse)
        {
            dynamic response = default(dynamic);
            if (jsonResponse != null)
            {
                if (jsonResponse.paging != null)
                {
                    if (jsonResponse.paging.next != null)
                    {
                        string previous = (string)jsonResponse.paging.previous;
                        previous = DecodeHtmlText(previous);
                        response = MakeRequestToGraphAsync(previous).Result;
                    }
                }
            }

            return response;
        }

        private void AddPublications(dynamic jsonResponse, IList<IPublication> publications, IQueryConfiguration queryConfiguration)
        {

            lock (this)
            {
                if (jsonResponse.data == null)
                {
                    throw new ArgumentException("No json data");
                }
                foreach (var item in jsonResponse.data)
                {
                    if (item != null)
                    {
                        IPublication publication = ParsePublicationOfJsonResponse(item, queryConfiguration);

                        IList<IPublication> responses = GetResponesPublications(item, queryConfiguration);


                        if (publication!=null && Classify(publication, queryConfiguration))
                        {
                            publications.Add(publication);
                        }

                        if (responses != null)
                        {
                            for (int i = 0; i < responses.Count; i++)
                            {

                                var response = responses.ElementAt(i);
                                if (Classify(response, queryConfiguration))
                                {
                                    response.Id = "Response " + i + " with id: " + response.Id + " of: " + publication.Id;
                                    publications.Add(response);
                                }
                            }

                        }



                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private IPublication ParsePublicationOfJsonResponse(dynamic item, IQueryConfiguration queryConfiguration)
        {
            bool parse = true;
            if (queryConfiguration.SearchType == SearchTypes.Popular)
            {
                if (!(item.likes != null && item.likes.summary != null
                     && item.likes.summary.total_count > 100))
                {
                    parse = false;
                }
            }
            string filter = GetFilterForFacebook(queryConfiguration);

            if (queryConfiguration.Filter != Filters.None)
            {
                if (!(item.type != null && ((string)item.type).Equals(filter, StringComparison.OrdinalIgnoreCase)))
                {
                    parse = false;
                }
            }

            if (parse)
            {
                string id = item.id ?? "Not found";
                DateTime createDate = item.created_time ?? QueryConfiguration.NONE_DATE;
                string message = item.message ?? "Not message";
                string decodeMessage = DecodeHtmlText(message);

                if (decodeMessage != null)
                {
                    message = decodeMessage;
                }
                string wroteBy = "Not found";
                if (item.from != null)
                {
                    wroteBy = (item.from.name + item.from.id);
                }
                IPublication publication = new Publication()
                {
                    Id = "Facebook:" + id,
                    CreateDate = createDate,
                    Message = message,
                    WroteBy = wroteBy,
                    ConfigurationName = queryConfiguration.Name
                };

                return publication;
            }
            return null;
        }

        private string GetFilterForFacebook(IQueryConfiguration queryConfiguration)
        {
            string filter = null;
            switch (queryConfiguration.Filter)
            {

                case Filters.Video:
                    {
                        filter = "video";
                        break;
                    }
                case Filters.News:
                    {
                        filter = "link";
                        break;
                    }

                case Filters.Image:
                    {
                        filter = "photo";
                        break;
                    }
                case Filters.Hashtag:
                    {
                        filter = "link";
                        break;
                    }
                default:
                    {
                        filter = "";
                        break;
                    }
            }

            return filter;
        }

        private string DecodeHtmlText(string text)
        {
            return WebUtility.HtmlDecode(text);
        }

        private IList<IPublication> GetResponesPublications(dynamic item, IQueryConfiguration queryConfiguration)
        {
            List<IPublication> responses = null;
            if (item.comments != null)
            {

                int totalComments = queryConfiguration.MaxResponsesCount;
                if (totalComments > 0)
                {
                    responses = new List<IPublication>();
                    AddPublications(item.comments, responses, queryConfiguration);
                }
            }

            return responses;
        }

        private async Task<dynamic> MakeRequestToGraphAsync(string request)
        {

            dynamic response = await client.GetAsync(request);

            if (response.ReasonPhrase.Equals("Not Found"))
            {
                return default(dynamic);
            }
            if (!response.IsSuccessStatusCode)
            {
                return default(dynamic);
            }

            var result = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject(result);
        }

        public string MakeTokenRequestToGraphAsync(string request)
        {
            try
            {
                var jsonResponse = MakeRequestToGraphAsync(request).Result;

                return (string)jsonResponse.access_token;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string CreateFeedRequestToGraph(string user, string consult, IDictionary<string, string> fields)
        {
            string reqConsult = $"{consult}?access_token={Credential}";
            reqConsult += GetConsultWithFields(fields);


            string request = user + "/" + reqConsult;

            return request;
        }

        private string GetConsultWithFields(IDictionary<string, string> fields)
        {
            string reqConsult = "";

            if (fields != null)
            {
                ICollection<string> keys = fields.Keys;

                foreach (var key in keys)
                {
                    if (fields.TryGetValue(key, out string value))
                    {
                        reqConsult += "&" + key + "=" + value;
                    }
                }
            }
            return reqConsult;
        }
    }
}