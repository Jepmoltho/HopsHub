using HopsHub.Shared.DTOs;

namespace HopsHub.Frontend.Services.Interfaces;

public interface ITypeService
{
    Task<List<TypeDTO>> GetTypesAsync();

    //Todo: Implement unit testing
}

