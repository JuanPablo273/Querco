namespace QuercuApi.Entities
{
    public class OwnerEnt
    {
        public long Id{ get; set; }

        public string Name { get; set; } = string.Empty;

        public string Telephone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string IdentificationNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public bool Status { get; set; }


    }
}
