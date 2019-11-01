using System.Collections.Generic;
using Repository.EF.Base;
using System.Linq;
using Model;
using System;

namespace Repository.EF.Repository
{
    public class UniversityRepository : EFBaseRepository<University>
    {
        public IEnumerable<University> Select(int index, int count)
        {
            var UniversityList = from university in Context.Universities
                                 select university;

            return UniversityList.OrderBy(A => A.Name).Skip(index).Take(count).ToArray();
        }
        public University GetUniversityById(int id)
        {
            return Context.Universities.Find(id);
        }
        public void UpdateUniversityImage(int universityId, string universityPictureUrl)
        {
            var oldPerson = (from s in Context.Universities where s.Id == universityId select s).FirstOrDefault();

            oldPerson.UniversityPictureUrl = universityPictureUrl;

            Update(oldPerson);
        }
    }
}
