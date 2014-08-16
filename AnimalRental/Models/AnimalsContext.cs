using System.Data.Entity;

namespace AnimalRental.Models
{
	public class AnimalsContext : DbContext
	{
		public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AnimalSpecies> AnimalSpecies { get; set; }

		public AnimalsContext() : base("AnimalsConnectionString")
		{
			
		}
	}
}