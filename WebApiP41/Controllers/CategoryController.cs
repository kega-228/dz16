using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiP41.Services;
using WebApiP41.Models;
using WebApiP41.Models.DTO;

namespace WebApiP41.Controllers;

[ApiController]
[Route("api/categories")]

public class CategoryController(CategoryService service, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), 200)]
    public async Task<IActionResult> GetPagedCategories([FromQuery] PageRequest<CategoryFilter> pageRequest)
    {
        var result = await service.GetPagedCategoriesAsync(pageRequest);
        return Ok(result);
    }
    

    [HttpPost]
    [ProducesResponseType(typeof(Category), 200)]
    public async Task<IActionResult> AddNewCategory([FromBody] CreateCategory category)
    {
        var newCategory = mapper.Map<Category>(category);
        var addedCategory = await service.AddCategoryAsync(newCategory);
        return Ok(addedCategory);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Category), 200)]
    [ProducesResponseType(404, Description = "Category not found")]
    public async Task<IActionResult> ChangeCategory([FromRoute] int id, [FromBody] string name)
    {
        var category = await service.ChangeCategoryAsync(id, name);
        if (category == null) return NotFound();
        return Ok(category);
        
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(Category), 200)]
    [ProducesResponseType(404, Description = "Category not found")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        var category = await service.DeleteCategoryAsync(id);
        if (category == false) return NotFound();
        return Ok(category);
    }
}