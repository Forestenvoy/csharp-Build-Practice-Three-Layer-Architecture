using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Practice.Repository.Persistence;

namespace Practice.Application.Services
{
    public class AdminService
    {
        private readonly PracticeDbContext _dbContext;
        private readonly ILogger<AdminService> _logger;

        public AdminService(PracticeDbContext dbContext, ILogger<AdminService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


    }
}
