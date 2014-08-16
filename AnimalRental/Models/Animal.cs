using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimalRental.Models
{
    public enum Status { Available, Rented };

    public class Animal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public int AnimalSpeciesId { get; set; }
        public virtual AnimalSpecies Species { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public virtual ICollection<Comment> Comments { get; set;}
    }
}