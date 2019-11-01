using BLL.Base;
using Model;
using Model.ViewModels.SafetyItem;
using Repository.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLSafetyItem : BLBase
    {

        public VmSafetyItem GetSafetyItem(int id)
        {
            try
            {
                var safetyItemRepository = UnitOfWork.GetRepository<SafetyItemRepository>();

                var safetyItem = safetyItemRepository.GetSafetyItemById(id);

                var vmSafetyItemList = new VmSafetyItem
                {
                    Id = safetyItem.Id,
                    Name = safetyItem.Name,
                    Instruction = safetyItem.Instruction,
                    Priority = safetyItem.Priority,
                };

                return vmSafetyItemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<VmSafetyItem> GetAllSafetyItems()
        {
            try
            {
                var safetyItemRepository = UnitOfWork.GetRepository<SafetyItemRepository>();

                var safetyItemList = safetyItemRepository.GetAllSafetyItems();

                var vmSafetyItemList = from si in safetyItemList
                                       select new VmSafetyItem
                                       {
                                           Id = si.Id,
                                           Name = si.Name,
                                           Instruction = si.Instruction,
                                           Priority = si.Priority,
                                       };

                return vmSafetyItemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CreateSafetyItem(VmSafetyItem vmSafetyItem)
        {
            try
            {

                var safetyItemRepository = UnitOfWork.GetRepository<SafetyItemRepository>();

                safetyItemRepository.CreateSafetyItem(
                    new SafetyItem
                    {
                        Name = vmSafetyItem.Name,
                        Instruction = vmSafetyItem.Instruction,
                        Priority = vmSafetyItem.Priority,
                    });

                UnitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateSafetyItem(VmSafetyItem vmSafetyItem)
        {
            try
            {
                var safetyItemRepository = UnitOfWork.GetRepository<SafetyItemRepository>();

                var safetyItem = new SafetyItem
                {
                    Id = vmSafetyItem.Id,
                    Name = vmSafetyItem.Name,
                    Instruction = vmSafetyItem.Instruction,
                    Priority = vmSafetyItem.Priority,
                };

                safetyItemRepository.UpdateSafetyItem(safetyItem);

                UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteSafetyItem(int id)
        {
            try
            {
                var safetyItemRepository = UnitOfWork.GetRepository<SafetyItemRepository>();
                safetyItemRepository.DeleteSafetyItem(id);

                UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}