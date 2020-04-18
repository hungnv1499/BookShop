using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Helpers
{
    public enum AjaxStatusEnum
    {
        Success = 1,
        Error = 2,
        Fail = 3,
    }
    public enum OrderStatusEnum
    {
        New,
        Delivery,
        Finish,
        Deleted
    }
}
