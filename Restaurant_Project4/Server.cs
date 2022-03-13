using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant_Project4
{
    public class Server
    {
        public TableRequests requests = new TableRequests();
      

        public delegate void Ready(TableRequests r);
        public event Ready ReadyEvent;

        public Server()
        {
            
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

        public void Send()
        {
            

            ReadyEvent(requests);
            
        }

        public (List<string>, string) ServeAll(TableRequests requests)
        {
            List<string> result = new List<string>();
            List<string> result1 = new List<string>();


            

            foreach (var e in requests)
            {
                var customer = e.ToString();
                string text = "Customer " + customer + " is served ";
                string textDrink = "Customer " + customer + " is served Drink ";

                var customerOrders = requests.GetMenuItemsCustomer(customer);


                string drink ="";
                int eggCount = 0;
                int chickenCount = 0;

                foreach (var item in customerOrders)
                {
                    if (item.GetType() == typeof(Chicken))
                    {
                        chickenCount++;
                    }
                    else if (item.GetType() == typeof(Egg))
                    {
                        eggCount++;
                    }
                    else
                    {
                        drink = item.GetType().Name;
                    }
                }


                if (!string.IsNullOrEmpty(drink))
                {
                    textDrink += ", " + drink;
                }
                result.Add(textDrink);

                if (chickenCount > 0)
                {
                    text += " Chicken " + chickenCount;
                }
                if (eggCount > 0)
                {
                    text += ", Egg " + eggCount;
                }

                result1.Add(text);
            }

            if (result.Any())
            {
                result.Add("Please enjoy your drink!");
            }
            result.AddRange(result1);
            this.requests = new TableRequests();
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