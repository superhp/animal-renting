using System.Linq;
using AnimalRental.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.WebApi;
using System.Net.Http;
using AnimalRental.Helper;
using System.Net;

namespace AnimalRental.Controllers
{
    /// <summary>
    /// Class for accessings animal enity
    /// </summary>
    public class AnimalsApiController : UmbracoApiController
    {
        /// <summary>
        /// Gets all animals
        /// </summary>
        [HttpGet]
        public IEnumerable<Animal> Get()
        {
            var db = new AnimalsContext();
            return db.Animals.ToList();
        }

        /// <summary>
        /// Gets animal by id
        /// </summary>
        [HttpGet]
        public HttpResponseMessage GetAnimal(int id)
        {
            var db = new AnimalsContext();
            var animal = db.Animals.SingleOrDefault(x => x.AnimalId == id);
            if (animal == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, animal);
        }

        /// <summary>
        /// Get last 5 added animals (for carousel)
        /// </summary>
        [HttpGet]
        public IEnumerable<Animal> GetLastFive()
        {
            var db = new AnimalsContext();
            var animals = db.Animals.OrderBy(x => x.PublishDate).Take(5).ToList();
            return animals;
        }

        /// <summary>
        /// Posts a new animal
        /// </summary>
        [HttpPost]
        [MemberAuthorize]
        public Animal Post(Animal model)
        {
            model.PublishDate = DateTime.Now;
            model.UserId = Members.GetCurrentMemberId();
            var db = new AnimalsContext();
            var newAnimal = db.Animals.Add(model);
            db.SaveChanges();
            return newAnimal;
        }
        
        /// <summary>
        /// Updates an existing animal
        /// </summary>
        [HttpPut]
        [MemberAuthorize]
        public HttpResponseMessage Put(Animal model)
        {
            var db = new AnimalsContext();
            var modelToUpdate = db.Animals.SingleOrDefault(x => x.AnimalId == model.AnimalId);
            if (modelToUpdate != null)
            {
                modelToUpdate.Status = model.Status!=modelToUpdate.Status ? model.Status : modelToUpdate.Status;
                modelToUpdate.Name = model.Name != null && model.Name != modelToUpdate.Name ? model.Name : modelToUpdate.Name;
                modelToUpdate.PhotoUrl = model.PhotoUrl != null && model.PhotoUrl != modelToUpdate.PhotoUrl ? model.PhotoUrl : modelToUpdate.PhotoUrl;
                modelToUpdate.Description = model.Description != null && model.Description != modelToUpdate.Description ? model.Description : modelToUpdate.Description;
                //modelToUpdate.Species = model.Species != modelToUpdate.Species ? model.Species : modelToUpdate.Species;
                modelToUpdate.AnimalSpeciesId = model.AnimalSpeciesId != 0 && model.AnimalSpeciesId != modelToUpdate.AnimalSpeciesId ? model.AnimalSpeciesId : modelToUpdate.AnimalSpeciesId;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, modelToUpdate);
            }
            return Request.CreateResponse(HttpStatusCode.Gone);
        }

        /// <summary>
        /// Updates an existing animal's status
        /// </summary>
        [HttpPut]
        public HttpResponseMessage PutStatus(Animal model)
        {
            var db = new AnimalsContext();
            var modelToUpdate = db.Animals.SingleOrDefault(x => x.AnimalId == model.AnimalId);
            if (modelToUpdate != null)
            {
                modelToUpdate.Status = model.Status != modelToUpdate.Status ? model.Status : modelToUpdate.Status;
                db.SaveChanges();

                // We promissed to inform by e-mail
                Email.SendEmail(modelToUpdate);

                return Request.CreateResponse(HttpStatusCode.OK, modelToUpdate);
            }
            return Request.CreateResponse(HttpStatusCode.Gone);
        }
    }
}
