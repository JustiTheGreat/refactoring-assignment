namespace Assignment13
{
    public class SpeakerService(IRepository repository)
    {
        private readonly IRepository _repository = repository;
        private readonly int _requiredExperience = 10;
        private readonly int _requiredNumberOfCertificates = 3;
        private readonly int _browserVersionThreshold = 9;

        /// <summary>
        /// Register a speaker
        /// </summary>
        /// <returns>speakerID</returns>
        public int? Register(Speaker speaker)
        {
            //these lists should be obtained from the services corresponding to their entities
            var technologies = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };
            var domains = new List<string>() { "aol.com", "hotmail.com", "prodigy.com", "CompuServe.com" };
            var employers = new List<Employer>() { new("Microsoft"), new("Google"), new("Fog Creek Software"), new("37Signals") };
            //

            if (string.IsNullOrWhiteSpace(speaker.FirstName))
                throw new ArgumentNullException("First Name is required");

            if (string.IsNullOrWhiteSpace(speaker.LastName))
                throw new ArgumentNullException("Last name is required.");

            if (string.IsNullOrWhiteSpace(speaker.Email))
                throw new ArgumentNullException("Email is required.");

            CheckIfSpeakerMeetsRequirements(speaker, domains, employers);

            ApproveSessions(speaker.Sessions, technologies);

            speaker.RegistrationFee = ComputeRegistrationFee(speaker.Experience);

            int? speakerId = null;
            try
            {
                speakerId = _repository.SaveSpeaker(speaker);
            }
            catch (Exception)
            {
                //in case the db call fails
            }

            return speakerId;
        }

        private void CheckIfSpeakerMeetsRequirements(Speaker speaker, List<string> domains, List<Employer> employers)
        {
            bool good = speaker.Experience > _requiredExperience
                || speaker.HasBlog
                || speaker.Certifications.Count > _requiredNumberOfCertificates
                || employers.Contains(speaker.Employer);

            if (!good)
            {
                string emailDomain = speaker.Email.Split('@').Last();
                good = !domains.Contains(emailDomain)
                    && (speaker.Browser.Name != WebBrowser.BrowserName.InternetExplorer
                        || speaker.Browser.MajorVersion >= _browserVersionThreshold);
            }

            if (!good)
                throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.");
        }

        private void ApproveSessions(List<Session> speakerSessions, List<string> technologies)
        {
            if (speakerSessions.Count == 0)
                throw new ArgumentException("Can't register speaker with no sessions to present.");

            bool aSessionWasApproved = false;
            foreach (Session session in speakerSessions)
            {
                foreach (string technology in technologies)
                {
                    session.Approved = !session.Title.Contains(technology)
                                    && !session.Description.Contains(technology);

                    if (!session.Approved)
                        break;

                    aSessionWasApproved = true;
                }
            }

            if (!aSessionWasApproved)
                throw new NoSessionsApprovedException("No sessions approved.");
        }

        private static int ComputeRegistrationFee(int? experience)
            => experience <= 1 ? 500
                : experience >= 2 && experience <= 3 ? 250
                : experience >= 4 && experience <= 5 ? 100
                : experience >= 6 && experience <= 9 ? 50
                : 0;
    }
}
