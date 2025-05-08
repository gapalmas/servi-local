using System.Text.RegularExpressions;

namespace App.Core.Entities
{
    public class INE : Document
    {
        // Personal information
        public string FullName { get; set; } = string.Empty;           // Full name
        public string LastName { get; set; } = string.Empty;           // Last name (paternal)
        public string MothersMaidenName { get; set; } = string.Empty;  // Mother's maiden name (maternal)

        // Identification information
        public string CURP { get; set; } = string.Empty;               // Unique Population Registry Code
        public string VoterKey { get; set; } = string.Empty;           // Voter key
        public string CredentialNumber { get; set; } = string.Empty;   // Credential number

        // Address information
        public string Address { get; set; } = string.Empty;            // Full address
        public string Neighborhood { get; set; } = string.Empty;       // Neighborhood
        public string PostalCode { get; set; } = string.Empty;         // Postal code
        public string State { get; set; } = string.Empty;              // State
        public string Municipality { get; set; } = string.Empty;       // Municipality
        public string ElectoralSection { get; set; } = string.Empty;  // Electoral section

        // Validity information
        public DateTime BirthDate { get; set; }        // Date of birth
        public DateTime IssueDate { get; set; }        // Date of issue
        public DateTime ExpirationDate { get; set; }   // Expiration date

        // Additional information
        public string Folio { get; set; } = string.Empty;             // Folio number
        public string OCR { get; set; } = string.Empty;               // Optical Character Recognition code
        public string RegistrationYear { get; set; } = string.Empty;  // Registration year
        public string Issuance { get; set; } = string.Empty;          // Issuance (first, second, etc.)

        // Method to validate CURP
        public bool IsCURPValid()
        {
            // Regular expression to validate CURP format
            var curpRegex = new Regex(@"^[A-Z]{4}\d{6}[HM][A-Z]{5}[0-9A-Z]\d$");
            return curpRegex.IsMatch(CURP);
        }

        // Method to validate Voter Key
        public bool IsVoterKeyValid()
        {
            // Regular expression to validate Voter Key format
            var voterKeyRegex = new Regex(@"^[A-Z]{6}\d{8}[HM][A-Z]{3}$");
            return voterKeyRegex.IsMatch(VoterKey);
        }

        // Method to format INE information
        public override string ToString()
        {
            return $"Full Name: {FullName}, CURP: {CURP}, Voter Key: {VoterKey}, Address: {Address}";
        }
    }
}
