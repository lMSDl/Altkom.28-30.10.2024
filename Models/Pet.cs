using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Models
{
    public class Pet : Entity
    {
        public string Name { get; set; }
        public float Age { get; set; }

        public string Description => $"{Name} ({Age})";

        [JsonIgnore]
        [XmlIgnore]
        public string Sth { get; set; } = "ala ma kota";
    }
}
