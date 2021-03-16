// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
using System;
namespace ad_auth.Models
{
    public class Url
    {
        public string value { get; set; }
    }

    public class Request
    {
        public string id { get; set; }
        public string method { get; set; }
        public Url url { get; set; }
        public string ipAddress { get; set; }
    }

    public class Credential
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Context
    {
        public Request request { get; set; }
        public Credential credential { get; set; }
    }

    public class Action
    {
        public string credential { get; set; }
    }

    public class Data
    {
        public Context context { get; set; }
        public Action action { get; set; }
    }

    public class RootRequest
    {
        public string eventId { get; set; }
        public DateTime eventTime { get; set; }
        public string eventType { get; set; }
        public string eventTypeVersion { get; set; }
        public string contentType { get; set; }
        public string cloudEventVersion { get; set; }
        public string source { get; set; }
        public Data data { get; set; }
    }
}

