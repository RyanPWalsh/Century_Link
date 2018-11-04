using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GHF.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace GHFollowers2.Controllers
{
    //https://ghfollowers.azurewebsites.net/api/followers/jessephelps
    [Route("api/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {

        // GET api/values/5
        [HttpGet("{gitHubId}")]
        public ActionResult<string> Get(string gitHubId)
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
