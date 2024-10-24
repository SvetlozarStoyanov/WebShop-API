using Contracts.DataAccess.Repositories.Common.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Statuses;

namespace DataAccess.Repositories.Common.Statuses
{
    public class EmailStatusRepository : BaseRepository<long, EmailStatus>, IEmailStatusRepository
    {
        public EmailStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
