using Contracts.Services.Seeding;
using Database;
using Database.Entities.Addresses;
using Database.Entities.Common;
using Database.Entities.Common.Enums.InventoryTransactions;
using Database.Entities.Common.Enums.Orders;
using Database.Entities.Common.Enums.Products;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.InventoryTransactions;
using Database.Entities.Common.Nomenclatures.Orders;
using Database.Entities.Common.Nomenclatures.Products;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Customers;
using Database.Entities.Discounts;
using Database.Entities.Emails;
using Database.Entities.Identity;
using Database.Entities.Inventory;
using Database.Entities.Orders;
using Database.Entities.PhoneNumbers;
using Database.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;

namespace Services.Seeding
{
    public class SeedingService : ISeedingService
    {
        private readonly WebShopDbContext context;

        #region Common 
        private static AddressStatus[] addressStatuses = Enum.GetNames(typeof(AddressStatuses))
                                                             .Select(x => new AddressStatus()
                                                             {
                                                                 Name = x.ToString(),
                                                             })
                                                             .ToArray();

        private static DiscountStatus[] discountStatuses = Enum.GetNames(typeof(DiscountStatuses))
                                                             .Select(x => new DiscountStatus()
                                                             {
                                                                 Name = x.ToString(),
                                                             })
                                                             .ToArray();

        private static EmailStatus[] emailStatuses = Enum.GetNames(typeof(EmailStatuses))
                                                             .Select(x => new EmailStatus()
                                                             {
                                                                 Name = x.ToString(),
                                                             })
                                                             .ToArray();

        private static OrderStatus[] orderStatuses = Enum.GetNames(typeof(OrderStatuses))
                                                             .Select(x => new OrderStatus()
                                                             {
                                                                 Name = x.ToString(),
                                                             })
                                                             .ToArray();

        private static OrderDetailsStage[] orderDetailsStages = Enum.GetNames(typeof(OrderDetailsStages))
                                                             .Select(x => new OrderDetailsStage()
                                                             {
                                                                 Name = x.ToString(),
                                                             })
                                                             .ToArray();

        private static OrderDetailsStatus[] orderDetailsStatuses = Enum.GetNames(typeof(OrderDetailsStatuses))
                                                             .Select(x => new OrderDetailsStatus()
                                                             {
                                                                 Name = x.ToString(),
                                                             })
                                                             .ToArray();

        private static PhoneNumberStatus[] phoneNumberStatuses = Enum.GetNames(typeof(PhoneNumberStatuses))
                                                                 .Select(x => new PhoneNumberStatus()
                                                                 {
                                                                     Name = x.ToString(),
                                                                 })
                                                                 .ToArray();

        private static InventoryTransactionType[] inventoryTransactionTypes = Enum.GetNames(typeof(InventoryTransactionTypes))
                                                                             .Select(x => new InventoryTransactionType()
                                                                             {
                                                                                 Name = x.ToString(),
                                                                             })
                                                                             .ToArray();

        private static ProductCategory[] productCategories = Enum.GetNames(typeof(ProductCategories))
                                                    .Select(x => new ProductCategory()
                                                    {
                                                        Name = x.ToString(),
                                                    })
                                                    .ToArray();

