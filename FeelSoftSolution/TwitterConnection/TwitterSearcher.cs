﻿using HtmlAgilityPack;
using SocialNetworkConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace TwitterConnection
{
    public class TwitterSearcher : PublicationSearcher
    {
        public static WebClient webClient = new WebClient();
        public static HtmlDocument htmlDocument = new HtmlDocument();

        public TwitterSearcher(string credential) : base(credential)
        {

        }

        public override IList<IPublication> SearchPublications(IList<IQueryConfiguration> queriesConfigurations)
        {
            List<IPublication> publications = new List<IPublication>();
            foreach (var item in queriesConfigurations)
            {
                publications.AddRange(SearchPublications(item));
            }
            return publications;
        }

        private IList<IPublication> ReorganizeSearches(IList<IPublication> publications, int maxPublicationCount)
        {
            IList<IPublication> responsePublications = new List<IPublication>();
            if (publications.Count > maxPublicationCount)
            {
                Random random = new Random();

                for (int i = 0; i < maxPublicationCount; i++)
                {
                    int toAdd = random.Next(0, publications.Count);
                    responsePublications.Add(publications[toAdd]);
                    publications.RemoveAt(toAdd);
                }
            }
            else
            {
                responsePublications = publications;
            }

            return responsePublications;
        }

        private void ParseTweets(IList<ITweet> tweets, IList<IPublication> publications, IQueryConfiguration queryConfiguration)
        {
            if (tweets != null)
            {
                ITweet[] arrayTweets = tweets.ToArray();
                for (int i = 0; i < arrayTweets.Length; i++)
                {
                    IPublication parsedPublication = ParseTweetToPublication(arrayTweets[i], queryConfiguration);
                    if (arrayTweets[i] == null)
                    {
                        throw new ArgumentException("No deberia suceder.");
                    }

                    if (parsedPublication != null)
                    {
                        publications.Add(parsedPublication);
                    }
                }
            }
        }
        
        public static IPublication ParseTweetToPublication(ITweet tweet, IQueryConfiguration queryConfiguration)
        {
            string message = "";
            message = tweet.FullText ?? (tweet.Text + tweet.Suffix);
            IPublication publication = null;
            long id = tweet.Id;

            if (String.IsNullOrEmpty(message))
            {
                if (String.IsNullOrEmpty(tweet.FullText))
                {

                    message = tweet.Prefix + tweet.Text + tweet.Suffix;
                }
                else
                {
                    message = tweet.FullText;
                }
            }

            publication = new Publication()
            {
                Id = "Twitter:" + id,
                Message = message,
                WroteBy = tweet.CreatedBy.Name,
                CreateDate = tweet.TweetLocalCreationDate,
                ConfigurationName = queryConfiguration.Name

            };

            return publication;
        }

        public override IList<IPublication> SearchPublications(IQueryConfiguration queryConfiguration)
        {
            IQueryConfiguration myQuery = (IQueryConfiguration)queryConfiguration.Clone();
             if (myQuery.SinceDate.CompareTo(myQuery.UntilDate) > 0)
            {
                throw new ArgumentException("until date must be longer than since date ");

            }
            IList<IPublication> publications = new List<IPublication>();
            int totalPublications = myQuery.MaxPublicationCount;
            TimeSpan rangeTime = myQuery.UntilDate.Subtract(myQuery.SinceDate);
            int totalDays = (int) (rangeTime.TotalDays == 0 ? 1 : rangeTime.TotalDays+1);            
            if (totalDays > 10 || myQuery.UntilDate.CompareTo(DateTime.Now.AddDays(-7))<0)
            {
                //throw new ArgumentException("You can't search longer than a week");
                totalDays = 7;
                myQuery.UntilDate = DateTime.Now;
                myQuery.SinceDate = myQuery.UntilDate.AddDays(-7);
            }
            List<DateTime> dates = Enumerable.Range(0,totalDays).Select(x=> myQuery.SinceDate.AddDays(x)).ToList();
            int totalSearchesByDate = queryConfiguration.MaxPublicationCount / totalDays;

            queryConfiguration.MaxPublicationCount = totalSearchesByDate+1000;
            foreach (var key in queryConfiguration.Keywords)
            {
                
                    ISearchTweetsParameters parameters = ParseSearchTweetsParameters(myQuery, key);
                    IList<ITweet> tweets = Search.SearchTweets(parameters).ToList();
                    ParseTweets(tweets, publications, queryConfiguration);
                    if (publications.Count > totalPublications)
                    {
                        break;
                    }
                


            }
            queryConfiguration.MaxPublicationCount = totalPublications;
            publications = ReorganizeSearches(publications, queryConfiguration.MaxPublicationCount);
            return publications;
        }

        private ISearchTweetsParameters ParseSearchTweetsParameters(IQueryConfiguration queryConfiguration, string key)
        {
            ISearchTweetsParameters parameters = new SearchTweetsParameters(key)
            {
                MaximumNumberOfResults = queryConfiguration.MaxPublicationCount
            };
            ParseFilter(parameters, queryConfiguration);
            ParseLanguage(parameters, queryConfiguration);
            ParseLocation(parameters, queryConfiguration);
            ParseSearchType(parameters, queryConfiguration);
            ParseSearchDates(parameters, queryConfiguration);
            //TODO ParseGeoCoordinates.

            return parameters;

        }

        private void ParseSearchDates(ISearchTweetsParameters parameters, IQueryConfiguration queryConfiguration)
        {
            if (queryConfiguration.SinceDate.CompareTo(QueryConfiguration.NONE_DATE) != 0)
            {
                DateTime since = new DateTime(queryConfiguration.SinceDate.Year,
                    queryConfiguration.SinceDate.Month, queryConfiguration.SinceDate.Day);
                parameters.Since = since;
            }
            if (queryConfiguration.UntilDate.CompareTo(QueryConfiguration.NONE_DATE) != 0)
            {
                DateTime until = new DateTime(queryConfiguration.UntilDate.Year,
                    queryConfiguration.UntilDate.Month, queryConfiguration.UntilDate.Day);
                parameters.Until = until;
            }

        }

        private void ParseSearchType(ISearchTweetsParameters parameters, IQueryConfiguration queryConfiguration)
        {
            if (queryConfiguration.SearchType == SearchTypes.Mixed)
            {
                parameters.SearchType = SearchResultType.Mixed;
            }
            else if (queryConfiguration.SearchType == SearchTypes.Popular)
            {
                parameters.SearchType = SearchResultType.Popular;
            }

            else if (queryConfiguration.SearchType == SearchTypes.Recent)
            {
                parameters.SearchType = SearchResultType.Recent;
            }
        }

        private void ParseLocation(ISearchTweetsParameters parameters, IQueryConfiguration queryConfiguration)
        {
            //if (queryConfiguration.Location == Locations.Colombia)
            //{
            //    parameters.Locale = "Colombia";
            //}
            //else if(queryConfiguration.Location == Locations.USA)
            //{
            //    parameters.Locale = "USA";
            //}
        }

        private void ParseLanguage(ISearchTweetsParameters parameters, IQueryConfiguration queryConfiguration)
        {
            if (queryConfiguration.Language ==Languages.English)
            {
                parameters.Lang = LanguageFilter.English;
            }

           

            else if (queryConfiguration.Language ==Languages.Spanish)
            {
                parameters.Lang = LanguageFilter.Spanish;
            }
        }

        private void ParseFilter(ISearchTweetsParameters parameters, IQueryConfiguration queryConfiguration)
        {
            switch (queryConfiguration.Filter)
            {
                case Filters.Hashtag:
                    {
                        parameters.Filters = TweetSearchFilters.Hashtags;
                        break;
                    }
                case Filters.Image:
                    {
                        parameters.Filters = TweetSearchFilters.Images;
                        break;
                    }
                case Filters.News:
                    {
                        parameters.Filters = TweetSearchFilters.News;
                        break;
                    }
                case Filters.None:
                    {
                        parameters.Filters = TweetSearchFilters.None;
                        break;
                    }
                case Filters.Video:
                    {
                        parameters.Filters = TweetSearchFilters.Videos;
                        break;
                    }
            }
        }
    }
}
