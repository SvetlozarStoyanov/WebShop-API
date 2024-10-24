namespace Contracts.Services.Common.Helpers
{
    public interface IEnumMapper
    {
        public Dictionary<long, string> MapEnumToDictionary<TEnum>(TEnum enumeration) where TEnum : Enum;
    }
}
