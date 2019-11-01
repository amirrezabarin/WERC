using Repository.EF.Repository;
using System.Collections.Generic;
using System;
using Model;
using System.Linq.Expressions;
using BLL.Base;
using System.Linq;
using Model.ToolsModels.DropDownList;

namespace BLL
{
    public class BLUniversity : BLBase
    {
        public IEnumerable<VmSelectListItem> GetUniversitySelectListItem(int index, int count)
        {
            var UniversityRepository = UnitOfWork.GetRepository<UniversityRepository>();

            var universityList = UniversityRepository.Select(index, count);
            var vmSelectListItem = (from university in universityList
                                    select new VmSelectListItem
                                    {
                                        Value = university.Id.ToString(),
                                        Text = university.Name,
                                    });

            return vmSelectListItem;
        }
        public bool UploadUniversityImage(int universityId, string universityPictureUrl)
        {
            try
            {

                var universityRepository = UnitOfWork.GetRepository<UniversityRepository>();
                universityRepository.UpdateUniversityImage(universityId, universityPictureUrl);

                UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetUniversityPictureUrl(int id)
        {
            var UniversityRepository = UnitOfWork.GetRepository<UniversityRepository>();

            return UniversityRepository.GetUniversityById(id).UniversityPictureUrl;
        }
    }
}
