using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MareneStore.Api.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}