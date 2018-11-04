using System;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using GHF.Models;
using System.Collections.Generic;
using System.Linq;

namespace GHF.Controllers
{
    public class FollowersController : ApiController
    {
        // GET: api/Followers/username
        public string Get(string gitHubId)
        {
            var followers = getFiveFollowers(gitHubId);
            foreach (Follower f1 in followers)
            {
                f1.followers = getFiveFollowers(f1.login);
                foreach (Follower f2 in f1.followers)
                {
                    f2.followers = getFiveFollowers(f2.login);
                }
            }
            return JsonConvert.SerializeObject(followers);
        }
        private List<Follower> getFiveFollowers(string gitHubId)
        {
            string url = $"https://api.github.com/users/{gitHubId}/followers";
            var client = new HttpClient();           
            client.DefaultRequestHeaders.Add("user-agent", "user_agent_is_required_by_gitHub");
            try
            {
                var content = client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<List<Follower>>(content).Take(5).ToList();
            }
            catch
            {
                return new List<Follower>();
            }
        }
    }
}