        private static Country[] countries = new Country[]
        {
            new Country{ Name = "Afghanistan", ISO2Code = "AF", ISO3Code = "AFG", PhoneCode = "+93"},
            new Country{ Name = "Albania", ISO2Code = "AL", ISO3Code = "ALB", PhoneCode = "+355"},
            new Country{ Name = "Algeria", ISO2Code = "DZ", ISO3Code = "DZA", PhoneCode = "+213"},
            new Country{ Name = "Andorra", ISO2Code = "AD", ISO3Code = "AND", PhoneCode = "+376"},
            new Country{ Name = "Angola", ISO2Code = "AO", ISO3Code = "AGO", PhoneCode = "+244"},
            new Country{ Name = "Antigua and Barbuda", ISO2Code = "AG", ISO3Code = "ATG", PhoneCode = "+1-268"},
            new Country{ Name = "Argentina", ISO2Code = "AR", ISO3Code = "ARG", PhoneCode = "+54"},
            new Country{ Name = "Armenia", ISO2Code = "AM", ISO3Code = "ARM", PhoneCode = "+374"},
            new Country{ Name = "Australia", ISO2Code = "AU", ISO3Code = "AUS", PhoneCode = "+61"},
            new Country{ Name = "Austria", ISO2Code = "AT", ISO3Code = "AUT", PhoneCode = "+43"},
            new Country{ Name = "Azerbaijan", ISO2Code = "AZ", ISO3Code = "AZE", PhoneCode = "+994"},
            new Country{ Name = "Bahamas", ISO2Code = "BS", ISO3Code = "BHS", PhoneCode = "+1-242"},
            new Country{ Name = "Bahrain", ISO2Code = "BH", ISO3Code = "BHR", PhoneCode = "+973"},
            new Country{ Name = "Bangladesh", ISO2Code = "BD", ISO3Code = "BGD", PhoneCode = "+880"},
            new Country{ Name = "Barbados", ISO2Code = "BB", ISO3Code = "BRB", PhoneCode = "+1-246"},
            new Country{ Name = "Belarus", ISO2Code = "BY", ISO3Code = "BLR", PhoneCode = "+375"},
            new Country{ Name = "Belgium", ISO2Code = "BE", ISO3Code = "BEL", PhoneCode = "+32"},
            new Country{ Name = "Belize", ISO2Code = "BZ", ISO3Code = "BLZ", PhoneCode = "+501"},
            new Country{ Name = "Benin", ISO2Code = "BJ", ISO3Code = "BEN", PhoneCode = "+229"},
            new Country{ Name = "Bhutan", ISO2Code = "BT", ISO3Code = "BTN", PhoneCode = "+975"},
            new Country{ Name = "Bolivia", ISO2Code = "BO", ISO3Code = "BOL", PhoneCode = "+591"},
            new Country{ Name = "Bosnia and Herzegovina", ISO2Code = "BA", ISO3Code = "BIH", PhoneCode = "+387"},
            new Country{ Name = "Botswana", ISO2Code = "BW", ISO3Code = "BWA", PhoneCode = "+267"},
            new Country{ Name = "Brazil", ISO2Code = "BR", ISO3Code = "BRA", PhoneCode = "+55"},
            new Country{ Name = "Brunei", ISO2Code = "BN", ISO3Code = "BRN", PhoneCode = "+673"},
            new Country{ Name = "Bulgaria", ISO2Code = "BG", ISO3Code = "BGR", PhoneCode = "+359"},
            new Country{ Name = "Burkina Faso", ISO2Code = "BF", ISO3Code = "BFA", PhoneCode = "+226"},
            new Country{ Name = "Burundi", ISO2Code = "BI", ISO3Code = "BDI", PhoneCode = "+257"},
            new Country{ Name = "Cabo Verde", ISO2Code = "CV", ISO3Code = "CPV", PhoneCode = "+238"},
            new Country{ Name = "Cambodia", ISO2Code = "KH", ISO3Code = "KHM", PhoneCode = "+855"},
            new Country{ Name = "Cameroon", ISO2Code = "CM", ISO3Code = "CMR", PhoneCode = "+237"},
            new Country{ Name = "Canada", ISO2Code = "CA", ISO3Code = "CAN", PhoneCode = "+1"},
            new Country{ Name = "Central African Republic", ISO2Code = "CF", ISO3Code = "CAF", PhoneCode = "+236"},
            new Country{ Name = "Chad", ISO2Code = "TD", ISO3Code = "TCD", PhoneCode = "+235"},
            new Country{ Name = "Chile", ISO2Code = "CL", ISO3Code = "CHL", PhoneCode = "+56"},
            new Country{ Name = "China", ISO2Code = "CN", ISO3Code = "CHN", PhoneCode = "+86"},
            new Country{ Name = "Colombia", ISO2Code = "CO", ISO3Code = "COL", PhoneCode = "+57"},
            new Country{ Name = "Comoros", ISO2Code = "KM", ISO3Code = "COM", PhoneCode = "+269"},
            new Country{ Name = "Congo (Congo-Brazzaville)", ISO2Code = "CG", ISO3Code = "COG", PhoneCode = "+242"},
            new Country{ Name = "Congo (Democratic Republic)", ISO2Code = "CD", ISO3Code = "COD", PhoneCode = "+243"},
            new Country{ Name = "Costa Rica", ISO2Code = "CR", ISO3Code = "CRI", PhoneCode = "+506"},
            new Country{ Name = "Croatia", ISO2Code = "HR", ISO3Code = "HRV", PhoneCode = "+385"},
            new Country{ Name = "Cuba", ISO2Code = "CU", ISO3Code = "CUB", PhoneCode = "+53"},
            new Country{ Name = "Cyprus", ISO2Code = "CY", ISO3Code = "CYP", PhoneCode = "+357"},
            new Country{ Name = "Czechia (Czech Republic)", ISO2Code = "CZ", ISO3Code = "CZE", PhoneCode = "+420"},
            new Country{ Name = "Denmark", ISO2Code = "DK", ISO3Code = "DNK", PhoneCode = "+45"},
            new Country{ Name = "Djibouti", ISO2Code = "DJ", ISO3Code = "DJI", PhoneCode = "+253"},
            new Country{ Name = "Dominica", ISO2Code = "DM", ISO3Code = "DMA", PhoneCode = "+1-767"},
            new Country{ Name = "Dominican Republic", ISO2Code = "DO", ISO3Code = "DOM", PhoneCode = "+1-809"},
            new Country{ Name = "Ecuador", ISO2Code = "EC", ISO3Code = "ECU", PhoneCode = "+593"},
            new Country{ Name = "Egypt", ISO2Code = "EG", ISO3Code = "EGY", PhoneCode = "+20"},
            new Country{ Name = "El Salvador", ISO2Code = "SV", ISO3Code = "SLV", PhoneCode = "+503"},
            new Country{ Name = "Equatorial Guinea", ISO2Code = "GQ", ISO3Code = "GNQ", PhoneCode = "+240"},
            new Country{ Name = "Eritrea", ISO2Code = "ER", ISO3Code = "ERI", PhoneCode = "+291"},
            new Country{ Name = "Estonia", ISO2Code = "EE", ISO3Code = "EST", PhoneCode = "+372"},
            new Country{ Name = "Eswatini (Swaziland)", ISO2Code = "SZ", ISO3Code = "SWZ", PhoneCode = "+268"},
            new Country{ Name = "Ethiopia", ISO2Code = "ET", ISO3Code = "ETH", PhoneCode = "+251"},
            new Country{ Name = "Fiji", ISO2Code = "FJ", ISO3Code = "FJI", PhoneCode = "+679"},
            new Country{ Name = "Finland", ISO2Code = "FI", ISO3Code = "FIN", PhoneCode = "+358"},
            new Country{ Name = "France", ISO2Code = "FR", ISO3Code = "FRA", PhoneCode = "+33"},
            new Country{ Name = "Gabon", ISO2Code = "GA", ISO3Code = "GAB", PhoneCode = "+241"},
            new Country{ Name = "Gambia", ISO2Code = "GM", ISO3Code = "GMB", PhoneCode = "+220"},
            new Country{ Name = "Georgia", ISO2Code = "GE", ISO3Code = "GEO", PhoneCode = "+995"},
            new Country{ Name = "Germany", ISO2Code = "DE", ISO3Code = "DEU", PhoneCode = "+49"},
            new Country{ Name = "Ghana", ISO2Code = "GH", ISO3Code = "GHA", PhoneCode = "+233"},
            new Country{ Name = "Greece", ISO2Code = "GR", ISO3Code = "GRC", PhoneCode = "+30"},
            new Country{ Name = "Grenada", ISO2Code = "GD", ISO3Code = "GRD", PhoneCode = "+1-473"},
            new Country{ Name = "Guatemala", ISO2Code = "GT", ISO3Code = "GTM", PhoneCode = "+502"},
            new Country{ Name = "Guinea", ISO2Code = "GN", ISO3Code = "GIN", PhoneCode = "+224"},
            new Country{ Name = "Guinea-Bissau", ISO2Code = "GW", ISO3Code = "GNB", PhoneCode = "+245"},
            new Country{ Name = "Guyana", ISO2Code = "GY", ISO3Code = "GUY", PhoneCode = "+592"},
            new Country{ Name = "Haiti", ISO2Code = "HT", ISO3Code = "HTI", PhoneCode = "+509"},
            new Country{ Name = "Honduras", ISO2Code = "HN", ISO3Code = "HND", PhoneCode = "+504"},
            new Country{ Name = "Hungary", ISO2Code = "HU", ISO3Code = "HUN", PhoneCode = "+36"},
            new Country{ Name = "Iceland", ISO2Code = "IS", ISO3Code = "ISL", PhoneCode = "+354"},
            new Country{ Name = "India", ISO2Code = "IN", ISO3Code = "IND", PhoneCode = "+91"},
            new Country{ Name = "Indonesia", ISO2Code = "ID", ISO3Code = "IDN", PhoneCode = "+62"},
            new Country{ Name = "Iran", ISO2Code = "IR", ISO3Code = "IRN", PhoneCode = "+98"},
            new Country{ Name = "Iraq", ISO2Code = "IQ", ISO3Code = "IRQ", PhoneCode = "+964"},
            new Country{ Name = "Ireland", ISO2Code = "IE", ISO3Code = "IRL", PhoneCode = "+353"},
            new Country{ Name = "Israel", ISO2Code = "IL", ISO3Code = "ISR", PhoneCode = "+972"},
            new Country{ Name = "Italy", ISO2Code = "IT", ISO3Code = "ITA", PhoneCode = "+39"},
            new Country{ Name = "Jamaica", ISO2Code = "JM", ISO3Code = "JAM", PhoneCode = "+1-876"},
            new Country{ Name = "Japan", ISO2Code = "JP", ISO3Code = "JPN", PhoneCode = "+81"},
            new Country{ Name = "Jordan", ISO2Code = "JO", ISO3Code = "JOR", PhoneCode = "+962"},
            new Country{ Name = "Kazakhstan", ISO2Code = "KZ", ISO3Code = "KAZ", PhoneCode = "+7"},
            new Country{ Name = "Kenya", ISO2Code = "KE", ISO3Code = "KEN", PhoneCode = "+254"},
            new Country{ Name = "Kiribati", ISO2Code = "KI", ISO3Code = "KIR", PhoneCode = "+686"},
            new Country{ Name = "Kuwait", ISO2Code = "KW", ISO3Code = "KWT", PhoneCode = "+965"},
            new Country{ Name = "Kyrgyzstan", ISO2Code = "KG", ISO3Code = "KGZ", PhoneCode = "+996"},
            new Country{ Name = "Laos", ISO2Code = "LA", ISO3Code = "LAO", PhoneCode = "+856"},
            new Country{ Name = "Latvia", ISO2Code = "LV", ISO3Code = "LVA", PhoneCode = "+371"},
            new Country{ Name = "Lebanon", ISO2Code = "LB", ISO3Code = "LBN", PhoneCode = "+961"},
            new Country{ Name = "Lesotho", ISO2Code = "LS", ISO3Code = "LSO", PhoneCode = "+266"},
            new Country{ Name = "Liberia", ISO2Code = "LR", ISO3Code = "LBR", PhoneCode = "+231"},
            new Country{ Name = "Libya", ISO2Code = "LY", ISO3Code = "LBY", PhoneCode = "+218"},
            new Country{ Name = "Liechtenstein", ISO2Code = "LI", ISO3Code = "LIE", PhoneCode = "+423"},
            new Country{ Name = "Lithuania", ISO2Code = "LT", ISO3Code = "LTU", PhoneCode = "+370"},
            new Country{ Name = "Luxembourg", ISO2Code = "LU", ISO3Code = "LUX", PhoneCode = "+352"},
            new Country{ Name = "Madagascar", ISO2Code = "MG", ISO3Code = "MDG", PhoneCode = "+261"},
            new Country{ Name = "Malawi", ISO2Code = "MW", ISO3Code = "MWI", PhoneCode = "+265"},
            new Country{ Name = "Malaysia", ISO2Code = "MY", ISO3Code = "MYS", PhoneCode = "+60"},
            new Country{ Name = "Maldives", ISO2Code = "MV", ISO3Code = "MDV", PhoneCode = "+960"},
            new Country{ Name = "Mali", ISO2Code = "ML", ISO3Code = "MLI", PhoneCode = "+223"},
            new Country{ Name = "Malta", ISO2Code = "MT", ISO3Code = "MLT", PhoneCode = "+356"},
            new Country{ Name = "Marshall Islands", ISO2Code = "MH", ISO3Code = "MHL", PhoneCode = "+692"},
            new Country{ Name = "Mauritania", ISO2Code = "MR", ISO3Code = "MRT", PhoneCode = "+222"},
            new Country{ Name = "Mauritius", ISO2Code = "MU", ISO3Code = "MUS", PhoneCode = "+230"},
            new Country{ Name = "Mexico", ISO2Code = "MX", ISO3Code = "MEX", PhoneCode = "+52"},
            new Country{ Name = "Micronesia", ISO2Code = "FM", ISO3Code = "FSM", PhoneCode = "+691"},
            new Country{ Name = "Moldova", ISO2Code = "MD", ISO3Code = "MDA", PhoneCode = "+373"},
            new Country{ Name = "Monaco", ISO2Code = "MC", ISO3Code = "MCO", PhoneCode = "+377"},
            new Country{ Name = "Mongolia", ISO2Code = "MN", ISO3Code = "MNG", PhoneCode = "+976"},
            new Country{ Name = "Montenegro", ISO2Code = "ME", ISO3Code = "MNE", PhoneCode = "+382"},
            new Country{ Name = "Morocco", ISO2Code = "MA", ISO3Code = "MAR", PhoneCode = "+212"},
            new Country{ Name = "Mozambique", ISO2Code = "MZ", ISO3Code = "MOZ", PhoneCode = "+258"},
            new Country{ Name = "Myanmar (Burma)", ISO2Code = "MM", ISO3Code = "MMR", PhoneCode = "+95"},
            new Country{ Name = "Namibia", ISO2Code = "NA", ISO3Code = "NAM", PhoneCode = "+264"},
            new Country{ Name = "Nauru", ISO2Code = "NR", ISO3Code = "NRU", PhoneCode = "+674"},
            new Country{ Name = "Nepal", ISO2Code = "NP", ISO3Code = "NPL", PhoneCode = "+977"},
            new Country{ Name = "Netherlands", ISO2Code = "NL", ISO3Code = "NLD", PhoneCode = "+31"},
            new Country{ Name = "New Zealand", ISO2Code = "NZ", ISO3Code = "NZL", PhoneCode = "+64"},
            new Country{ Name = "Nicaragua", ISO2Code = "NI", ISO3Code = "NIC", PhoneCode = "+505"},
            new Country{ Name = "Niger", ISO2Code = "NE", ISO3Code = "NER", PhoneCode = "+227"},
            new Country{ Name = "Nigeria", ISO2Code = "NG", ISO3Code = "NGA", PhoneCode = "+234"},
            new Country{ Name = "North Macedonia", ISO2Code = "MK", ISO3Code = "MKD", PhoneCode = "+389"},
            new Country{ Name = "Norway", ISO2Code = "NO", ISO3Code = "NOR", PhoneCode = "+47"},
            new Country{ Name = "Oman", ISO2Code = "OM", ISO3Code = "OMN", PhoneCode = "+968"},
            new Country{ Name = "Pakistan", ISO2Code = "PK", ISO3Code = "PAK", PhoneCode = "+92"},
            new Country{ Name = "Palau", ISO2Code = "PW", ISO3Code = "PLW", PhoneCode = "+680"},
            new Country{ Name = "Palestine", ISO2Code = "PS", ISO3Code = "PSE", PhoneCode = "+970"},
            new Country{ Name = "Panama", ISO2Code = "PA", ISO3Code = "PAN", PhoneCode = "+507"},
            new Country{ Name = "Papua New Guinea", ISO2Code = "PG", ISO3Code = "PNG", PhoneCode = "+675"},
            new Country{ Name = "Paraguay", ISO2Code = "PY", ISO3Code = "PRY", PhoneCode = "+595"},
            new Country{ Name = "Peru", ISO2Code = "PE", ISO3Code = "PER", PhoneCode = "+51"},
            new Country{ Name = "Philippines", ISO2Code = "PH", ISO3Code = "PHL", PhoneCode = "+63"},
            new Country{ Name = "Poland", ISO2Code = "PL", ISO3Code = "POL", PhoneCode = "+48"},
            new Country{ Name = "Portugal", ISO2Code = "PT", ISO3Code = "PRT", PhoneCode = "+351"},
            new Country{ Name = "Qatar", ISO2Code = "QA", ISO3Code = "QAT", PhoneCode = "+974"},
            new Country{ Name = "Romania", ISO2Code = "RO", ISO3Code = "ROU", PhoneCode = "+40"},
            new Country{ Name = "Russia", ISO2Code = "RU", ISO3Code = "RUS", PhoneCode = "+7"},
            new Country{ Name = "Rwanda", ISO2Code = "RW", ISO3Code = "RWA", PhoneCode = "+250"},
            new Country{ Name = "Saint Kitts and Nevis", ISO2Code = "KN", ISO3Code = "KNA", PhoneCode = "+1-869"},
            new Country{ Name = "Saint Lucia", ISO2Code = "LC", ISO3Code = "LCA", PhoneCode = "+1-758"},
            new Country{ Name = "Saint Vincent and the Grenadines", ISO2Code = "VC", ISO3Code = "VCT", PhoneCode = "+1-784"},
            new Country{ Name = "Samoa", ISO2Code = "WS", ISO3Code = "WSM", PhoneCode = "+685"},
            new Country{ Name = "San Marino", ISO2Code = "SM", ISO3Code = "SMR", PhoneCode = "+378"},
            new Country{ Name = "Sao Tome and Principe", ISO2Code = "ST", ISO3Code = "STP", PhoneCode = "+239"},
            new Country{ Name = "Saudi Arabia", ISO2Code = "SA", ISO3Code = "SAU", PhoneCode = "+966"},
            new Country{ Name = "Senegal", ISO2Code = "SN", ISO3Code = "SEN", PhoneCode = "+221"},
            new Country{ Name = "Serbia", ISO2Code = "RS", ISO3Code = "SRB", PhoneCode = "+381"},
            new Country{ Name = "Seychelles", ISO2Code = "SC", ISO3Code = "SYC", PhoneCode = "+248"},
            new Country{ Name = "Sierra Leone", ISO2Code = "SL", ISO3Code = "SLE", PhoneCode = "+232"},
            new Country{ Name = "Singapore", ISO2Code = "SG", ISO3Code = "SGP", PhoneCode = "+65"},
            new Country{ Name = "Slovakia", ISO2Code = "SK", ISO3Code = "SVK", PhoneCode = "+421"},
            new Country{ Name = "Slovenia", ISO2Code = "SI", ISO3Code = "SVN", PhoneCode = "+386"},
            new Country{ Name = "Solomon Islands", ISO2Code = "SB", ISO3Code = "SLB", PhoneCode = "+677"},
            new Country{ Name = "Somalia", ISO2Code = "SO", ISO3Code = "SOM", PhoneCode = "+252"},
            new Country{ Name = "South Africa", ISO2Code = "ZA", ISO3Code = "ZAF", PhoneCode = "+27"},
            new Country{ Name = "South Sudan", ISO2Code = "SS", ISO3Code = "SSD", PhoneCode = "+211"},
            new Country{ Name = "Spain", ISO2Code = "ES", ISO3Code = "ESP", PhoneCode = "+34"},
            new Country{ Name = "Sri Lanka", ISO2Code = "LK", ISO3Code = "LKA", PhoneCode = "+94"},
            new Country{ Name = "Sudan", ISO2Code = "SD", ISO3Code = "SDN", PhoneCode = "+249"},
            new Country{ Name = "Suriname", ISO2Code = "SR", ISO3Code = "SUR", PhoneCode = "+597"},
            new Country{ Name = "Sweden", ISO2Code = "SE", ISO3Code = "SWE", PhoneCode = "+46"},
            new Country{ Name = "Switzerland", ISO2Code = "CH", ISO3Code = "CHE", PhoneCode = "+41"},
            new Country{ Name = "Syria", ISO2Code = "SY", ISO3Code = "SYR", PhoneCode = "+963"},
            new Country{ Name = "Taiwan", ISO2Code = "TW", ISO3Code = "TWN", PhoneCode = "+886"},
            new Country{ Name = "Tajikistan", ISO2Code = "TJ", ISO3Code = "TJK", PhoneCode = "+992"},
            new Country{ Name = "Tanzania", ISO2Code = "TZ", ISO3Code = "TZA", PhoneCode = "+255"},
            new Country{ Name = "Thailand", ISO2Code = "TH", ISO3Code = "THA", PhoneCode = "+66"},
            new Country{ Name = "Timor-Leste", ISO2Code = "TL", ISO3Code = "TLS", PhoneCode = "+670"},
            new Country{ Name = "Togo", ISO2Code = "TG", ISO3Code = "TGO", PhoneCode = "+228"},
            new Country{ Name = "Tonga", ISO2Code = "TO", ISO3Code = "TON", PhoneCode = "+676"},
            new Country{ Name = "Trinidad and Tobago", ISO2Code = "TT", ISO3Code = "TTO", PhoneCode = "+1-868"},
            new Country{ Name = "Tunisia", ISO2Code = "TN", ISO3Code = "TUN", PhoneCode = "+216"},
            new Country{ Name = "Turkey", ISO2Code = "TR", ISO3Code = "TUR", PhoneCode = "+90"},
            new Country{ Name = "Turkmenistan", ISO2Code = "TM", ISO3Code = "TKM", PhoneCode = "+993"},
            new Country{ Name = "Tuvalu", ISO2Code = "TV", ISO3Code = "TUV", PhoneCode = "+688"},
            new Country{ Name = "Uganda", ISO2Code = "UG", ISO3Code = "UGA", PhoneCode = "+256"},
            new Country{ Name = "Ukraine", ISO2Code = "UA", ISO3Code = "UKR", PhoneCode = "+380"},
            new Country{ Name = "United Arab Emirates", ISO2Code = "AE", ISO3Code = "ARE", PhoneCode = "+971"},
            new Country{ Name = "United Kingdom", ISO2Code = "GB", ISO3Code = "GBR", PhoneCode = "+44"},
            new Country{ Name = "United States", ISO2Code = "US", ISO3Code = "USA", PhoneCode = "+1"},
            new Country{ Name = "Uruguay", ISO2Code = "UY", ISO3Code = "URY", PhoneCode = "+598"},
            new Country{ Name = "Uzbekistan", ISO2Code = "UZ", ISO3Code = "UZB", PhoneCode = "+998"},
            new Country{ Name = "Vanuatu", ISO2Code = "VU", ISO3Code = "VUT", PhoneCode = "+678"},
            new Country{ Name = "Vatican City", ISO2Code = "VA", ISO3Code = "VAT", PhoneCode = "+379"},
            new Country{ Name = "Venezuela", ISO2Code = "VE", ISO3Code = "VEN", PhoneCode = "+58"},
            new Country{ Name = "Vietnam", ISO2Code = "VN", ISO3Code = "VNM", PhoneCode = "+84"},
            new Country{ Name = "Yemen", ISO2Code = "YE", ISO3Code = "YEM", PhoneCode = "+967"},
            new Country{ Name = "Zambia", ISO2Code = "ZM", ISO3Code = "ZMB", PhoneCode = "+260"},
            new Country{ Name = "Zimbabwe", ISO2Code = "ZW", ISO3Code = "ZWE", PhoneCode = "+263"}
        };

