using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWindowsUI.ViewModels
{
    public class SalesViewModel : Screen
    {
		private BindingList<string> _products;

		public BindingList<string> Products
		{
			get { return _products; }
			set 
			{ 
				_products = value;
                NotifyOfPropertyChange(() => Products);
            }
		}

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get { return _cart; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantity;

		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set 
			{ 
				_itemQuantity = value;
                NotifyOfPropertyChange(() => Products);
            }
		}


        public string Subtotal
        {
            // replace with calculation
            get { return "$0.00"; }
        }

        public string Tax
        {
            // replace with calculation
            get { return "$0.00"; }
        }

        public string Total
        {
            // replace with calculation
            get { return "$0.00"; }
        }


        // buttons

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                // make sure an item is selected
				// make sure there is an item quantity


                return output;
            }

        }
        public void AddToCart()
		{

		}

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                // make sure an item is selected
                


                return output;
            }

        }
        public void RemoveFromCart()
        {

        }

        public bool CanCheckout
        {
            get
            {
                bool output = false;

                // make sure sometime is in cart



                return output;
            }

        }
        public void CheckOut()
        {

        }


    }
}
