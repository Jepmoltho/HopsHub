using HopsHub.Shared.DTOs;
namespace HopsHub.Api.Services.Interfaces;

public interface ITypeService
{
    Task<List<Models.Type>> GetTypes();

    Task<Models.Type> PostType(TypeDTO typeDTO);

    Task<Models.Type> PutType(UpdateTypeDTO typeDTO);

    Task<Models.Type> DeleteType(DeleteTypeDTO typeDTO);
}

