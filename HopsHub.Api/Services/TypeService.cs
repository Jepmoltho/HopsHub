using HopsHub.Api.DTOs;
using HopsHub.Api.Exceptions;
using HopsHub.Api.Helpers;
using HopsHub.Api.Services.Interfaces;


namespace HopsHub.Api.Services;

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

    public async Task<Models.Type> PostType(TypeDTO typeDTO)
    {
        var exist = await _typeRepository.ExistAsync(type => type.Name == typeDTO.Name);

        if (exist)
        {
            throw new EntityExistsException($"Type with name {typeDTO.Name} already exist");
        }

        var type = new Models.Type
        {
            Name = typeDTO.Name,
            ShortName = typeDTO.ShortName
        };

        await _typeRepository.AddAsync(type);

        await _typeRepository.SaveAsync();

        return type;
    }

    public async Task<Models.Type> PutType(UpdateTypeDTO updateTypeDTO) 
    {
        var type = await _typeRepository.GetByIdAsync(updateTypeDTO.Id);

        if (type == null)
        {
            throw new EntityNotFoundException($"Type with {updateTypeDTO.Id} not found in database");
        }

        UpdateHelper.UpdateEntity(updateTypeDTO, type);

        _typeRepository.Update(type);

        await _typeRepository.SaveAsync();

        return type;
    }

    public async Task<Models.Type> DeleteType(DeleteTypeDTO typeDTO)
    {
        var type = await _typeRepository.GetByIdAsync(typeDTO.Id);

        if (type == null)
        {
            throw new EntityNotFoundException($"Type {typeDTO.Id} not found in database");
        }

        UpdateHelper.UpdateEntity(typeDTO, type);

        _typeRepository.Update(type);
        await _typeRepository.SaveAsync();

        return type;
    }
}
