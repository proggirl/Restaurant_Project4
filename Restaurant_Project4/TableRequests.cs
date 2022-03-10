using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Restaurant_Project4
{
    public class TableRequests: IEnumerable
    {
        Dictionary<string, List<IItemInterface>> menuItems = new Dictionary<string, List<IItemInterface>>();

        public void Add<T>(string customer) 
        { 
            List<IItemInterface> list = new List<IItemInterface>();
            bool cs = menuItems.TryGetValue(customer,out list);
           
            if (cs)
            {
                menuItems.Remove(customer);
            }
            else
            {
                list = new List<IItemInterface>();
            }
            var item = Activator.CreateInstance(typeof(T)); 
           
            list.Add(item as IItemInterface);

            menuItems.Add(customer, list);
        }
        public List<IItemInterface> this[string i] {get => GetMenuItemsCustomer(i);}
      
        public ICollection<T> Get<T>()

        {
            ICollection<T> list = new Collection<T>();
           
            foreach(var item in menuItems)
            {
                var t = item.Value.Where(x => x.GetType() == typeof(T)).ToList();
              
                foreach (var i in t )
                {
                    list.Add((T)i);
                }
            }
            return list;
        }
       
        public List<IItemInterface> GetMenuItemsCustomer(string customer)
        {
            List<IItemInterface> list = new List<IItemInterface>();
            menuItems.TryGetValue(customer, out list);
            return list;            
        }

        IEnumerator  IEnumerable.GetEnumerator()
        {
            foreach(var e in menuItems)
            {
                yield return e.Key;
            }
        }
    }
    public interface IItemInterface
    {
        void Obtain();
        void Serve();
        Type GetType();
    }

}
