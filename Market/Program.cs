/*
		Made by:
			Mahmoud Algohary -SWE-
			Nabil   Salah    -SWE-
			Mostafa Mahmoud  -SWE-
 */
using System;
using System.Threading;
using System.Collections.Generic;

namespace Market
{
    public abstract class customer
    {
        protected string Name;
        protected double price;
        protected List<Product> cart = new List<Product>();
		public List<Product> c_cart { get { return cart; } }
        public void addTocart(Product newProduct)
        {
			foreach (Product product in c_cart)
            {
				if(product.p_id == newProduct.p_id)
                {
					Console.WriteLine("  This product already exist in the cart...");
					Console.Write("  Do you to add quantity\nletter (y) to proceed any letter to quit:  ");
					Char num;
					bool numSuccess = char.TryParse(Console.ReadLine(), out num);
					if(numSuccess){
						if (num == 'y')
						{
							addQuantity(newProduct);
							return;
						}else{
							Project.customerGet();
						}
					}else{
						Console.WriteLine("\n\t---> Please Enter a valid input. <---\n");
					}
                }
            }

            while(true){
				Console.Write("Enter Quantity: ");
				int q;
				bool qSuccess = int.TryParse(Console.ReadLine(), out q);
				if (qSuccess){
					Product p = new Product(newProduct);
					p.p_quantity = q;
					p.p_price = newProduct.p_price * q;
					cart.Add(p);
					price += p.p_price;
					newProduct.p_quantity -= q;
					break;
				}else{
					Console.WriteLine("\n\t --->  Please enter a valid decimal <---\n");
				}
			}
        }

		public void addQuantity(Product newProduct)
		{
			Console.Write("Enter Quantity: ");
			int q;
			bool qSuccess = int.TryParse(Console.ReadLine(), out q);
			if(qSuccess){
				newProduct.p_quantity -= q;
				foreach (Product p in cart)
	            {
					if(p.p_id == newProduct.p_id)
	                {
						p.p_quantity += q;
						p.p_price += q * newProduct.p_price;
						price += q* newProduct.p_price;
	                }
	            }
            }else{
            	Console.WriteLine("\n\t---> Please Enter a valid input. <---\n");
            	addQuantity(newProduct);
            }
		}