        #endregion

        #region Users and Roles
        private static ApplicationRole[] roles = new ApplicationRole[]
        {
            new ApplicationRole { Id = "1136356c-6824-4193-aaf2-fb5cc4207873", Name = "Admin", NormalizedName = "ADMIN", Description = "Administrator" },
            new ApplicationRole { Id = "e50274c1-3e7d-443f-84b0-7f2a29cf7a87", Name = "Customer", NormalizedName = "CUSTOMER", Description = "Customer" }
        };

        private static ApplicationUser[] users = new ApplicationUser[]
        {
            new ApplicationUser { Id = "f2c3e240-cae9-4d2f-952e-99d09157b742", UserName = "Admin", NormalizedUserName = "ADMIN", PasswordHash = "AQAAAAIAAYagAAAAEF8iWq5/zeUH+rQABFMDEF/PJN8Tmt0I7dH40xLHkwOGi7Zry4OWB9edm/xczeaj5g==", FirstName = "Admin", MiddleName = null, LastName = "Admin"},
            new ApplicationUser { Id = "3c5d6b41-b89e-4368-b100-513d649f54af", UserName = "JohnDoe", NormalizedUserName = "JOHNDOE", PasswordHash = "AQAAAAIAAYagAAAAEJyCzIekweUzeVk1+8SG8nIqK7tiDPqv29jdpfGHToaLynRpdOVkVTcIm4xzEk4baQ==", FirstName = "John", MiddleName = null, LastName = "Doe"},
            new ApplicationUser { Id = "72959841-e649-4fcd-8962-8f45c5a9c3df", UserName = "TestGuy", NormalizedUserName = "TESTGUY", PasswordHash = "AQAAAAIAAYagAAAAEHBNp+dGo3r9LPTBCbu16ttsMQY2rGThTlkt3NJI7JlN7b+xHhsoAyZ/Wn/v2KpVtQ==", FirstName = "Test", MiddleName = null, LastName = "Guy"},
            new ApplicationUser { Id = "642b607b-cfcf-486c-bda2-bf8cfd6dd339", UserName = "TestUser", NormalizedUserName = "TESTUSER", PasswordHash = "AQAAAAIAAYagAAAAEAhiXQcxLOgFLkJ8KCizUuGMfZhTpXVlqnUD6gHnQVJfTKohidZosyV9ddgLufUSqg==", FirstName = "Test", MiddleName = null, LastName = "User" },
        };

