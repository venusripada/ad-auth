// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
using System.Collections.Generic;


namespace ad_auth.Models
{ 
    public class Value
    {
        public string credential { get; set; }
    }

    public class Command
    {
        public string type { get; set; }
        public Value value { get; set; }
    }

    public class RootResponse
    {
        public List<Command> commands { get; set; }
    }
}