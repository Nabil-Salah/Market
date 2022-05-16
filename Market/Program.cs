using System;
namespace OOP_Project {

/*
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
*/

class Product{
		public static uint counter;
		public uint p_id = counter;

		public string p_name;
		public double p_price;
		public int    p_quantity;

		public Product(string p_name, double p_price, int p_quantity) {
			this.p_name     = p_name;
			this.p_price    = p_price;
			this.p_quantity = p_quantity;
			Product.counter++;
		}
	}


	class Project {

		public static Product[] Products = new Product[] {
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
		public static Product[] new_product = new Product[100];
		public static int index;


		/* ----------- start of customer ot staff Method ----------- */
		public static void CustomerORstuff() {
			Console.WriteLine("\t[1] Marketr Staff");
			Console.WriteLine("\t[2] Customer");
			Console.Write("Please choose (1) or (2)    ");
			int  _1stchoice;
			bool _1stchoiceCheck = int.TryParse(Console.ReadLine(), out _1stchoice);
			if(_1stchoice == 1) {
				staff_log_in();
			}else if(_1stchoice == 2){
				Console.WriteLine("HI, Customer...");
			}else{
				Console.WriteLine("\n----> Please Enter number (1) or number (2) <----\n");
				CustomerORstuff();
			}
		}
		/* ----------- end of customer ot staff Method ----------- */


		/* ----------- Start of staff login Method ----------- */
		public static void staff_log_in() {
			Console.WriteLine("\n*** Hi their, please enter market staff login credentials ***");
			Console.Write("username: ");
			string uname = Console.ReadLine();
			Console.Write("password: ");
			string password = Console.ReadLine();
			if (uname == "admin" && password == "admin"){
				Staff_List_Choose();
			}else{
				Console.WriteLine("\n----> Wrong user name or password please try agin <----\n");
				staff_log_in();
			}
		}
		/* ----------- end of staff login Method ----------- */


		/* ----------- Start of staff list Method ----------- */
		public static void Staff_List_Choose() {
			Console.Clear();
			Console.WriteLine("Welcome to admin panel.....");
			Console.WriteLine("Please choose what do you want to do....");
			Console.WriteLine("[-] Products");
			Console.WriteLine("\t[1] List    Products");
			Console.WriteLine("\t[2] Add     Products");
			Console.WriteLine("\t[3] Modifiy Products");
			Console.WriteLine("\t[4] Delete  Products");
			Console.Write("Please choose number from the above:     ");
			int  _1staffchoice;
			bool _1staffchoiceScucess = int.TryParse(Console.ReadLine(), out _1staffchoice);
			if(_1staffchoice == 1){
				print_products();
				In_or_out();
			}else if(_1staffchoice == 2){
				add_product();
				In_or_out();
			}else{
				Console.WriteLine("\n----> Please Enter a vaild number <----\n");
				Staff_List_Choose();
			}
		}
		/* ----------- end of staff list Method ----------- */


		/* ----------- Start of print products Method ----------- */
		public static void print_products(){
			Console.WriteLine("-----------------------------------------");
			Console.WriteLine("ID\tName\t\t\tPrice\tQuantity");
			Console.WriteLine("-----------------------------------------");
			foreach (Product p in Products) {
				Console.Write("{0}",p.p_id);
				Console.Write("\t{0}",p.p_name);
				Console.Write("\t\t\t{0}",p.p_price);
				Console.Write("\t{0}",p.p_quantity);
				Console.WriteLine();
			}
			for (uint i = 0; i<Project.index; i++){
				Console.Write("{0}",new_product[i].p_id);
			 	Console.Write("\t{0}",new_product[i].p_name);
			 	Console.Write("\t\t\t{0}",new_product[i].p_price);
			 	Console.Write("\t{0}",new_product[i].p_quantity);
			 	Console.WriteLine();
			}
		}
		/* ----------- end of print products Method ----------- */


		/* ----------- Start of add products Method ----------- */
		public static void add_product() {
					Console.WriteLine("[1] add to existing product");
					Console.WriteLine("[2] add new product");
					Console.Write("Please Choose (1) or (2)    ");
					int  success;
					bool productScucess = int.TryParse(Console.ReadLine(), out success);
					if (success == 1){
						while (true) {
							Console.Write("Enter Product id: ");
							int  _id;
							bool _idSuccess = int.TryParse(Console.ReadLine(), out _id);
							if (_idSuccess){
								if(_id <= 13){
									Console.Write("Enter the quantity you woule like to add: ");
									int quantityCheck;
									bool quantityCheckSuccess = int.TryParse(Console.ReadLine(), out quantityCheck);
									if (quantityCheckSuccess) {
										Products[_id].p_quantity+=quantityCheck;
										break;
									}else{
										Console.WriteLine("Plese Enter valid intger....");
									}
								}else if(_id > 13 && (Project.index-_id)>=0){
									Console.Write("Enter the quantity you woule like to add: ");
									int quantityCheck;
									bool quantityCheckSuccess = int.TryParse(Console.ReadLine(), out quantityCheck);
									if (quantityCheckSuccess) {
										new_product[_id].p_quantity+=quantityCheck;
										break;
									}else{
										Console.WriteLine("Plese Enter valid intger....");
									}
								}else{
									Console.WriteLine("---> Their is no prduct with this id, please enter a valid id.");
								}
							}else{
								Console.WriteLine("Please Enter a valid Intger...");
							}
						}
					}
					else if (success == 2){
						Project.index = 0;
						Console.Write("Enter Product Name: ");
						string name = Console.ReadLine();

						double _price;
						while (true) {
							Console.Write("Enter Product Price: ");
							bool _priceSuccess = double.TryParse(Console.ReadLine(), out _price);
							if (_priceSuccess){
								break;
							}else{
								Console.WriteLine("Please Enter a valid decmial...");
							}
						}

						int _quantity;
						while (true) {
							Console.Write("Enter Product Quantity: ");
							bool _quantitySuccess = int.TryParse(Console.ReadLine(), out _quantity);
							if (_quantitySuccess){
								break;
							}else{
								Console.WriteLine("Please Enter a valid intger...");
							}
						}

						new_product[Project.index] = new Product(name, _price, _quantity);
						Project.index++;
					}else{
						Console.WriteLine("\n----> Please Enter a vaild choic number (1) or number (2) <----\n");
						add_product();
					}
				}
		/* ----------- end of add products Method ----------- */

		/* ----------- start of in or out Method ----------- */
			public static void In_or_out() {
				Console.WriteLine("Would you like to proceed or exit...");
				Console.WriteLine("\t[1] Proceed");
				Console.WriteLine("\t[2] exit");
				Console.Write("Please Choose (1) or (2)    ");
				int P_var;
				bool P_varSuccess = int.TryParse(Console.ReadLine(), out P_var);
				if (P_var == 1){
					Staff_List_Choose();
				}else{
					Console.WriteLine("Ok Good BYE........");
				}
			}
		/* ----------- end of int or out Method ----------- */

		/* ----------- Start of Main Method ----------- */
		public static void Main(string[] args) {
			Console.WriteLine("Welcome to our store software....");
			CustomerORstuff();
		}
		/* ----------- end of Main Method ----------- */
	}
}