        private static IdentityUserRole<string>[] usersRoles = new IdentityUserRole<string>[]
        {
            new IdentityUserRole<string>(){ UserId = "f2c3e240-cae9-4d2f-952e-99d09157b742", RoleId = "1136356c-6824-4193-aaf2-fb5cc4207873" },
            new IdentityUserRole<string>(){ UserId = "3c5d6b41-b89e-4368-b100-513d649f54af", RoleId = "e50274c1-3e7d-443f-84b0-7f2a29cf7a87" },
            new IdentityUserRole<string>(){ UserId = "72959841-e649-4fcd-8962-8f45c5a9c3df", RoleId = "e50274c1-3e7d-443f-84b0-7f2a29cf7a87" },
            new IdentityUserRole<string>(){ UserId = "642b607b-cfcf-486c-bda2-bf8cfd6dd339", RoleId = "e50274c1-3e7d-443f-84b0-7f2a29cf7a87" },
        };
        #endregion

        #region Customers and Personal Information
        private static Customer[] customers = new Customer[]
        {
            new Customer { UserId = "3c5d6b41-b89e-4368-b100-513d649f54af" },
            new Customer { UserId = "72959841-e649-4fcd-8962-8f45c5a9c3df" },
            new Customer { UserId = "642b607b-cfcf-486c-bda2-bf8cfd6dd339" },
        };

