using System.Collections.Generic;

namespace MetaWorldWeb.Models
{
    public class FormData
    {
        public string ActionUri { get; set; }
        public string ActionMethod { get; set; }

        public Dictionary<string, string> Inputs { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Hidden { get; set; } = new Dictionary<string, string>();
    }
}
