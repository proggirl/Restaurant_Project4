using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Project4
{
    public class Cook    
    {
    public delegate void Processed(TableRequests r); 
      
    public event Processed ProcessedEvent; 
   
    public void Process(TableRequests requests)
        {
            var chickens = requests.Get<Chicken>();
            foreach (var ch in chickens)
            {
                ch.CutUp();
                ch.Cook();                
            }

            var eggs = requests.Get<Egg>();
            foreach (var e in eggs)
            {
                e.Crack();
                e.DiscardShells();
                e.Cook();                
            }
            ProcessedEvent(requests); 
        }
    }
}
