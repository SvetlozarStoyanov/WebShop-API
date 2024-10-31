namespace Models.Dto.PhoneNumbers
{
    public class UpdatePhoneNumbersDto
    {
        public IEnumerable<PhoneNumberCreateDto> CreatedPhoneNumbers { get; set; }
        public IEnumerable<PhoneNumberEditDto> EditedPhoneNumbers { get; set; }
        public IEnumerable<PhoneNumberDeleteDto> DeletedPhoneNumbers { get; set; }
    }
}
