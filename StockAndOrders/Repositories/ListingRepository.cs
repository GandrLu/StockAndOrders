using StockAndOrders.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAndOrders.Repositories
{
    public class ListingRepository : IListingRepository
    {
        public async Task<IList> GetListings()
        {
            var listingsList = new List<Listing>();
            return listingsList;
        }
    }
}
