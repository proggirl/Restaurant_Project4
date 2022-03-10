using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Project4
{
    public abstract class MenuItem : IItemInterface {

        public MenuItemStatus Status = MenuItemStatus.Requested;
        public virtual void Obtain()
        {
            Status = MenuItemStatus.Obtained;

        }
        public virtual void Serve()
        {
            Status = MenuItemStatus.Served;

        }
    }
    public enum MenuItemStatus
    {
        Requested,
        Obtained,
        Served
    }
}


