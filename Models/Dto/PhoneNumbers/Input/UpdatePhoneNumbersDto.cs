namespace Models.Dto.PhoneNumbers.Input
{
    public class UpdatePhoneNumbersDto
    {
        public IEnumerable<PhoneNumberCreateDto> CreatedPhoneNumbers { get; set; }
        public IEnumerable<PhoneNumberEditDto> EditedPhoneNumbers { get; set; }
        public IEnumerable<PhoneNumberDeleteDto> DeletedPhoneNumbers { get; set; }
    }
}