		public void print_cart()
		{
			Console.WriteLine("-------------------------------------------------");
			Console.WriteLine("ID\tName\t\t\tPrice\tQuantity");
			Console.WriteLine("-------------------------------------------------");
			foreach (Product p in cart)
			{
				Console.Write("{0}", p.p_id);
				Console.Write("\t{0}", p.p_name);
				Console.Write("\t\t\t{0}", p.p_price);
				Console.Write("\t  {0}", p.p_quantity);
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		public void removeFromcart(int itemId,List<Product> pro)
        {
			int q = 0;
            if(cart.Count == 0) return;
            foreach (Product p in cart)
            {
				if(p.p_id == itemId)
                {
					q=p.p_quantity;
					price -= p.p_price * p.p_quantity;
					cart.Remove(p);
					break;
                }
            }
			foreach (Product p in pro)
			{
				if (p.p_id == itemId)
				{
					p.p_quantity += q;
					break;
				}
			}
		}

        public customer()
        {
            Name = "Default Name";
            price = 0;
        }

        public customer(string Name,int price)
        {
            this.Name = Name;
            this.price = price;
        }

		public customer(string Name)
		{
			this.Name = Name;
			this.price = 0;
		}

		public virtual double totalPaid()
		{
			return price;
		}
	}

    public class visaCustomer : customer
    {
        private double discount;
        public double discount_p
        {
            get { return discount; }
            set { 
                if(0 < value)
                    discount = value;
                else
                    discount = 0;
            }
        }

        public visaCustomer(string Name)
        {
            this.Name = Name;
			price = 0;
        }

		public visaCustomer()
		{
			this.Name = "defualt";
			this.price = 0;
		}

		public override double totalPaid()
        {
            return price - price*discount;
        } 
    }

    public class cashCustomer : customer
    {
        public cashCustomer(string Name)
        {
            this.Name = Name;
            price = 0;
        }

        public override double totalPaid()
        {
            return price;
        }
    }

	public class Product
	{
		public static int counter;
		public int p_id = counter;

		public string p_name;
		public double p_price;
		public int p_quantity;

		public Product(string p_name, double p_price, int p_quantity)
		{
			this.p_name = p_name;
			this.p_price = p_price;
			this.p_quantity = p_quantity;
			Product.counter++;
		}

		public void Modify_Product()
		{
			Console.WriteLine("You are modeifing the following product.. ");
			Console.WriteLine("\nID\tName\t\t\tPrice\tQuantity");
			Console.WriteLine("-------------------------------------------------");
			Console.Write("{0}", this.p_id);
			Console.Write("\t{0}", this.p_name);
			Console.Write("\t\t\t{0}", this.p_price);
			Console.Write("\t  {0}", this.p_quantity);
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("What do yuo want to change...");
			Console.WriteLine("  [1] Name");
			Console.WriteLine("  [2] Price");
			Console.Write("Please Enter (1) or (2)   ");
			int choice;
			bool ChoiceSuccess = int.TryParse(Console.ReadLine(), out choice);
			if (choice == 1)
			{
				Console.Write("Please Enter the new name: ");
				string N_name = Console.ReadLine();
				this.p_name = N_name;
			}
			else if (choice == 2)
			{
				Console.Write("Please Enter the new price: ");
				double N_price = double.Parse(Console.ReadLine());
				this.p_price = N_price;
			}
			else
			{
				Console.WriteLine("\n\t---> Please Enter a valid input.\n");
				System.Threading.Thread.Sleep(1000);
				Modify_Product();

			}
		}

		public Product(Product old)
        {
			this.p_id = old.p_id;
			this.p_name = old.p_name;
			this.p_price = old.p_price;
			this.p_quantity = old.p_quantity;
        }

		public override string ToString()
		{
			return "Name: " + p_name + "Price: " + p_price;
		}

		public static bool operator >(Product a, Product b)
        {
			return a.p_price > b.p_price;
        }

		public static bool operator <(Product a, Product b)
		{
			return a.p_price < b.p_price;
		}
	}

	public class _Market
	{
		private static double income;
		public int NumberOfCustomers;
		public List<Product> Products;
		public Product this[int i] { get{ return Products[i]; } set { Products[i] = value; } }
		public static double _income { get { return income; } } 
		public _Market()
        {
			income = 0;
        }
		public _Market(List<Product> p)
        {
			income = 0;
			Products = p;
        }
		public void deleteProduct(int _id)
		{
			Console.WriteLine("You are deleting the following product.. ");
			Console.WriteLine("\nID\tName\t\t\tPrice\tQuantity");
			Console.WriteLine("-------------------------------------------------");
			Console.Write("{0}", Products[_id].p_id);
			Console.Write("\t{0}", Products[_id].p_name);
			Console.Write("\t\t\t{0}", Products[_id].p_price);
			Console.Write("\t  {0}", Products[_id].p_quantity);
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Are you sure you want to delete this product ???   ");
			Console.WriteLine("    [1] yes, delete this product.");
			Console.WriteLine("    [2] no, i donnot want to delete this product");
			Console.Write("Please Choose number (1) or number (2)     ");
			int lastchoce;
			bool lastchoceSuccess = int.TryParse(Console.ReadLine(), out lastchoce);
			if(lastchoce == 1){
				foreach (Product p in Products)
	            {
					if(p.p_id == _id)
	                {
						Products.Remove(p);
						break;
					}
	            }
	        }else if(lastchoce == 2){
	        	Project.Product_Delete();
	        }else{
	        	Console.WriteLine("\n\t -----> Please Enter a valid input <-----\n");
	        	deleteProduct(_id);
	        }
		}

		public void AddProduct(List<Product> p1)
        {
			for (int i = 0; i < p1.Count; i++)
            {
				Products.Add(p1[i]);
            }
        }

		public void addProduct(Product ObJect)
		{
			Products.Add(ObJect);
		}

		public double checkOut(ref customer c)
        {
				income += c.totalPaid();
				return c.totalPaid();
        }

		public void print_products()
		{
			Console.WriteLine("-------------------------------------------------");
			Console.WriteLine("ID\tName\t\t\tPrice\tQuantity");
			Console.WriteLine("-------------------------------------------------");
			foreach (Product p in Products)
			{
				Console.Write("{0}", p.p_id);
				Console.Write("\t{0}", p.p_name);
				Console.Write("\t\t\t{0}", p.p_price);
				Console.Write("\t  {0}", p.p_quantity);
				Console.WriteLine("");
			}
			Console.WriteLine();
		}

		public void addQuantity(int _id, int q)
		{
			foreach (Product p in Products)
			{
				if (p.p_id == _id)
				{
					p.p_quantity += q;
				}
			}
		}

		public void modifyP(int _id)
        {
			foreach(Product p in Products)
            {
				if(p.p_id == _id)
                {
					p.Modify_Product();
					break;
                }
            }
        }
	}

	class Project
	{

		public static List<Product> Products = new List<Product> {
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
		public static _Market newMarket = new _Market(Products);
		public static customer MainCustomer;

		public static void CustomerORstuff()
		{
			Console.WriteLine("\t[1] Marketr Staff");
			Console.WriteLine("\t[2] Customer");
			Console.Write("Please choose (1) or (2)    ");
			int _1stchoice;
			bool _1stchoiceCheck = int.TryParse(Console.ReadLine(), out _1stchoice);
			if (_1stchoice == 1)
			{
				staff_log_in();
			}
			else if (_1stchoice == 2)
			{
				
				while (true)
				{
					Console.Clear();
					Console.WriteLine("Welcome to customer mangment section....");
					Console.WriteLine("please choose the payment method to proceed");
					Console.WriteLine("\t[1] Visa");
					Console.WriteLine("\t[2] Cash");
					Console.Write("Choose form (1) or (2): ");
					int op;
					bool opSuccess = int.TryParse(Console.ReadLine(), out op);

					if(opSuccess){
						if (op == 1)
						{
							Console.Write("Enter Customer Name: ");
							string nam = Console.ReadLine();
							MainCustomer = new visaCustomer(nam);
							customerGet();
						}
						else if (op == 2)
						{
							Console.Write("Enter Customer Name: ");
							string nam = Console.ReadLine();
							MainCustomer = new cashCustomer(nam);
							customerGet();
						}else{
							Console.WriteLine("\n\t---> Please Choose form 1 or 2 only <---\n");
							System.Threading.Thread.Sleep(1000);
						}
					}else{
						Console.WriteLine("\n\t---> Please Enter a valid input. <---\n");
						System.Threading.Thread.Sleep(1000);
					}
				}
			}
			else
			{
				Console.WriteLine("\n\t---> Please Enter a valid input. <---");
				Console.WriteLine("\t---> number (1) and number (2) are valid inputs. <---\n");
				CustomerORstuff();
			}
		}

		public static void customerGet()
        {
			Console.Clear();
			Console.WriteLine("Please choose what do you want to do....");
			Console.WriteLine("[-] Products");
			Console.WriteLine("\t[1] List cart Products");
			Console.WriteLine("\t[2] Add Products to cart");
			Console.WriteLine("\t[3] Delete Products from cart");
			Console.WriteLine("\t[4] Checkout");
			Console.Write("Please choose an operation form 1 to 4:     ");
			int  _1staffchoice;
			bool _1staffchoiceScucess = int.TryParse(Console.ReadLine(), out _1staffchoice);
			if (_1staffchoice == 1)
			{
				MainCustomer.print_cart();
				In_or_out_c();
			}
			else if (_1staffchoice == 2)
			{
				newMarket.print_products();
				while(true){
					Console.Write("Enter product Id: ");
					int  _id;
					bool _idSuccess = int.TryParse(Console.ReadLine(), out _id);
					if(_idSuccess){
						bool found = false;
						foreach(Product p in Products)
				            {
								if(p.p_id == _id)
				                {
									found = true;
									break;
				                }
				                else
				                {
				                	found = false;
				                }
				            }
				        if(found == true)
				            {
								MainCustomer.addTocart(newMarket[_id]);
								customerGet();

			            	}else
			            	{
			            		Console.WriteLine("\n\t----> There is no product whis this id please neter a valid id. <----\n");

			            	}
					}else{
						Console.WriteLine("\n\t----> Please enter a valid input <----\n");
					}
				}
			}
			else if (_1staffchoice == 3)
			{
				Product_Delete_C();
				In_or_out_c();
			}
			else if (_1staffchoice == 4)
			{
				MainCustomer.print_cart();
				Console.WriteLine("Total Price: " + newMarket.checkOut(ref MainCustomer));
				Console.WriteLine("Thank you for choosing out Sooftware...");
				Console.WriteLine("Have a good day...");
				Environment.Exit(0);
			}
			else
			{
				Console.WriteLine("\n\t----> Please Enter a vaild input <----\n");
				System.Threading.Thread.Sleep(1000);
				customerGet();
			}
		}

		public static void In_or_out_c()
		{
			Console.WriteLine("Would you like to proceed or exit...");
			Console.WriteLine("\t[1] Proceed");
			Console.WriteLine("\t[2] exit");
			Console.Write("Please Choose (1) or (2)    ");
			int P_var;
			bool P_varSuccess = int.TryParse(Console.ReadLine(), out P_var);
			if (P_var == 1)
			{
				customerGet();
			}
			else
			{
				Console.WriteLine("Ok Good BYE........");
				Environment.Exit(0);
			}
		}

		public static void Product_Delete_C()
		{
			while (true)
			{
				Console.Write("Enter Product id: ");
				int _id;
				bool _idSuccess = int.TryParse(Console.ReadLine(), out _id);
				if (_idSuccess)
				{
					MainCustomer.removeFromcart(_id, newMarket.Products);
					break;
				}
				else
				{
					Console.WriteLine("Please Enter a valid Intger...");
				}
			}
		}

		public static void Product_modification()
		{
			while (true)
			{
				Console.Write("Enter Product id: ");
				int _id;
				bool _idSuccess = int.TryParse(Console.ReadLine(), out _id);
				if (_idSuccess)
				{
					bool found = false;
					foreach(Product p in newMarket.Products)
			            {
							if(p.p_id == _id)
			                {
								found = true;
								Console.WriteLine(p.ToString());
								break;
			                }
			                else
			                {
			                	found = false;
			                }
			            }
		            if (found) {
						newMarket.modifyP(_id);
						break;
					}else{
						Console.WriteLine("\n\t-----> There in no product with this id please enter a valid id. <-----\n");
					}
				}
				else
				{
					Console.WriteLine("\n\t-----> There in no product with this id please enter a valid id. <-----\n");
				}
			}
		}

		public static void staff_log_in()
		{
			Console.WriteLine("\n*** Hi their, please enter market staff login credentials ***");
			Console.Write("username: ");
			string uname = Console.ReadLine();
			Console.Write("password: ");
			string password = Console.ReadLine();
			if (uname == "admin" && password == "admin")
			{
				Staff_List_Choose();
			}
			else
			{
				Console.WriteLine("\n----> Wrong username or password please try agin <----\n");
				staff_log_in();
			}
		}

		public static void Staff_List_Choose()
		{
			Console.Clear();
			Console.WriteLine("Welcome to admin panel.....");
			Console.WriteLine("Please choose what do you want to do....");
			Console.WriteLine("[-] Products");
			Console.WriteLine("\t[1] List    Products");
			Console.WriteLine("\t[2] Add     Products");
			Console.WriteLine("\t[3] Modifiy Products");
			Console.WriteLine("\t[4] Delete  Products");
			Console.Write("Please choose operation from 1 to 4:     ");
			int _1staffchoice;
			bool _1staffchoiceScucess = int.TryParse(Console.ReadLine(), out _1staffchoice);
			if (_1staffchoice == 1)
			{
				newMarket.print_products();
				In_or_out();
			}
			else if (_1staffchoice == 2)
			{
				add_product();
				In_or_out();
			}else if(_1staffchoice == 3)
            {
				Product_modification();
				In_or_out();
			}
			else if (_1staffchoice == 4)
			{
				Product_Delete();
				In_or_out();
			}
			else
			{
				Console.WriteLine("\n\t----> Please Enter a vaild input <----\n");
				Console.WriteLine("\t----> numbers from 1 to 4 are valid inputs <----\n");
				System.Threading.Thread.Sleep(1000);

				Staff_List_Choose();
			}
		}

		public static void add_product()
		{
			Console.WriteLine("  [1] add to existing product");
			Console.WriteLine("  [2] add new product");
			Console.Write("Please Choose (1) or (2)    ");
			int success;
			bool productScucess = int.TryParse(Console.ReadLine(), out success);
			if (success == 1)
			{
				while (true)
				{
					Console.Write("Enter Product id: ");
					int _id;
					bool _idSuccess = int.TryParse(Console.ReadLine(), out _id);
					bool found = false;
					foreach(Product p in Products)
			            {
							if(p.p_id == _id)
			                {
								found = true;
								break;
			                }
			                else
			                {
			                	found = false;
			                }
			            }
					if (_idSuccess && found == true)
					{
						Console.Write("Enter the quantity you woule like to add: ");
						int quantityCheck;
						bool quantityCheckSuccess = int.TryParse(Console.ReadLine(), out quantityCheck);
						if (quantityCheckSuccess)
						{
							newMarket.addQuantity(_id, quantityCheck);
							break;
						}
						else
						{
							Console.WriteLine("\n\t---> Plese Enter valid intger.... <---\n");
						}
					}
					else
					{
						Console.WriteLine("\n\t---> Not a valid id, please Enter a valid id... <---\n");
					}
				}
			}
			else if (success == 2)
			{
				Console.Write("Enter Product Name: ");
				string name = Console.ReadLine();

				double _price;
				while (true)
				{
					Console.Write("Enter Product Price: ");
					bool _priceSuccess = double.TryParse(Console.ReadLine(), out _price);
					if (_priceSuccess)
					{
						break;
					}
					else
					{
						Console.WriteLine("Please Enter a valid decmial...");
					}
				}

				int _quantity;
				while (true)
				{
					Console.Write("Enter Product Quantity: ");
					bool _quantitySuccess = int.TryParse(Console.ReadLine(), out _quantity);
					if (_quantitySuccess)
					{
						break;
					}
					else
					{
						Console.WriteLine("Please Enter a valid intger...");
					}
				}
				Product p = new Product(name, _price, _quantity);
				newMarket.addProduct(p);
			}
			else
			{
				Console.WriteLine("\n----> Please Enter a vaild choice number (1) or number (2) <----\n");
				add_product();
			}
		}

		public static void Product_Delete()
		{
			while (true)
			{
				Console.Write("Enter Product id: ");
				int _id;
				bool _idSuccess = int.TryParse(Console.ReadLine(), out _id);
				bool found = false;
				foreach(Product p in Products)
		            {
						if(p.p_id == _id)
		                {
							found = true;
							break;
		                }
		                else
		                {
		                	found = false;
		                }
		            }
				if (_idSuccess && found == true)
				{
					newMarket.deleteProduct(_id);
					break;
				}
				else
				{
					Console.WriteLine("\n\t-----> There in no product with this id please enter a valid id. <-----\n");
				}
			}
		}

		public static void In_or_out()
		{
			Console.WriteLine("Would you like to proceed or exit...");
			Console.WriteLine("\t[1] Proceed");
			Console.WriteLine("\t[2] exit");
			Console.Write("Please Choose (1) or (2)    ");
			int P_var;
			bool P_varSuccess = int.TryParse(Console.ReadLine(), out P_var);
			if (P_var == 1)
			{
				Staff_List_Choose();
			}
			else
			{
				Console.WriteLine("Ok Good BYE........");
			}
		}

		public static void Main(string[] args)
		{
			
			Console.WriteLine("Welcome to our store software....");
			CustomerORstuff();
		}
	}
}
