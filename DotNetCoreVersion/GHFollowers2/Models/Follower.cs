using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GHF.Models
{
    public class Follower
    {
        public string login { get; set; }
        public string id { get; set; }
        public List<Follower> followers { get; set; }
    }
}