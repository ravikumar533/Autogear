﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace AutogearWeb.Repositories
{
    public class AutogearRepo : IAutogearRepo
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        public enum Roles
        {
            Administrator,
            SystemAdministrator
        } 
        public IList<SelectListItem> GenderListItems()
        {
            string[] list = { "", "Male", "Female" };
            var genderList = new List<SelectListItem>();
            var i = 0;
            foreach (var gender in list)
            {
                genderList.Add(new SelectListItem { Value = i.ToString(), Text = gender });
                i++;
            }
            return genderList;
        }
    }
}