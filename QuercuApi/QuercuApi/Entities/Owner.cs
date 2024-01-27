namespace QuercuApi.Entities
{
    public class OwnerEnt
    {
        public long Id{ get; set; }

        public string Description { get; set;} = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Telephone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string IdentificationNumer { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

    }
}
