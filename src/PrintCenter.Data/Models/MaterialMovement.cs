using System;

namespace PrintCenter.Data.Models
{
     public class MaterialMovement :IHasId
    {
        public int Id { get; set; }

        public Material Material { get; set; }

        public DateTime DateTime { get; set; }

        public User User { get; set; }

        public MovementType Type { get; set; }

        public float Count { get; set; }

        public Invoice Invoice { get; set; }
    }

    public enum MovementType
    {
        Coming,
        Consumption
    }
}
