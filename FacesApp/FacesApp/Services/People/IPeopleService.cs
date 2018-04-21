using FacesApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacesApp.Services.People
{
    public interface IPeopleService
    {
        Task<List<Person>> GetFacesAsync();
    }
}