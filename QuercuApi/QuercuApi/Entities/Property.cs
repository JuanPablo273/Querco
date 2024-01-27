namespace QuercuApi.Entities
{
    public class PropertyEnt
    {
        public long Id{ get; set; }

        public string PropertyTypeId { get; set;} = string.Empty;

        public string OwnerId { get; set; } = string.Empty;

        public string Number { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Area { get; set; } = string.Empty;

        public string ContructionArea { get; set; } = string.Empty;

    }
}
