﻿using GameZone.Models;

namespace GameZone.Services
{
    public interface ICategoryService
    {
        IEnumerable<SelectListItem> GetSelectListCategories();
    }
}
