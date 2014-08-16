using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Umbraco.Web.WebApi;
using AnimalRental.Models;

namespace AnimalRental.Controllers
{
    /// <summary>
    /// Class for managing AnimalSpecies entity
    /// </summary>
    public class AnimalSpeciesApiController : UmbracoApiController
    {

        /// <summary>
        /// Gets all animal species (for drop-down list)
        /// </summary>
        [HttpGet]
        public IEnumerable<AnimalSpecies> Get()
        {
            var db = new AnimalsContext();
            var species = db.AnimalSpecies.ToList();
            return species;
        }

    }
}
