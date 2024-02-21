using Caliburn.Micro;
using RMWindowsUI.Library.Api;
using RMWindowsUI.Library.Helpers;
using RMWindowsUI.Library.Models;
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
        IProductEndpoint _productEndpoint;
        IConfigHelper _configHelper;
        public SalesViewModel(IProductEndpoint productEndpoint, IConfigHelper configHelper)
        {
            _productEndpoint = productEndpoint;     
            _configHelper = configHelper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        // need to add a loading indicator on frontend
        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

		private BindingList<ProductModel> _products;

		public BindingList<ProductModel> Products
		{
			get { return _products; }
			set 
			{ 
				_products = value;
                NotifyOfPropertyChange(() => Products);
            }
		}

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }


        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantity = 1;
        // using int means that quantity has to be typed as a number in the textbox
		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set 
			{ 
				_itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
		}

        private string _subTotal;
        public string SubTotal
        {
            // replace with calculation
            get 
            {
                return CalculateSubTotal().ToString("C");
            }
            
        }

        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;
            foreach (var item in Cart)
            {
                subTotal += item.Product.RetailPrice * item.QuantityInCart;
            }

            return subTotal;
        }

        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.GetTaxRate() / 100;

            taxAmount = Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);

            //foreach (var item in Cart)
            //{
            //    if (item.Product.IsTaxable)
            //    {
            //        taxAmount += (item.Product.RetailPrice * item.QuantityInCart * taxRate);
            //    }
            //}
            return taxAmount;
        }

        public string Tax
        {
            get 
            {
                return CalculateTax().ToString("C"); 
            }
        }

        

        public string Total
        {
            get 
            { 
                decimal total = CalculateSubTotal() + CalculateTax();
                return total.ToString("C");
            }
        }


        // buttons

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                // make sure an item is selected
				// make sure there is an item quantity
                if (ItemQuantity >= 1 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }


                return output;
            }

        }
        public void AddToCart()
		{
            // compare to find exact same object lambda expression
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);
            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;
                // HACK should be a better way of refreshing cart
                Cart.Remove(existingItem);
                Cart.Add(existingItem);
            } else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity,
                };
                Cart.Add(item);
            }
            
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
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
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        public bool CanCheckOut
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
