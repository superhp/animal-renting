using System;
using System.ComponentModel.DataAnnotations;

namespace AnimalRental.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int AnimalId { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime PublishDate { get; set; }
    }
}