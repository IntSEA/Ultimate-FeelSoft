using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkConnection;

namespace Controller
{
    public class QueryInformation
    {
        private TypeSocialNetwork typeSocialNetwork;
        private TypeAnalysisTechnique typeAnalysis;
        private DateTime firstDate;
        private DateTime lastDate;

        public QueryInformation(TypeSocialNetwork typeSocialNetwork, TypeAnalysisTechnique typeAnalysis, DateTime firstDate, DateTime lastDate)
        {
            this.typeSocialNetwork = typeSocialNetwork;
            this.TypeAnalysis = typeAnalysis;
            this.firstDate = firstDate;
            this.lastDate = lastDate;
        }

        public TypeSocialNetwork TypeSocialNetwork { get => typeSocialNetwork; set => typeSocialNetwork = value; }
        public TypeAnalysisTechnique TypeAnalysis { get => typeAnalysis; set => typeAnalysis = value; }

        public Boolean SatisfiesQuery(IPublication publication)
        {
            if (typeSocialNetwork.Equals(TypeSocialNetwork.Both))
            {
                return BetweenDates(publication);
            }
            else
            {
                TypeSocialNetwork social;

                if (publication.Id.Contains("Facebook"))
                {
                    social = TypeSocialNetwork.Facebook;
                }
                else
                {
                    social = TypeSocialNetwork.Twitter;
                }

                return (BetweenDates(publication) && typeSocialNetwork.Equals(social));
            }
        }

        private Boolean BetweenDates(IPublication publication)
        {
            bool betweenDates = false;

            if (publication.CreateDate.CompareTo(firstDate) > 0 && publication.CreateDate.CompareTo(lastDate) < 0)
            {
                betweenDates = true;
            }
            return betweenDates;
        }

    }
}
