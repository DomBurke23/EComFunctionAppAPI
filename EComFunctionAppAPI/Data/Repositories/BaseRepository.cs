using EComFunctionAppAPI.Options;
using Microsoft.Extensions.Options;

namespace EComFunctionAppAPI.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly DbOptions _dbOptions;

        public BaseRepository(IOptions<DbOptions> dbOptions)
        {
            _dbOptions = dbOptions.Value;
        }
    }
}
