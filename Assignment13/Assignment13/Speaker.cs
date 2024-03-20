namespace Assignment13
{
    /// <summary>
    /// Represents a single speaker
    /// </summary>
    public class Speaker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Experience { get; set; }
        public bool HasBlog { get; set; }
        public string BlogURL { get; set; }
        public WebBrowser Browser { get; set; }
        public List<Certification> Certifications { get; set; }
        public Employer Employer { get; set; }
        public int RegistrationFee { get; set; }
        public List<Session> Sessions { get; set; }
    }
}