using FacesApp.Helpers;
using FacesApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FacesApp.Services.People
{
    public class PeopleService : IPeopleService
    {
        public async Task<List<Person>> GetFacesAsync()
        {
            HttpClient httpClient = new HttpClient();
            string response = await httpClient.GetStringAsync(Constants.PersonApiUrl);
            List<Person> persons = JsonConvert.DeserializeObject<List<Person>>(response);
            return persons;
        }
    }
}