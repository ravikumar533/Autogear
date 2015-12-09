﻿using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading.Tasks;
using AutogearWeb.EFModels;
using AutogearWeb.Models;

namespace AutogearWeb.Repositories
{
    public interface IPostalRepo :  IDisposable
    {
        AutogearDBEntities DataContext { get; set; }

        IQueryable<TblPostCodeModel> TblPostCodeModels { get; set; }
        IQueryable<TblSuburbModel> TblSuburbModels { get; set; }
        IQueryable<TblStateModel> TblStateModels { get; set; }
        
        void SaveState(State repo);
        void SaveSubUrb(Suburb repo);
        void SavePostCode(PostCode repo);
        void SaveChanges();
        PostCode GetPostCode(int postalId,int subUrbId);
        Suburb GetSuburb(string subUrbName);
        State GetState(string stateName);
        Task<IList<int>> GetPostcodes(string suburbName);
        Task<IList<string>> GetSuburbNames(int? postCode);
        Task<IList<TblPostCodeSuburbModel>> GetPostCodeWithSuburbs();
    }
}
