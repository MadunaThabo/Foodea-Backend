using System.Net.Http.Headers;
using Foodea.Models;
using Microsoft.Extensions.Configuration;

namespace Foodea.Services {

    public interface ISpoonacularServices {
        public Task<string> GetRandomRecipes(int number);
        public Task<string> GetRecipeById(int number);
        public Task<string> GetSimilarRecipesToRecipe(int recipeId, int numberOfSimilarRecipes);

        public Task<string> searchRecipe(string query);
    }
    public class SpoonacularService : ISpoonacularServices {

        private readonly HttpClient httpClient = new HttpClient();
        private string apiKey;
        public SpoonacularService(IConfiguration config) {
            //var random = config.GetValue<string>("APIKeys:spoonacularApiKey");
            this.apiKey = config.GetValue<string>("APIKeys:spoonacularApiKey");
            this.httpClient.DefaultRequestHeaders.Add("apiKey", config.GetValue<string>("APIKeys:spoonacularApiKey"));
        }

        public async Task<string> GetRandomRecipes(int number) {
            //var apiKey = "apiKey=38262c2033cb423b9b84d05b835d1ee8&";
            var url = "https://api.spoonacular.com/recipes/random?" + "number=" + number.ToString() + "&apiKey=" + this.apiKey;
            var response = await this.httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                return content;
            }
            else {
                throw new KeyNotFoundException("will change to correct error");
            }
        }

        public async Task<string> GetRecipeById(int recipeId) {
            var url = "https://api.spoonacular.com/recipes/" + recipeId + "/information?includeNutrition=false&apiKey=" + apiKey;
            var response = await this.httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                return content;
            }
            else {
                return "An error occurred.";
            }
        }

        public async Task<string> GetSimilarRecipesToRecipe(int recipeId, int numberOfSimilarRecipes) {
            var url = "https://api.spoonacular.com/recipes/" + recipeId + "/similar&apiKey=" + apiKey;
            var response = await this.httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                return content;
            }
            else {
                return "An error occurred.";
            }
        }

        public async Task<string> searchRecipe(string query) {
            var url = "https://api.spoonacular.com/recipes/complexSearch?query=" + query + "&number=4"+ "&apiKey=" + apiKey;
            var response = await this.httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK) {
                return content;
            }
            else {
                return "An error occurred.";
            }
        }
    }
}
