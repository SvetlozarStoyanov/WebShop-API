﻿namespace Contracts.Services.Entity.ApplicationUsers
{
    public interface IApplicationUserService
    {
        Task<bool> IsUserNameTakenAsync(string userName);

        Task<IEnumerable<string>> GetAllUserNamesAsync();
    }
}
