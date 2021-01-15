using System;
using System.Collections.Generic;

namespace PSE.WebApp.MVC.Models
{
    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserLoginTokenResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }
}
