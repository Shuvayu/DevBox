using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Services
{
    public class MedicinesServices : BaseService
    {
         public MedicinesServices()
            : base()
        {
        }

         public MedicinesServices(EnetContext context)
            : base(context)
        {
        }

         public virtual ICollection<Medicine> GetAll()
         {
             return Context.Medicines.ToList();

         }

         public virtual int Add(Medicine medicines)
        {
            Context.Medicines.Add(medicines);
            Context.SaveChanges();
            return medicines.MedicineId;
        }

         public virtual int Delete(int id)
         {
             var medicine = Context.Medicines.First(x => x.MedicineId == id);
            Context.Medicines.Remove(medicine);
            Context.SaveChanges();
            return id;

        }

         public virtual int Update(Medicine Medicines)
        {
            AutoMapper.Mapper.CreateMap<Medicine, Medicine>();
            var Medicinetr =
                Context.Medicines.First(
                    x => x.MedicineId == Medicines.MedicineId);
            var mapped = AutoMapper.Mapper.Map<Medicine>(Medicinetr);
            Context.SaveChanges();
            return mapped.MedicineId;
        }

         public virtual Medicine Details(int Id)
        {
            return Context.Medicines.First(x => x.MedicineId == Id);

        }

         public virtual ICollection<Medicine> Medicines()
        {
            return Context.Medicines.ToList();
        }

    
    }
}
