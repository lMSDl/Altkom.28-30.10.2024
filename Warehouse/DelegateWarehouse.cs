using Models;
using Services.InMemory;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    internal class DelegateWarehouse<T> : GenericWarehouse<T> where T : Entity
    {
        private Func<T> _createNewItem;
        private Func<T, T> _createUpdatedItem;
        private Func<T, string> _getItemInfo;

        public DelegateWarehouse(Func<T> createNewItem, Func<T, T> createUpdatedItem, Func<T, string> getItemInfo, IEntityService<T> service) : base(service)
        {
            _createNewItem = createNewItem;
            _createUpdatedItem = createUpdatedItem;
            _getItemInfo = getItemInfo;
        }


        protected override T CreateNewItem()
        {
            return _createNewItem.Invoke();
        }

        protected override T CreateUpdatedItem(T old)
        {
            return _createUpdatedItem.Invoke(old);
        }

        protected override string GetItemInfo(T items)
        {
            return _getItemInfo.Invoke(items);
        }
    }
}
