using logger;
using System;
using System.Data.SQLite;
using System.Windows;
namespace lavender
{
    /// <summary>
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        public Product()
        {
            InitializeComponent();
        }
        /// <summary>
        /// переход в корзину
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonBasket(object sender, RoutedEventArgs e)
        {
            basket basket = new basket();
            basket.Show();
        }
        /// <summary>
        /// закрытие окна
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            this.Close();
            new LoggerClass().MLogg("закрытие окна добавление");
        }
        /// <summary>
        /// сохранение изменений в таблице 
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            if (Name.Text == string.Empty || Price.Text == string.Empty || Manufacturer.Text == string.Empty || Barcode.Text == String.Empty || Type.Text == string.Empty || QuantityHall.Text == string.Empty || QuantityWarehouse.Text == string.Empty)
            {
                MessageBox.Show("Введите данные");
            }
            else
            {
                using (var connection = new SQLiteConnection("Data Source=Lavender.db"))
                {
                    connection.Open();
                    string sqlExpression = $"INSERT INTO Product(Name, Price, Manufacturer, Barcode, Type, QuantityHall, QuantityWarehouse) VALUES('{Name.Text}', {Price.Text}, '{Manufacturer.Text}', {Barcode.Text}, '{Type.Text}', {QuantityHall.Text}, {QuantityWarehouse.Text}) ";
                    SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
            }
            new LoggerClass().MLogg("сохранение изменений");
        }
    }
}
