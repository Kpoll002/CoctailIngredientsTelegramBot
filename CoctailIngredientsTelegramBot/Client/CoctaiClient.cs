using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CoctailsIngredient.Constant;
using CoctailsIngredient.Models;


namespace CoctailsIngredient.Client
{
    public class CoctailClient

    {
        private HttpClient _Client;
        private static string _address;
        public CoctailClient()
        {


            _address = Constat.address;
            _Client = new HttpClient();
            _Client.BaseAddress = new Uri(_address);
        }

        public async Task<SearchByIngredient> GetSearchByIngredientAsync(string Ingredient)
        {

            var responce = await _Client.GetAsync($"SearchByIngredients?ingredient={Ingredient}");
            var content = responce.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
            var result = JsonConvert.DeserializeObject<SearchByIngredient>(content);
            Console.WriteLine(result);
            return result;


        }
        public async Task<SearchByName> GetSearchByNameAsync(string NameOfCoctail)
        {

            var response = await _Client.GetAsync($"SearchByName?NameOfCoctail={NameOfCoctail}");
            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
            var result = JsonConvert.DeserializeObject<SearchByName>(content);
            return result;

        }
        public async Task<RandomCoctail> GetRandomCoctailsAsync()
        {

            var response = await _Client.GetAsync($"RandomCoctail");
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RandomCoctail>(body);
            return result;

        }
        public async Task<ListOfPopularCoctails> GetListOfPopularCoctailsAsync()
        {

            var response = await _Client.GetAsync($"ListOfPopularCoctails");
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListOfPopularCoctails>(body);
            return result;


        }
        public async Task<ListOfIngredients> GetListOfIngredientsAsync()
        {

            var response = await _Client.GetAsync($"ListOfIngredients");
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListOfIngredients>(body);
            Console.WriteLine(result);
            return result;

        }
    }
}

