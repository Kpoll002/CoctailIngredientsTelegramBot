
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CoctailsIngredient.Models
{

    public class SearchByName
    {
        [JsonProperty("ingredients")]
        public List<ingredients> ingredients { get; set; }
    }

    public class ingredients
    {
        public string strIngredient { get; set; }
        public string strDescription { get; set; }
        public string strAlcohol { get; set; }
        public string strABV { get; set; }
        public string strType { get; set; }
    }
}
