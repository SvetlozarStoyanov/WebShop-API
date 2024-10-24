using Contracts.Repositories.BaseRepository;
using Database.Entities.Emails;

namespace Contracts.Repositories.Emails
{
    public interface IEmailRepository : IBaseRepository<long, Email>
    {
    }
}
