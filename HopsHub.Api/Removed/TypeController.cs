//using Microsoft.AspNetCore.Mvc;
//using HopsHub.Api.Data;
//using Microsoft.EntityFrameworkCore;

//[ApiController]
//[Route("[controller]")]
//public class TypeController : ControllerBase
//{
//    //To do: Implement service layer between controller and context
//    private readonly TypeContext _typeContext;

//    public TypeController(TypeContext typeContext)
//    {
//        _typeContext = typeContext;
//    }

//    [HttpGet]
//    public async Task<List<HopsHub.Api.Models.Type>> GetTypes()
//    {
//        return await _typeContext.Types.ToListAsync();
//    }
//}