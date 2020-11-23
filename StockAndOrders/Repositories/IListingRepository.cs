using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAndOrders.Repositories
{
    public interface IListingRepository
    {
        Task<IList> GetListings();
    }
}
