using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectListCategories()
        {
            return  _context.Categories.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).AsNoTracking().OrderBy(c => c.Text).ToList();
        }
    }
}
