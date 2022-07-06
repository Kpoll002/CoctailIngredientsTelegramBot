using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CoctailsIngredient.Models
{
    



    public class RandomCoctail
    {
        [JsonProperty("drinks")]
        public List<Drink> drinks { get; set; }

    }

    public class Drink
    {
        public string strDrink { get; set; }
        public string strCategory { get; set; }
        public string strAlcoholic { get; set; }
        public string strInstructions { get; set; }
        public string strDrinkThumb { get; set; }


    }
}
