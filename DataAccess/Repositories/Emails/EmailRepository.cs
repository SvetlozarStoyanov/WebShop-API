using Contracts.Repositories.Emails;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Emails;

namespace DataAccess.Repositories.Emails
{
    public class EmailRepository : BaseRepository<long, Email>, IEmailRepository
    {
        public EmailRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
