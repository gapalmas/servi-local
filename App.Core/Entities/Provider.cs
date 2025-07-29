namespace App.Core.Entities
{
    public class Provider : Document
    {
        // Provider Information
        public string CompanyName { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ServiceArea { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Services Offered
        public List<Service> Services { get; set; } = [];

        // Experience and Qualifications
        public int YearsOfExperience { get; set; }
        public List<string> Certifications { get; set; } = [];
        public List<PreviousProject> PreviousProjects { get; set; } = [];
        public List<KeyPersonnel> KeyPersonnel { get; set; } = [];

        // Reviews and Ratings
        public List<Review> Reviews { get; set; } = [];

        // Work Gallery
        public List<ProjectImage> Gallery { get; set; } = [];

        // Frequently Asked Questions
        public List<FrequentlyAskedQuestion> FAQs { get; set; } =   [];

        // Additional Information
        public string CancellationPolicy { get; set; } = string.Empty;
        public string? WarrantyPolicy { get; set; }
        public string BusinessHours { get; set; } = string.Empty;
        public List<string> AcceptedPaymentMethods { get; set; } = [];

        // Contact and Quote Request
        public string ContactForm { get; set; } = string.Empty;
        public string LiveChat { get; set; } = string.Empty;

        // Statistics and Metrics (optional)
        public int CompletedProjects { get; set; } = int.MinValue;
        public double SatisfactionPercentage { get; set; } = double.MinValue;
        public TimeSpan AverageResponseTime { get; set; } = TimeSpan.MinValue;

        // Location Map (optional)
        public string LocationMap { get; set; } = string.Empty;

        // Social Media Links (optional)
        public string SocialMediaLinks { get; set; } = string.Empty;

        // Booking Integration (if applicable)
        public bool AllowsBooking { get; set; } = true;
    }

    public class Service
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.MinValue;
    }

    public class PreviousProject
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }

    public class KeyPersonnel
    {
        public string Name { get; set; } = string.Empty;
        public string Background { get; set; } = string.Empty;
    }

    public class Review
    {
        public int Rating { get; set; } = int.MinValue;
        public string Comment { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.MinValue;
        public string CustomerName { get; set; } = string.Empty;
    }

    public class ProjectImage
    {
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class FrequentlyAskedQuestion
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}