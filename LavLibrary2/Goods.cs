using System;
using System.Windows;
using System.Windows.Controls;
namespace LavLibrary2
{
    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public int QuantityWarehouse { get; set; }
        public int Barcode { get; set; }
    }
}
