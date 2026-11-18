using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApiP41.Models;
using WebApiP41.Models.DTO;

namespace WebApiP41.Services;

public class CategoryService(ApiDbContext context)
{
    public async Task<PageResult<Category>> GetPagedCategoriesAsync(PageRequest<CategoryFilter> pageRequest)
    {

        Func<IQueryable<Category>, IQueryable<Category>> orderBy = (q) =>
        {
            return pageRequest.OrderBy switch
            {
                "Id" => q.OrderBy(p => p.Id),
                "-Id" => q.OrderByDescending(p => p.Id),
                "Name" => q.OrderBy(p => p.Name),
                "-Name" => q.OrderByDescending(p => p.Name),
                _ => q.OrderBy(p => p.Id)
            };

        };

        Func<IQueryable<Category>, IQueryable<Category>> filter = (q) =>
        {
            if (pageRequest.Filter != null)
            {
                if (!string.IsNullOrEmpty(pageRequest.Filter.Name))
                {
                    q = q.Where(p => p.Name.Contains(pageRequest.Filter.Name));
                }

                if (pageRequest.Filter.Id.HasValue)
                {
                    q = q.Where(p => p.Id == pageRequest.Filter.Id);
                }
            }
            return q;
        };

        Func<IQueryable<Category>, IQueryable<Category>> paginate = (q) =>
        {
            return q.Skip((pageRequest.Page - 1) * pageRequest.PageSize).Take(pageRequest.PageSize);
        };
        var query = context.Categories.AsQueryable();
        query = filter(query);
        query = orderBy(query);
        
        var totalCount = await query.CountAsync();

        query = paginate(query);
        var categories = await query.ToListAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageRequest.PageSize);

        var result = new PageResult<Category>
        {
            TotalItems = totalCount,
            TotalPages = totalPages,
            CurrentPage = pageRequest.Page,
            PageSize = pageRequest.PageSize,
            Items = categories
        };
        return result;

    }
    public async Task<ICollection<Category>> GetAllAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category> AddCategoryAsync(Category newCategory)
    {
        newCategory.Id = context.Categories.Any() ? context.Categories.Max(p => p.Id) + 1 : 1;
        newCategory.CreatedAt = DateTime.Now;
        newCategory.UpdatedAt = DateTime.Now;
        context.Categories.Add(newCategory);
        await context.SaveChangesAsync();
        return newCategory;
    }

    public async Task<Category> ChangeCategoryAsync(int id, string name)
    {
        var category = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);
        if (category == null)
        {
            return null;
        }
        category.Name = name;
        category.UpdatedAt = DateTime.Now;
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);
        if (category == null)
        {
            return false;
        }
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return true;
    }
}