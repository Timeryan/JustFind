using System;
using System.Collections.Generic;
using System.Linq;

namespace Advertisement.Application.Services.Category.Contracts.GetAllCategory
{
    public class GetAllCategoryResponse
    {
        public IEnumerable<GetAllCategoryResponseItem> Items { get; set; }
    }
}