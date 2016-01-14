using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Optimization;
using AutogearWeb.EFModels;
using AutogearWeb.Models;

namespace AutogearWeb.Repositories
{
    public class PostalRepo : IPostalRepo
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public AutogearDBEntities DataContext { get; set; }

        public PostalRepo()
        {
            DataContext = new AutogearDBEntities();
        }

        private IQueryable<TblPostCodeModel> _tblPostCodeModels;
        private IQueryable<TblSuburbModel> _tblSuburbModels;
        private IQueryable<TblStateModel> _tblStateModels;

        public IQueryable<TblPostCodeModel> TblPostCodeModels
        {
            get
            {
                _tblPostCodeModels = _tblPostCodeModels ?? DataContext.PostCodes.Select(s => new TblPostCodeModel {PostCodeId = s.PostCode1, SuburbId = s.SuburbID});
                return _tblPostCodeModels;
            }
            set { _tblPostCodeModels = value; }
        }
        
        public IQueryable<TblSuburbModel> TblSuburbModels
        {
            get
            {
                _tblSuburbModels = _tblSuburbModels ??
                                   DataContext.Suburbs.Select(
                                       s =>
                                           new TblSuburbModel
                                           {
                                               Name = s.Suburb_Name,
                                               SuburbId = s.SuburbId,
                                               StateId = s.StateId
                                           });
                return _tblSuburbModels;
            }
            set { _tblSuburbModels = value; }
        }

        public IQueryable<TblStateModel> TblStateModels
        {
            get
            {
                _tblStateModels = _tblStateModels ??
                                  DataContext.States.Select(
                                      s => new TblStateModel {Name = s.State_Name, StateId = s.StateId});
                return _tblStateModels;
            }
            set { _tblStateModels = value; }
        }

        public void SaveState(State repo)
        {
            if (repo != null)
                DataContext.States.Add(repo);
        }

        public void SaveSubUrb(Suburb repo)
        {
            if (repo != null)
                DataContext.Suburbs.Add(repo);
        }

        public void SavePostCode(PostCode repo)
        {
            if (repo != null)
                DataContext.PostCodes.Add(repo);
            
        }

        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }

        public PostCode GetPostCode(int postalId, int subUrbId)
        {
            return
                 DataContext.PostCodes.SingleOrDefault(s => s.PostCode1 == postalId && s.SuburbID == subUrbId);
        }

        public Suburb GetSuburb(string subUrbName)
        {
            return  DataContext.Suburbs.SingleOrDefault(s => s.Suburb_Name.ToLower() == subUrbName.ToLower());
        }
        public TblSuburbModel GetSuburbById(int subUrbId)
        {
            return TblSuburbModels.SingleOrDefault(s => s.SuburbId == subUrbId);
        }

        public State GetState(string stateName)
        {
            return  DataContext.States.SingleOrDefault(s => s.State_Name.ToLower() == stateName.ToLower());
        }

        public async Task<IList<int>> GetPostcodes(string suburbName)
        {
            if (string.IsNullOrEmpty(suburbName))
                return await TblPostCodeModels.Select(s => s.PostCodeId).ToListAsync();
            var subUrb = TblSuburbModels.SingleOrDefault(s => String.Equals(s.Name, suburbName, StringComparison.CurrentCultureIgnoreCase));
            if (subUrb != null)
                return await 
                    TblPostCodeModels.Where(s => s.SuburbId == subUrb.SuburbId)
                        .Select(s => s.PostCodeId).Distinct()
                        .ToListAsync();
            return await TblPostCodeModels.Select(s => s.PostCodeId).ToListAsync();
        }

        public IList<string> GetSuburbNames()
        {
            var names = new List<string>();
            var suburbName = "";
            var suburbId = 0;
            int stateId = 0;
            var stateName = "";
            var postCodeModels = TblPostCodeModels.ToList();
            var subUrbModels = TblSuburbModels.ToList();
            var stateModels = TblStateModels.ToList();
            foreach (var post in postCodeModels)
            {
                if (suburbId != post.SuburbId)
                {
                    suburbId = post.SuburbId;
                    var suburb = subUrbModels.FirstOrDefault(s => s.SuburbId == post.SuburbId);
                    if (suburb != null)
                    {
                        suburbName = suburb.Name;
                        if (stateId != suburb.StateId)
                        {
                            var state = stateModels.FirstOrDefault(s => s.StateId == suburb.StateId);
                            if (state != null)
                                stateName = state.Name;
                        }
                    }
                }
                names.Add(suburbName + "," + stateName + "," + post.PostCodeId);

            }

            return  names.ToList();
        }

        public TblStateModel GetStateById(int stateId)
        {
            return TblStateModels.FirstOrDefault(s => s.StateId == stateId);
        }
        public async Task<IList<TblPostCodeSuburbModel>> GetPostCodeWithSuburbs()
        {
            var postCodeSuburbModel = new List<TblPostCodeSuburbModel>();
            foreach (var postcode in TblPostCodeModels)
            {
                var suburb = TblSuburbModels.SingleOrDefault(s => s.SuburbId == postcode.SuburbId);
                postCodeSuburbModel.Add(new TblPostCodeSuburbModel
                {
                    PostCodeId = postcode.PostCodeId,
                    SuburbName = (suburb == null )? "":suburb.Name
                });
            }
            return await Task.Run(() => postCodeSuburbModel);
        }
    }
}