        private static Address[] addresses = new Address[]
        {
            new Address { Customer = customers.First(x => x.UserId == "3c5d6b41-b89e-4368-b100-513d649f54af"), City = "Varna", AddressLineOne = "Batova Street Number 5", Country = countries.First(x => x.ISO3Code == "BGR"), IsMain = true, PostCode = "9000", Status = addressStatuses.First(x => x.Name == AddressStatuses.Active.ToString())},
            new Address { Customer = customers.First(x => x.UserId == "3c5d6b41-b89e-4368-b100-513d649f54af"), City = "Varna", AddressLineOne = "Batova Street Number 18", Country = countries.First(x => x.ISO3Code == "BGR"), IsMain = false, PostCode = "9000", Status = addressStatuses.First(x => x.Name == AddressStatuses.Active.ToString()) },
            new Address { Customer = customers.First(x => x.UserId == "72959841-e649-4fcd-8962-8f45c5a9c3df"), City = "Varna", AddressLineOne = "Pirin Street Number 29", Country = countries.First(x => x.ISO3Code == "BGR"), IsMain = true, PostCode = "9000", Status = addressStatuses.First(x => x.Name == AddressStatuses.Active.ToString()) },
            new Address { Customer = customers.First(x => x.UserId == "642b607b-cfcf-486c-bda2-bf8cfd6dd339"), City = "New York", AddressLineOne = "3 Elmwood Ave", AddressLineTwo = "Middletown, NY 10940", Country = countries.First(x => x.ISO3Code == "USA"), IsMain = true, PostCode = "9000", Status = addressStatuses.First(x => x.Name == AddressStatuses.Active.ToString()) },
        };

        private static Email[] emails = new Email[]
        {
            new Email { Customer = customers.First(x => x.UserId == "3c5d6b41-b89e-4368-b100-513d649f54af"), Address = "johndoe@mail.com", IsMain = true, Status = emailStatuses.First(x => x.Name == EmailStatuses.Active.ToString()) },
            new Email { Customer = customers.First(x => x.UserId == "72959841-e649-4fcd-8962-8f45c5a9c3df"), Address = "testguy@mail.com", IsMain = true, Status = emailStatuses.First(x => x.Name == EmailStatuses.Active.ToString()) },
            new Email { Customer = customers.First(x => x.UserId == "642b607b-cfcf-486c-bda2-bf8cfd6dd339"), Address = "testuser@mail.com", IsMain = true, Status = emailStatuses.First(x => x.Name == EmailStatuses.Active.ToString()) }
        };

