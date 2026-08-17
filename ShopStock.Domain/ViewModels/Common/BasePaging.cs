using Microsoft.EntityFrameworkCore;
namespace ShopStock.Domain.ViewModels.Common
{
    public class BasePaging<T>
    {
        public BasePaging()
        {
            PageId = 1;
            TakeEntity = 20;
            HowManyPageAfterAndBefore = 1;
            Entities = new List<T>();
        }
        public int PageId { get; set; }
        public int PageCount { get; set; }
        public int AllEntitiesCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TakeEntity { get; set; }
        public int SkipEntity { get; set; }
        public int HowManyPageAfterAndBefore { get; set; }
        public List<T> Entities { get; set; }

        public async Task<BasePaging<T>> Paging(IQueryable<T> query)
        {
            var allEntitiesCount = await query.CountAsync();
            var pageCount = (int)Math.Ceiling(allEntitiesCount / (double)TakeEntity);
            PageId = pageCount < 1 ? PageId : PageId > pageCount ? pageCount : PageId;
            AllEntitiesCount = allEntitiesCount;

            SkipEntity = (PageId - 1) * TakeEntity;

            PageCount = pageCount;
            StartPage = PageId - HowManyPageAfterAndBefore <= 0 ? 1 : PageId - HowManyPageAfterAndBefore;
            EndPage = PageId + HowManyPageAfterAndBefore > PageCount ? PageCount : PageId + HowManyPageAfterAndBefore;

            Entities = await query.Skip(SkipEntity).Take(TakeEntity).ToListAsync();
            return this;
        }


        public PagingViewModel GetCurrentPaging()
        {
            return new PagingViewModel()
            {
                PageId = this.PageId,
                StartPage = this.StartPage,
                EndPage = this.EndPage,
                TakeEntity = this.TakeEntity
            };
        }
    }
    public class PagingViewModel
    {
        public int PageId { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TakeEntity { get; set; }

    }
}