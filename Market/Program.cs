using System;

namespace Market
{
    public class customer
    {
        protected string Name;
        protected int price;
        protected int[] cart= new int[50];
        private int i;
        public void addTocart(int item)
        {
            cart[i] = item;
        }
        public customer()
        {
            i = 0;
            Name = "Default Name";
            price = 0;
        }
        public customer(string Name,int price)
        {
            this.Name = Name;
            this.price = price;
            i = 0;
        }
    }
    public class visaCustomer : customer
    {
        public double discount;
        public visaCustomer(string Name,int price)
        {
            this.Name = Name;
            this.price = price;
        }
        public double totalPaid()
        {
            return (double)price*discount;
        } 
    }
    public class cashCustomer : customer
    {
        public cashCustomer(string Name, int price)
        {
            this.Name = Name;
            this.price = price;
        }
        public int totalPaid()
        {
            return price;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
