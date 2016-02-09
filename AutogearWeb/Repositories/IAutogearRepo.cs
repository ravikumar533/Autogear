using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AutogearWeb.Repositories
{
   public interface IAutogearRepo : IDisposable
    {
        IList<SelectListItem> GenderListItems();
       IList<SelectListItem> DrivingTypeItems();
       SelectListItem CancelledItem();
    }
}
