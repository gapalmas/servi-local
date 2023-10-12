namespace App.Core.Entities
{
    public class Provider : Document
    {
        // Provider Information
        public string? CompanyName { get; set; }
        //public string? Logo { get; set; }
        //public string? Address { get; set; }
        //public string? ServiceArea { get; set; }
        //public string? Email { get; set; }
        //public string? PhoneNumber { get; set; }
        //public string? Website { get; set; }
        //public string? Description { get; set; }

        //// Services Offered
        //public List<Service>? Services { get; set; }

        //// Experience and Qualifications
        //public int YearsOfExperience { get; set; }
        //public List<string>? Certifications { get; set; }
        //public List<PreviousProject>? PreviousProjects { get; set; }
        //public List<KeyPersonnel>? KeyPersonnel { get; set; }

        //// Reviews and Ratings
        //public List<Review>? Reviews { get; set; }

        //// Work Gallery
        //public List<ProjectImage>? Gallery { get; set; }

        //// Frequently Asked Questions
        //public List<FrequentlyAskedQuestion>? FAQs { get; set; }

        //// Additional Information
        //public string? CancellationPolicy { get; set; }
        //public string? WarrantyPolicy { get; set; }
        //public string? BusinessHours { get; set; }
        //public List<string>? AcceptedPaymentMethods { get; set; }

        //// Contact and Quote Request
        //public string? ContactForm { get; set; }
        //public string? LiveChat { get; set; }

        //// Statistics and Metrics (optional)
        //public int CompletedProjects { get; set; }
        //public double SatisfactionPercentage { get; set; }
        //public TimeSpan AverageResponseTime { get; set; }

        //// Location Map (optional)
        //public string? LocationMap { get; set; }

        //// Social Media Links (optional)
        //public string? SocialMediaLinks { get; set; }

        //// Booking Integration (if applicable)
        //public bool AllowsBooking { get; set; }
    }

    public class Service
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }

    public class PreviousProject
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }

    public class KeyPersonnel
    {
        public string? Name { get; set; }
        public string? Background { get; set; }
    }

    public class Review
    {
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public string? CustomerName { get; set; }
    }

    public class ProjectImage
    {
        public string? Url { get; set; }
        public string? Description { get; set; }
    }

    public class FrequentlyAskedQuestion
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
    }
}
