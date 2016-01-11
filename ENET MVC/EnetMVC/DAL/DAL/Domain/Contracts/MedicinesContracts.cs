using System.Collections.Generic;
using DAL.Models;
using DAL.Services;

namespace Domain.Contracts
{
    public class MedicinesContracts 
    {
        public Medicine MedicineModel { get; set; }
        public MedicinesServices MedicinesServices { get; set; }

        public MedicinesContracts()
        {
            MedicineModel = new Medicine();
            MedicinesServices = new MedicinesServices();
        }
        public int Add(Medicine Medicines)
        {
            MedicineModel = (Medicine)Medicines;
            MedicinesServices.Add(MedicineModel);
            return MedicineModel.MedicineId;
        }

        public int Delete(int id)
        {
            MedicinesServices.Delete(id);
            return id;
        }

        public ICollection<Medicine> GetAll()
        {
            return MedicinesServices.Medicines();
        }

        public Medicine Get(int id)
        {
            return MedicinesServices.Details(id);
        }

        public int Update(Medicine Medicines)
        {
            MedicineModel = (Medicine)Medicines;
            MedicinesServices.Update(MedicineModel);
            return MedicineModel.MedicineId;
        }
    }
}
