namespace aws_stuff.models {
    public class PersonDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public AddressDTO Address {get; set; }
    }
}