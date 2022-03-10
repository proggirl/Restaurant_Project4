using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant_Project4
{
    public class Server
    {
        public TableRequests requests = new TableRequests();
        Cook cook = new Cook();

        public delegate (List<string>, string)? Ready(TableRequests r);
        public event Ready ReadyEvent;

        public (List<string>, string)? OnReadyEvent()
        {
           return ReadyEvent?.Invoke(requests);
        }
        public Server()
        {
            ReadyEvent += cook.Process;
            cook.ProcessedEvent += ServeAll;
        }
        public void Receive(string customerName, int quantityChiken, int quantityEgg, string drink)
        {
            for (int i = 0; i < quantityChiken; i++)
            {
                requests.Add<Chicken>(customerName);
            }
            for (int i = 0; i < quantityEgg; i++)
            {
                requests.Add<Egg>(customerName);
            }
            GetMenuDrink(drink, customerName);
        }

        public (List<string>, string) Send()
        {
            List<string> result = new List<string>();
           
            foreach (var e in requests)
            {
                var customer = e.ToString();
                string text = "Customer " + customer + " is served Drink ";

                var customerOrders = requests.GetMenuItemsCustomer(customer);

                var d = customerOrders.Where(x =>
                x.GetType() == typeof(RSCola) ||
                x.GetType() == typeof(Lemonad) ||
                x.GetType() == typeof(Tea) ||
                x.GetType() == typeof(Coca_Cola) ||
                x.GetType() == typeof(NotDrink)
                ).FirstOrDefault();
                string drink = (d != null) ? d.GetType().Name : " ";

                if (!string.IsNullOrEmpty(drink))
                {
                    text += ", " + drink;
                }
                result.Add(text);
            }

            if (result.Any())
            {
                result.Add("Please enjoy your drink!");
            }
            else
            {
                result.Add("Please order any drink!");
            }
            result.Add("Orders sent to Cook ");
            result.Add("Please wait ...");
            result.Add("Orders are cooked!!!");
            var (ls, q) = OnReadyEvent().Value;

            result.AddRange(ls);
            requests = new TableRequests();
            return (result, q);
        }

        public (List<string>, string) ServeAll(TableRequests requests)
        {
            List<string> result = new List<string>();

            foreach (var e in requests)
            {
                var customer = e.ToString();
                string text = "Customer " + customer + " is served ";

                var customerOrders = requests.GetMenuItemsCustomer(customer);

                int chickenCount = customerOrders.Where(x => x.GetType() == typeof(Chicken)).Count();
                int eggCount = customerOrders.Where(x => x.GetType() == typeof(Egg)).Count();

                if (chickenCount > 0)
                {
                    text += " Chicken " + chickenCount;
                }
                if (eggCount > 0)
                {
                    text += ", Egg " + eggCount;
                }

                result.Add(text);
            }

            requests = new TableRequests();
            if (result.Any())
            {
                result.Add("Please enjoy your meal!");
            }
            else
            {
                result.Add("Please send all orders to cooker!");
            }
            return (result, Egg.quality.ToString());
        }

        private void GetMenuDrink(string str, string customerName)
        {
            switch (str)
            {
                case "RC Cola":
                    requests.Add<RSCola>(customerName);
                    return;
                case "Lemonad":
                    requests.Add<Lemonad>(customerName);
                    return;
                case "Tea":
                    requests.Add<Tea>(customerName);
                    return;
                case "Coca-Cola":
                    requests.Add<Coca_Cola>(customerName);
                    return;
            }
            requests.Add<NotDrink>(customerName);
        }
    }
}