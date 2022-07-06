using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;



namespace CoctailsIngredient.Models
{
    public class ListOfIngredients
    {
        [JsonProperty("drinks")]
        public List<Drink2> drinks { get; set; }
    }
    public class Drink2
    {
        public string strIngredient1 { get; set; }

    }

}