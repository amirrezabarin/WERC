using Repository.EF.Base;
using System.Linq;
using Model;
using System.Collections.Generic;
using System;

namespace Repository.EF.Repository
{
    public class GradeDetailRepository : EFBaseRepository<GradeDetail>
    {
        public void CreateGradeDetail(GradeDetail gradeDetail)
        {
            Add(gradeDetail);
        }
        public IEnumerable<GradeDetail> GetGradeDetails(int gradeId)
        {
            return (from s in Context.GradeDetails where s.GradeId == gradeId select s).ToArray();

        }
        public void DeleteDetailsGrade(int id)
        {
            var deletableGradeDetail = Context.GradeDetails.Find(id);

            Delete(deletableGradeDetail);

        }
        public void DeleteGradeDetailsByGrade(int gradeId)
        {
            var deleteList =  from s in Context.GradeDetails where s.GradeId == gradeId select s;
            foreach (var item in deleteList)
            {
                Delete(item);
            }

        }
 }
}
