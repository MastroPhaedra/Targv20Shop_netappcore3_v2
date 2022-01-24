using System;
using System.ComponentModel.DataAnnotations;


namespace Targv20Shop.Core.Domain
{
    public class Spaceship
    {
        [Key]
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Mass { get; set; }
        public decimal Prize { get; set; }
        public int Crew { get; set; }
        public DateTime ConstructedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}