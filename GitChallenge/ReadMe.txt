This API takes a username as input and accesses the GitHub user API, finding up to 5 followers
of that user. The process continues for each follower, up to levels deep, and returns a list of
unique GitHub accounts in Json (Users will only be listed once even if they follow multiple users).

Update the appsettings.json Authentication section with GitHub credentials to allow more than the
GitHub 60 requests per hour limit for their API's

Can be accessed here
http://gitchallenge.azurewebsites.net/api/v1/GitHubFollowerController/<USERNAME>