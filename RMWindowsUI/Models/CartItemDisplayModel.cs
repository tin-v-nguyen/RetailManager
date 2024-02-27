using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWindowsUI.Models
{
    // INotifyPropertyChanged is from .NET not Caliburn.Micro, NotifyOfPropertyChange is CM
    public class CartItemDisplayModel : INotifyPropertyChanged

    {
        public ProductDisplayModel Product { get; set; }

        private int _quantityInCart;
        public int QuantityInCart
        {
            get { return _quantityInCart; }
            set 
            { 
                _quantityInCart = value;
                
                // This isnt needed?
                // CallPropertyChanged(nameof(QuantityInCart));
                CallPropertyChanged(nameof(DisplayText));
            }
        }

        public string DisplayText
        {
            get
            {
                return $"{Product.ProductName} ({QuantityInCart})";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void CallPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
