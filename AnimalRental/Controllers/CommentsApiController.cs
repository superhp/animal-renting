using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.WebApi;
using AnimalRental.Models;

namespace AnimalRental.Controllers
{
    /// <summary>
    /// Class for managing 'comment' entity
    /// </summary>
    public class CommentsApiController : UmbracoApiController
    {

        /// <summary>
        /// Gets a comment by id
        /// </summary>
        public IEnumerable<Comment> GetCommentsByAnimal(int id)
        {
            var db = new AnimalsContext();
            var comments = db.Comments.Where(x => x.AnimalId == id).ToList();
            return comments;
        }

        /// <summary>
        /// Sets a new comment
        /// </summary>
        [HttpPost]
        public void AddComment(Comment comment)
        {
            comment.PublishDate = DateTime.Now;
            var db = new AnimalsContext();
            db.Comments.Add(comment);
            db.SaveChanges();
        }

    }
}
