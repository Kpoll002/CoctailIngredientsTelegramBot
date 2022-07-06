using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CoctailsIngredient.Models
{


    public class SearchByIngredient
    {
        [JsonProperty("drinks")]
        public List<drinks1> drinks { get; set; }
    }
    public class drinks1
    {
        public string strDrink { get; set; }
        public string strDrinkThumb { get; set; }
    }




}

