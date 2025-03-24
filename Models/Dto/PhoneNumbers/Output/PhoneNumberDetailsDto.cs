namespace Models.Dto.PhoneNumbers.Output
{
    public class PhoneNumberDetailsDto
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public bool IsMain { get; set; }
        public long CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