        private static PhoneNumber[] phoneNumbers = new PhoneNumber[]
        {
            new PhoneNumber { Customer = customers.First(x => x.UserId == "3c5d6b41-b89e-4368-b100-513d649f54af"), Number = "885222357", Country = countries.First(x => x.ISO3Code == "BGR"), Status = phoneNumberStatuses.First(x => x.Name == PhoneNumberStatuses.Active.ToString()) },
            new PhoneNumber { Customer = customers.First(x => x.UserId == "72959841-e649-4fcd-8962-8f45c5a9c3df"), Number = "895315329", Country = countries.First(x => x.ISO3Code == "BGR"), Status = phoneNumberStatuses.First(x => x.Name == PhoneNumberStatuses.Active.ToString()) },
            new PhoneNumber { Customer = customers.First(x => x.UserId == "642b607b-cfcf-486c-bda2-bf8cfd6dd339"), Number = "8144242362", Country = countries.First(x => x.ISO3Code == "USA"), Status = phoneNumberStatuses.First(x => x.Name == PhoneNumberStatuses.Active.ToString()) }
        };
        #endregion

        #region Individual Products

        private static Product ElectricKettle = new Product
        {
            Name = "Electric Kettle",
            Description = "High-speed boiling kettle.",
            Price = 25m,
            QuantityInStock = 500,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Kitchenware.ToString()),
                productCategories.First(x => x.Name == ProductCategories.HomeAppliances.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 500,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product Smartphone = new Product
        {
            Name = "Smartphone",
            Description = "Latest model with advanced features.",
            Price = 699.99m,
            QuantityInStock = 200,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.PhonesAndTablets.ToString()),
                productCategories.First(x => x.Name == ProductCategories.WearableTech.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 200,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product YogaMat = new Product
        {
            Name = "Yoga Mat",
            Description = "Non-slip yoga mat for comfort.",
            Price = 29.99m,
            QuantityInStock = 150,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.FitnessEquipment.ToString()),
                productCategories.First(x => x.Name == ProductCategories.HealthAndBeauty.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 150,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product BluetoothSpeaker = new Product
        {
            Name = "Bluetooth Speaker",
            Description = "Portable wireless speaker with great sound.",
            Price = 49.99m,
            QuantityInStock = 300,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.HomeAppliances.ToString()),
                productCategories.First(x => x.Name == ProductCategories.Music.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 300,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product ElectricToothbrush = new Product
        {
            Name = "Electric Toothbrush",
            Description = "Rechargeable electric toothbrush with timer.",
            Price = 45.00m,
            QuantityInStock = 250,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.HealthAndBeauty.ToString()),
                productCategories.First(x => x.Name == ProductCategories.PersonalCare.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 250,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product BoardGame = new Product
        {
            Name = "Board Game",
            Description = "Fun board game for family nights.",
            Price = 35.00m,
            QuantityInStock = 100,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.ToysAndGames.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 100,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product WirelessHeadphones = new Product
        {
            Name = "Wireless Headphones",
            Description = "Noise-canceling wireless headphones.",
            Price = 89.99m,
            QuantityInStock = 80,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.MusicalInstruments.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 80,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product Blender = new Product
        {
            Name = "Blender",
            Description = "High-speed blender for smoothies.",
            Price = 55.00m,
            QuantityInStock = 220,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Kitchenware.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 220,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product PortableCharger = new Product
        {
            Name = "Portable Charger",
            Description = "Compact power bank for devices.",
            Price = 25.99m,
            QuantityInStock = 400,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Automotive.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 400,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product CoffeeMaker = new Product
        {
            Name = "Coffee Maker",
            Description = "Brew coffee easily with this machine.",
            Price = 79.99m,
            QuantityInStock = 180,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Kitchenware.ToString()),
                productCategories.First(x => x.Name == ProductCategories.HomeAppliances.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 180,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product Dumbbells = new Product
        {
            Name = "Dumbbells",
            Description = "Adjustable dumbbells for home workouts.",
            Price = 99.99m,
            QuantityInStock = 120,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.FitnessEquipment.ToString()),
                productCategories.First(x => x.Name == ProductCategories.HealthAndBeauty.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 120,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product LeatherWallet = new Product
        {
            Name = "Leather Wallet",
            Description = "Genuine leather wallet with multiple slots.",
            Price = 45.00m,
            QuantityInStock = 300,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Footwear.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 300,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product AirPurifier = new Product
        {
            Name = "Air Purifier",
            Description = "Removes allergens and pollutants from the air.",
            Price = 120.00m,
            QuantityInStock = 75,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.HomeAppliances.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 75,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product GamingMouse = new Product
        {
            Name = "Gaming Mouse",
            Description = "Ergonomic gaming mouse with customizable buttons.",
            Price = 60.00m,
            QuantityInStock = 150,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.VideoGames.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 150,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product WoolBlanket = new Product
        {
            Name = "Wool Blanket",
            Description = "Cozy wool blanket for cold nights.",
            Price = 70.00m,
            QuantityInStock = 200,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Bedding.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 200,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product InstantPot = new Product
        {
            Name = "Instant Pot",
            Description = "Multi-functional cooker for quick meals.",
            Price = 89.99m,
            QuantityInStock = 160,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Kitchenware.ToString()),
                productCategories.First(x => x.Name == ProductCategories.HomeAppliances.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 160,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product Smartwatch = new Product
        {
            Name = "Smartwatch",
            Description = "Fitness tracking and notifications on your wrist.",
            Price = 199.99m,
            QuantityInStock = 140,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.WearableTech.ToString()),
                productCategories.First(x => x.Name == ProductCategories.HealthAndBeauty.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 140,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product ElectricScooter = new Product
        {
            Name = "Electric Scooter",
            Description = "Eco-friendly electric scooter for short commutes.",
            Price = 499.99m,
            QuantityInStock = 90,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Automotive.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 90,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product Cookbook = new Product
        {
            Name = "Cookbook",
            Description = "Delicious recipes for home cooks.",
            Price = 19.99m,
            QuantityInStock = 300,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Books.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 300,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product YogaBlocks = new Product
        {
            Name = "Yoga Blocks",
            Description = "Durable foam blocks for yoga support.",
            Price = 15.00m,
            QuantityInStock = 200,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.FitnessEquipment.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 200,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product WaterBottle = new Product
        {
            Name = "Water Bottle",
            Description = "Insulated stainless steel water bottle.",
            Price = 25.00m,
            QuantityInStock = 450,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.HealthAndBeauty.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 450,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product NonStickFryingPan = new Product
        {
            Name = "Non-Stick Frying Pan",
            Description = "Easy to clean frying pan for all cooking needs.",
            Price = 30.00m,
            QuantityInStock = 200,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Kitchenware.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 200,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product LEDDeskLamp = new Product
        {
            Name = "LED Desk Lamp",
            Description = "Adjustable LED desk lamp with multiple brightness levels.",
            Price = 39.99m,
            QuantityInStock = 100,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.HomeAppliances.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 100,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product FitnessTracker = new Product
        {
            Name = "Fitness Tracker",
            Description = "Monitor your fitness goals with this wearable.",
            Price = 59.99m,
            QuantityInStock = 130,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.WearableTech.ToString()),
                productCategories.First(x => x.Name == ProductCategories.HealthAndBeauty.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 130,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product Backpack = new Product
        {
            Name = "Backpack",
            Description = "Spacious and stylish backpack for everyday use.",
            Price = 49.99m,
            QuantityInStock = 220,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Bags.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 220,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product PlantPot = new Product
        {
            Name = "Plant Pot",
            Description = "Decorative pot for indoor plants.",
            Price = 15.00m,
            QuantityInStock = 180,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.HomeDecor.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 180,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product WirelessCharger = new Product
        {
            Name = "Wireless Charger",
            Description = "Convenient wireless charging pad for smartphones.",
            Price = 29.99m,
            QuantityInStock = 300,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Electronics.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 300,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product Bicycle = new Product
        {
            Name = "Bicycle",
            Description = "Lightweight bike for urban commuting.",
            Price = 299.99m,
            QuantityInStock = 60,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Automotive.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 60,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product SmartTV = new Product
        {
            Name = "Smart TV",
            Description = "55-inch Smart TV with streaming capabilities.",
            Price = 599.99m,
            QuantityInStock = 40,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Electronics.ToString()),
                productCategories.First(x => x.Name == ProductCategories.TV.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 40,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product ScentedCandle = new Product
        {
            Name = "Scented Candle",
            Description = "Relaxing scented candle for a cozy atmosphere.",
            Price = 15.00m,
            QuantityInStock = 400,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.HomeDecor.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 400,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product SkincareSet = new Product
        {
            Name = "Skincare Set",
            Description = "Complete skincare routine for healthy skin.",
            Price = 49.99m,
            QuantityInStock = 150,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.HealthAndBeauty.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 150,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product CampingTent = new Product
        {
            Name = "Camping Tent",
            Description = "Spacious and durable tent for camping trips.",
            Price = 199.99m,
            QuantityInStock = 80,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.OutdoorEquipment.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 80,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product PetBed = new Product
        {
            Name = "Pet Bed",
            Description = "Comfortable bed for pets of all sizes.",
            Price = 45.00m,
            QuantityInStock = 150,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.PetSupplies.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 150,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product PortableBluetoothSpeaker = new Product
        {
            Name = "Portable Bluetooth Speaker",
            Description = "Lightweight speaker with excellent sound quality.",
            Price = 69.99m,
            QuantityInStock = 200,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Music.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 200,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product LuggageSet = new Product
        {
            Name = "Luggage Set",
            Description = "Stylish luggage set for travelers.",
            Price = 199.99m,
            QuantityInStock = 50,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Bags.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 50,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product EssentialOilDiffuser = new Product
        {
            Name = "Essential Oil Diffuser",
            Description = "Ultrasonic diffuser for essential oils.",
            Price = 39.99m,
            QuantityInStock = 300,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.HomeDecor.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 300,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product ElectricGrill = new Product
        {
            Name = "Electric Grill",
            Description = "Indoor electric grill for delicious meals.",
            Price = 89.99m,
            QuantityInStock = 100,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Kitchenware.ToString()),
                productCategories.First(x => x.Name == ProductCategories.HomeAppliances.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 100,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                },
                new InventoryTransaction()
                {
                    Quantity = 1,
                    Date = DateTime.UtcNow.AddDays(-14),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Sold.ToString()),
                },
                new InventoryTransaction()
                {
                    Quantity = 1,
                    Date = DateTime.UtcNow.AddDays(-13),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Returned.ToString()),
                }
            }
        };

        private static Product HomeSecurityCamera = new Product
        {
            Name = "Home Security Camera",
            Description = "Keep your home safe with this security camera.",
            Price = 99.99m,
            QuantityInStock = 150,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Electronics.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 150,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                },
                new InventoryTransaction()
                {
                    Quantity = 1,
                    Date = DateTime.UtcNow.AddDays(-8),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Sold.ToString()),
                },
                new InventoryTransaction()
                {
                    Quantity = 1,
                    Date = DateTime.UtcNow.AddDays(-2),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Returned.ToString()),
                }
            }
        };

        private static Product Watch = new Product
        {
            Name = "Watch",
            Description = "Elegant watch with leather strap.",
            Price = 149.99m,
            QuantityInStock = 69,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.WearableTech.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 70,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                },
                new InventoryTransaction()
                {
                    Quantity = 1,
                    Date = DateTime.UtcNow.AddDays(-10),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Sold.ToString()),
                }
            }
        };

        private static Product InstantCamera = new Product
        {
            Name = "Instant Camera",
            Description = "Capture moments instantly with this camera.",
            Price = 129.99m,
            QuantityInStock = 90,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Electronics.ToString()),
                productCategories.First(x => x.Name == ProductCategories.Cameras.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 90,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product CampingStove = new Product
        {
            Name = "Camping Stove",
            Description = "Portable stove for camping and outdoor cooking.",
            Price = 89.99m,
            QuantityInStock = 75,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.OutdoorEquipment.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 75,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        private static Product Drone = new Product
        {
            Name = "Drone",
            Description = "High-definition camera drone for aerial photography.",
            Price = 499.99m,
            QuantityInStock = 30,
            Categories = new List<ProductCategory>
            {
                productCategories.First(x => x.Name == ProductCategories.Electronics.ToString())
            },
            InventoryTransactions = new List<InventoryTransaction>()
            {
                new InventoryTransaction()
                {
                    Quantity = 30,
                    Date = DateTime.UtcNow.AddDays(-20),
                    Type = inventoryTransactionTypes.First(x => x.Name == InventoryTransactionTypes.Import.ToString()),
                }
            }
        };

        #endregion

        #region Products
        private static Product[] products = new Product[]
        {
            ElectricKettle,
            Smartphone,
            YogaMat,
            BluetoothSpeaker,
            ElectricToothbrush,
            BoardGame,
            WirelessHeadphones,
            Blender,
            PortableCharger,
            CoffeeMaker,
            Dumbbells,
            LeatherWallet,
            AirPurifier,
            GamingMouse,
            WoolBlanket,
            InstantPot,
            Smartwatch,
            ElectricScooter,
            Cookbook,
            YogaBlocks,
            WaterBottle,
            NonStickFryingPan,
            LEDDeskLamp,
            FitnessTracker,
            Backpack,
            PlantPot,
            WirelessCharger,
            Bicycle,
            SmartTV,
            ScentedCandle,
            SkincareSet,
            CampingTent,
            PetBed,
            PortableBluetoothSpeaker,
            LuggageSet,
            EssentialOilDiffuser,
            ElectricGrill,
            HomeSecurityCamera,
            Watch,
            InstantCamera,
            CampingStove,
            Drone
        };
        #endregion

        #region Discounts

        private static Discount[] discounts = new Discount[]
        {
            new Discount { Product = ElectricGrill, CreatedOn = DateTime.UtcNow.AddDays(-7), ExpiredOn = null, Percentage = 25, DurationInDays = 14, Status = discountStatuses.First(x => x.Name == DiscountStatuses.Active.ToString()) },
            new Discount { Product = Watch, CreatedOn = DateTime.UtcNow.AddDays(-7), ExpiredOn = null, Percentage = 15, DurationInDays = 21, Status = discountStatuses.First(x => x.Name == DiscountStatuses.Active.ToString()) },
            new Discount { Product = WaterBottle, CreatedOn = DateTime.UtcNow.AddDays(-7), ExpiredOn = null, Percentage = 10, DurationInDays = 31, Status = discountStatuses.First(x => x.Name == DiscountStatuses.Active.ToString()) },
            new Discount { Product = YogaMat, CreatedOn = DateTime.UtcNow.AddDays(-7), ExpiredOn = null, Percentage = 50, DurationInDays = 14, Status = discountStatuses.First(x => x.Name == DiscountStatuses.Active.ToString()) },
            new Discount { Product = LEDDeskLamp, CreatedOn = DateTime.UtcNow.AddDays(-7), ExpiredOn = null, Percentage = 33, DurationInDays = 14, Status = discountStatuses.First(x => x.Name == DiscountStatuses.Active.ToString()) },
        };

        #endregion

        #region Orders

        private static Order[] orders = new Order[]
        {
            new Order
            {
                Customer = customers.First(x => x.UserId == "3c5d6b41-b89e-4368-b100-513d649f54af"),
                Status = orderStatuses.First(x => x.Name == OrderStatuses.Active.ToString()),
                Details = new List<OrderDetails>()
                {
                    new OrderDetails()
                    {
                        UpdatedOn = DateTime.UtcNow.AddDays(-14),
                        Status = orderDetailsStatuses.First(x => x.Name == OrderDetailsStatuses.Inactive.ToString()),
                        Stage = orderDetailsStages.First(x => x.Name == OrderDetailsStages.Shipped.ToString()),
                    },
                    new OrderDetails()
                    {
                        UpdatedOn = DateTime.UtcNow.AddDays(-13),
                        Status = orderDetailsStatuses.First(x => x.Name == OrderDetailsStatuses.Active.ToString()),
                        Stage = orderDetailsStages.First(x => x.Name == OrderDetailsStages.Cancelled.ToString()),
                    }
                },
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Product = ElectricGrill,
                        Quantity = 1,
                        UnitPrice = ElectricGrill.Price * (discounts.First(x => x.Product == ElectricGrill && x.Status.Name == DiscountStatuses.Active.ToString()).Percentage) / 100
                    }
                }
            },
            new Order
            {
                Customer = customers.First(x => x.UserId == "3c5d6b41-b89e-4368-b100-513d649f54af"),
                Status = orderStatuses.First(x => x.Name == OrderStatuses.Active.ToString()),
                Details = new List<OrderDetails>()
                {
                    new OrderDetails()
                    {
                        UpdatedOn = DateTime.UtcNow.AddDays(-10),
                        Status = orderDetailsStatuses.First(x => x.Name == OrderDetailsStatuses.Inactive.ToString()),
                        Stage = orderDetailsStages.First(x => x.Name == OrderDetailsStages.Shipped.ToString()),
                    },
                    new OrderDetails()
                    {
                        UpdatedOn = DateTime.UtcNow.AddDays(-6),
                        Status = orderDetailsStatuses.First(x => x.Name == OrderDetailsStatuses.Active.ToString()),
                        Stage = orderDetailsStages.First(x => x.Name == OrderDetailsStages.Completed.ToString()),
                    },
                },
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Product = Watch,
                        Quantity = 1,
                        UnitPrice = Watch.Price * (discounts.First(x => x.Product == Watch && x.Status.Name == DiscountStatuses.Active.ToString()).Percentage) / 100
                    }
                }
            },
            new Order
            {
                Customer = customers.First(x => x.UserId == "3c5d6b41-b89e-4368-b100-513d649f54af"),
                Status = orderStatuses.First(x => x.Name == OrderStatuses.Active.ToString()),
                Details = new List<OrderDetails>()
                {
                    new OrderDetails()
                    {
                        UpdatedOn = DateTime.UtcNow.AddDays(-8),
                        Status = orderDetailsStatuses.First(x => x.Name == OrderDetailsStatuses.Inactive.ToString()),
                        Stage = orderDetailsStages.First(x => x.Name == OrderDetailsStages.Shipped.ToString()),
                    },
                    new OrderDetails()
                    {
                        UpdatedOn = DateTime.UtcNow.AddDays(-2),
                        Status = orderDetailsStatuses.First(x => x.Name == OrderDetailsStatuses.Active.ToString()),
                        Stage = orderDetailsStages.First(x => x.Name == OrderDetailsStages.Returned.ToString()),
                    },
                },
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Product = HomeSecurityCamera,
                        Quantity = 1,
                        UnitPrice = HomeSecurityCamera.Price
                    }
                }
            }
        };

        #endregion

        public SeedingService(WebShopDbContext context)
        {
            this.context = context;
        }

        ///<inheritdoc/>
        public async Task<OperationResult> SeedAsync()
        {
            var operationResult = new OperationResult();
            if (await CheckAlreadySeededAsync())
            {
                operationResult.AppendError(new Error(ErrorTypes.InternalServerError, "Database is already seeded!"));
                return operationResult;
            }

            await context.AddRangeAsync(addressStatuses);
            await context.AddRangeAsync(discountStatuses);
            await context.AddRangeAsync(emailStatuses);
            await context.AddRangeAsync(orderStatuses);
            await context.AddRangeAsync(orderDetailsStages);
            await context.AddRangeAsync(orderDetailsStatuses);
            await context.AddRangeAsync(phoneNumberStatuses);
            await context.AddRangeAsync(inventoryTransactionTypes);
            await context.AddRangeAsync(productCategories);
            await context.AddRangeAsync(countries);

            await context.AddRangeAsync(roles);

            await context.AddRangeAsync(users);
            await context.AddRangeAsync(usersRoles);

            await context.AddRangeAsync(customers);
            await context.AddRangeAsync(addresses);
            await context.AddRangeAsync(phoneNumbers);
            await context.AddRangeAsync(emails);

            await context.AddRangeAsync(products);

            await context.AddRangeAsync(discounts);

            await context.AddRangeAsync(orders);

            await context.SaveChangesAsync();

            return operationResult;
        }

        private async Task<bool> CheckAlreadySeededAsync()
        {
            return await context.Countries.AnyAsync();
        }
    }
}
