using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Shared.Response
{
    public class PaginationResponse<T> : BaseResponse
    {
        public ICollection<T>? Data { get; set; }
        public int TotalPages { get; set; }
    }
}
