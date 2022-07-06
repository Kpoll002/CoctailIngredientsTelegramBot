using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CoctailsIngredient.Models
{

    public class ListOfPopularCoctails
    {
        [JsonProperty("drinks")]
        public List<Drink1> drinks{ get; set; }
        
    }
    public class Drink1
    {
        public string strDrink { get; set; }
        public string strCategory { get; set; }
        public string strAlcoholic { get; set; }
        public string strInstructions { get; set; }
        public string strImageSource { get; set; }
    }
}
