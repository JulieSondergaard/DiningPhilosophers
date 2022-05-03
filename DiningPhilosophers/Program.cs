using System;
using System.Threading;

namespace DiningPhilosophers
{
    class Program
    {
        public static object[] fork = new object[5] {new object(), new object(), new object(), new object(), new object()};


        static void Main(string[] args)
        {
            Thread p0 = new Thread(EatPasta);
            Thread p1 = new Thread(EatPasta);
            Thread p2 = new Thread(EatPasta);
            Thread p3 = new Thread(EatPasta);
            Thread p4 = new Thread(EatPasta);

            p0.Name = "0";
            p1.Name = "1";
            p2.Name = "2";
            p3.Name = "3";
            p4.Name = "4";

            p0.Start();
            p1.Start();
            p2.Start();
            p3.Start();
            p4.Start();

            p0.Join();
            p1.Join();
            p2.Join();
            p3.Join();
            p4.Join();
 
        }

      static void EatPasta()
      {
            int id = Convert.ToInt32(Thread.CurrentThread.Name);

            int fork1 = id;
            int fork2 = id + 1;

            if (fork2 == 5)
            {
                fork2 = 0;
            }
            
            object leftFork = fork[fork1];
            object rightFork = fork[fork2];

            while(true)
            {

                
                bool leftForkAvailable = false;
                bool rightForkAvailable = false;

                if (id == 0)
                {
                    Monitor.Enter(leftFork, ref rightForkAvailable);

                    try
                    {
                        if (rightForkAvailable)
                        {
                            Monitor.Enter(rightFork, ref leftForkAvailable);

                            try
                            {
                                if (leftForkAvailable)
                                {
                                    Eat();
                                    Monitor.Exit(leftFork);
                                }
                            } catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            Monitor.Exit(rightFork);
                            Console.WriteLine($"Philosopher {Thread.CurrentThread.Name} has put down his forks.");
                        }

                        Think();
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                } else
                {
                    Monitor.Enter(rightFork, ref leftForkAvailable);

                    try
                    {
                        if (leftForkAvailable)
                        {
                            Monitor.Enter(leftFork, ref rightForkAvailable); 
                                
                             try
                            {
                                if (rightForkAvailable)
                                {
                                    Eat();
                                    Monitor.Exit(rightFork);
                                }
                            } catch (Exception ex) {
                                Console.WriteLine(ex);
                            }
                            Monitor.Exit(leftFork);
                            Console.WriteLine($"Philosopher {Thread.CurrentThread.Name} has put down his forks.");
                        }

                        Think();
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }


            }

       }

        static void Eat()
        {
            Console.WriteLine($"Philosopher {Thread.CurrentThread.Name} is eating.");
            Random eatingTime = new Random();
            Thread.Sleep(eatingTime.Next(2000, 4000));
        }

        static void Think()
        {
            Console.WriteLine($"Philosopher {Thread.CurrentThread.Name} is thinking.");
            Random thinkingTime = new Random();
            Thread.Sleep(thinkingTime.Next(3000, 7000));
        }

    }


}


    

