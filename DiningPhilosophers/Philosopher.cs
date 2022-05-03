using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    class Philosopher
    {
        public string Name { get; set; }
        public int[] Forks { get; set; }

        public Philosopher()
        {
            Eat();
            Think();
        }

       
        public void Eat()
        {
            var random = new Random();
            int duration = random.Next(300, 700);


        }

        public void Think()
        {

        }

    }
}
