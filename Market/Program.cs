using System;

namespace Market
{
    public class customer
    {
        protected string Name;
        protected double price;
        protected Product[] cart= new Product [50];
        private int index;
        public void addTocart(Product item,double q)
        {
            cart[index++] = item;
            price += item.p_price*q;
        }
        public void removeFromcart(uint itemId, double q)
        {
            double price=0;
            for (int i = 0; i < cart.Length; i++)
            {
                if(cart[i].p_id == itemId)
                {
                    price = cart[i].p_price*q;
                    cart[i] = cart[--index];
                }
            }
            price -= price;
        }
        public customer()
        {
            index = 0;
            Name = "Default Name";
            price = 0;
        }
        public customer(string Name,int price)
        {
            this.Name = Name;
            this.price = price;
            index = 0;
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
            return price*discount;
        } 
    }
    public class cashCustomer : customer
    {
        public cashCustomer(string Name, int price)
        {
            this.Name = Name;
            this.price = price;
        }
        public double totalPaid()
        {
            return price;
        }
    }
    public class Product {
		public static uint id;
		public uint p_id = id; 

		public string p_name;
		public double p_price;
		public double p_quantity;

		public Product (string name, double price, int quantity) {
			this.p_name = name;
			this.p_price = price;
			this.p_quantity = quantity;
			Product.id++;

		}
	}
    internal class Program
    {
        static void Main(string[] args)
        {
            Product[] Products = new Product[] {
				new Product("Tea", 10, 100),
				new Product("Milk", 20.25, 100),
				new Product("Coffee", 15.21, 100),
				new Product("Suger", 12.33, 100),
				new Product("Salt", 5, 100),
				new Product("Meat", 100, 100),
				new Product("Chicken", 35.30, 100),
				new Product("Tomato", 12.25, 100),
				new Product("Potato", 20, 100),
				new Product("Water", 5.33, 100),
				new Product("Soda", 7.65, 100),
				new Product("Candy", 14, 100),
				new Product("Fish", 30, 100),
				new Product("Bread", 2, 100),
			};
			Console.WriteLine("ID\tName\t\tPrice\tQuantity");
			foreach (Product p in Products) {
				Console.Write("{0}",p.p_id);
				Console.Write("\t{0}",p.p_name);
				Console.Write("\t\t{0}",p.p_price);
				Console.Write("\t{0}",p.p_quantity);
				Console.WriteLine();
			}
        }
    }
}
