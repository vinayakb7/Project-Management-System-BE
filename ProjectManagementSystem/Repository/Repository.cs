using ProjectManagementSystem.Business;

namespace ProjectManagementSystem.Repository
{
    public class BaseRepository
    {
        private IUnitOfWork internalUnitOfWork;

        private readonly IConfiguration _configuration;
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IUnitOfWork unitOfWork
        {
            get
            {
                internalUnitOfWork ??= new UnitOfWork(_configuration);
                return internalUnitOfWork;
            }
            set
            {
                internalUnitOfWork = value;
            }
        }
    }
}
