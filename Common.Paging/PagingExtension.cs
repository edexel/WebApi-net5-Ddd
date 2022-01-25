using Common.Collection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Paging
{
    public static class PagingExtension
    {
        public static async Task<DataCollection<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int take)
        {
            var originalPages = page;
            page--;

            if (page > 0)
                page *= take;

            var result = new DataCollection<T>
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                Total = await query.CountAsync(),
                Page = originalPages
            };


            if (result.Total > 0)
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
        
            return result;
    }

        public static  DataCollection<T> GetPagedAsyncAsIENumerable<T>(this IEnumerable<T> query, int page, int take)
        {
            var originalPages = page;
            page--;

            if (page > 0)
                page *= take;

            var result = new DataCollection<T>
            {
                Items =  query.Skip(page).Take(take).ToList(),
                Total =  query.Count(),
                Page = originalPages
            };


            if (result.Total > 0)
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            
            return  result;
        }
    }
}
