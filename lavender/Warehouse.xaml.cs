using LavLibrary2;
using logger;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
namespace lavender
{
    /// <summary>
    /// Логика взаимодействия для Warehouse.xaml
    /// </summary>
    public partial class Warehouse : Window
    {
        List<Goods> list = new List<Goods>();
        public Warehouse()
        {
            InitializeComponent();
            using (var connection = new SQLiteConnection("Data Source=lavender.db"))
            {
                connection.Open();
                string sqlExpression = "SELECT * FROM Product";
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                //Используем класс SqliteDataReader для считывания данных по запросу Select
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //Если у нас есть хоть какие-то строки, то у нас будет все считываться
                    if (reader.HasRows)
                    {
                        //И пока у нас программа считывает строки, мы с ними взаимодействуем
                        while (reader.Read())
                        {
                            if (reader.GetInt32(7) > 0)
                            {
                                Goods item = new();
                                item.Id = reader.GetInt32(0);
                                item.Name = reader.GetString(1);
                                item.Price = reader.GetInt32(2);
                                item.Manufacturer = reader.GetString(3);
                                item.Barcode = reader.GetInt32(4);
                                item.Type = reader.GetString(5);
                                item.QuantityWarehouse = reader.GetInt32(7);
                                item.Quantity = reader.GetInt32(6);
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            ProdTabel.ItemsSource = list;
        }
        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
            new LoggerClass().MLogg("закрытие окна склада");
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
            new LoggerClass().MLogg("открытие корзины");
        }
        private void ButtonHall(object sender, RoutedEventArgs e)
        {
             MainWindow mainWindow = new MainWindow();
             mainWindow.Show();
            new LoggerClass().MLogg("открытие окна зала");
        }
        /// <summary>
        /// открытие окна редактирования
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void EditClick(object sender, RoutedEventArgs e)
        {
            Goods SelectedItem = (Goods)ProdTabel.SelectedItem;
            if (SelectedItem == null)
            {
                MessageBox.Show("Выберете элемент");
            }
            else
            {
                editing editing = new editing(SelectedItem);
                editing.ShowDialog();
            }
            new LoggerClass().MLogg("открытие окна редактирования");
        }
        /// <summary>
        /// открытие окна добавления
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void AddClick(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.Show();
            new LoggerClass().MLogg("открытие окна добавления");
        }
        /// <summary>
        /// удаление товара
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void DelClick(object sender, RoutedEventArgs e)
        {
            Goods SelectedItem = (Goods)ProdTabel.SelectedItem;
            if (SelectedItem == null)
            {
                MessageBox.Show("Выберете элемент");
            }
            else
            {
                using (var connection = new SQLiteConnection("Data Source=Lavender.db"))
                {
                    connection.Open();
                    string sqlExpression = $"DELETE FROM Product WHERE idProduct = {SelectedItem.Id}";
                    SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
            }
            new LoggerClass().MLogg("удаление товара");
        }
        /// <summary>
        /// добавление в корзину
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonBasketAdd(object sender, RoutedEventArgs e)
        {
            Goods SelectedItem = (Goods)ProdTabel.SelectedItem;
            if (SelectedItem == null)
            {
                MessageBox.Show("Выберете элемент");
            }
            else
            {
                using (var connection = new SQLiteConnection("Data Source=Lavender.db"))
                {
                    connection.Open();
                    string sqlExpression = $"INSERT INTO Basket(name, price) VALUES('{SelectedItem.Name}', {SelectedItem.Price}) ";
                    SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();

                }
            }
            new LoggerClass().MLogg("добавление в корзину");
        }
    }
}
