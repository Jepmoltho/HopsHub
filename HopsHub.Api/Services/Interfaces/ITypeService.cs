using HopsHub.Api.Models;
namespace HopsHub.Api.Services.Interfaces;

public interface ITypeService
{
    Task<List<Models.Type>> GetTypes();
}

