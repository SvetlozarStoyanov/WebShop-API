namespace Database.Entities.Common
{
    public class Country
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ISO2Code { get; set; }
        public string ISO3Code { get; set; }
        public string PhoneCode { get; set; }
    }
}
