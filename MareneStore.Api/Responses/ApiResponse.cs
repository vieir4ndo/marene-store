﻿using System.Collections.Generic;

namespace MareneStore.Api.Responses
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}