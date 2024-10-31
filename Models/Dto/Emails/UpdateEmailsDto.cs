namespace Models.Dto.Emails
{
    public class UpdateEmailsDto
    {
        public IEnumerable<EmailCreateDto> CreatedEmails { get; set; }
        public IEnumerable<EmailEditDto> EditedEmails { get; set; }
        public IEnumerable<EmailDeleteDto> DeletedEmails { get; set; }
    }
}
