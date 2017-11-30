using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace GitChallenge
{
    public interface IGitClient
    {
        List<GitHubUser> GetFollowerLevels(string user, int levels);
        List<GitHubUser> GetUserFollowers(string user);
    }

    public class GitClient : IGitClient
    {
        public static HttpClient Client = ClientFactory.GetGitClient();
        private int followerCount = 5;
        private List<string> UsersAlreadyFound = new List<string>();

        public GitClient(IConfiguration config)
        {
            var auth = config.GetSection("Authentication");
            var credentials = Encoding.ASCII.GetBytes($"{auth.GetSection("Name").Value}:{auth.GetSection("Pass").Value}");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
        }

        public List<GitHubUser> GetFollowerLevels(string user, int levels)
        {
            var accumulatedFollowers = new List<GitHubUser>();

            if (levels == 0)
            {
                return accumulatedFollowers;
            }

            var followers = GetUserFollowers(user);

            if(!followers.Any())
            {
                return followers;
            }

            accumulatedFollowers.AddRange(followers);

            foreach (var follower in followers)
            {
                accumulatedFollowers.AddRange(GetFollowerLevels(follower.login, levels - 1));
            }

            return accumulatedFollowers;
        }

        public List<GitHubUser> GetUserFollowers(string user)
        {
            try
            {
                var response = Client.GetAsync($"users/{user}/followers").Result;

                var followers = response.Content.ReadAsAsync<List<GitHubUser>>().Result;

                var uniqueFollowers = followers.Select(x => x).Where(x => !UsersAlreadyFound.Contains(x.login)).ToList();
                UsersAlreadyFound.AddRange(followers.Select(x => x.login).Where(x => !UsersAlreadyFound.Contains(x)).ToList());

                return uniqueFollowers.Take(followerCount).ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Error contacting git for followers of user {user}. ", e);
            }
        }
    }
}
