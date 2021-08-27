using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_First_Version.Interfaces
{
    interface IGeneral<T>
    {
        List<T> GetAllData();
        T FindDataByID(int id);
        T FindDataByName(string Name);
        void AddItem(T Item);
        void DeleteItem(int id);
        void UpdateItem(T UpdatedData);
    }
}
