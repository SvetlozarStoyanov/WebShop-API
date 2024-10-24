using Contracts.Services.Common.Helpers;

namespace ServiceLayer.Common.Helpers
{
    public class EnumMapper : IEnumMapper
    {
        public Dictionary<long, string> MapEnumToDictionary<TEnum>(TEnum enumeration) where TEnum : Enum
        {
            var enumKeys = Enum.GetValues(typeof(TEnum));

            var dictionary = new Dictionary<long, string>();

            foreach (var item in enumKeys)
            {
                dictionary.Add((long)item, item.ToString());
            }

            return dictionary;
        }
    }
}
