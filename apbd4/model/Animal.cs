using System.ComponentModel.DataAnnotations;

namespace apbd4.model
{
    public class Animal
    {
        [Required]
        public  int IdAnimal { get; set; }
        [Required]
        public String name { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public String Category { get; set; }
        [Required]
        public String Area { get; set; }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
