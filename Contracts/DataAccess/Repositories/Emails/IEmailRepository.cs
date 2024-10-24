using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Emails;

namespace Contracts.DataAccess.Repositories.Emails
{
    public interface IEmailRepository : IBaseRepository<long, Email>
    {
    }
}
