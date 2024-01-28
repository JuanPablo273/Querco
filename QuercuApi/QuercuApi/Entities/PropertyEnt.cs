namespace QuercuApi.Entities
{
    public class PropertyEnt
    {
        public long Id{ get; set; }

        public string PropertyTypeId { get; set;} = string.Empty;

        public string OwnerId { get; set; } = string.Empty;

        public string Number { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public decimal Area { get; set; } 

        public decimal ConstructionArea { get; set; } 

        public bool Status { get; set; }
    }
}
