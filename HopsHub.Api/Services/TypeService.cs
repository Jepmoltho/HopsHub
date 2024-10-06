using HopsHub.Api.Services.Interfaces;


namespace HopsHub.Api.Services
{
    public class TypeService : ITypeService
    {
        private readonly IRepository<Models.Type> _typeRepository;

        public TypeService(IRepository<Models.Type> typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<List<Models.Type>> GetTypes()
        {
            return await _typeRepository.GetAllAsync();
        }
    }
}

