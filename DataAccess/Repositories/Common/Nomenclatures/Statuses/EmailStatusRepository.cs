using Contracts.DataAccess.Repositories.Common.Nomenclatures.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Nomenclatures.Statuses;

namespace DataAccess.Repositories.Common.Nomenclatures.Statuses
{
    public class EmailStatusRepository : BaseRepository<long, EmailStatus>, IEmailStatusRepository
    {
        public EmailStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
