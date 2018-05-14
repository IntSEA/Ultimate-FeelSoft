using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetworkConnection;

namespace Controller
{
    public class Candidate
    {
        private string name;
        private IList<IPublication> publications;
        

        
        public string Name { get => name; set => name = value; }
        public IList<IPublication> Publications { get => publications; set => publications = value; }

        public Candidate(string name)
        {
            this.name = name;
            publications = new List<IPublication>();
        }
    }
}
