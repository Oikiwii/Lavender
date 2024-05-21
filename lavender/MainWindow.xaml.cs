using LavLibrary2;
using logger;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
namespace lavender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Goods> list = new List<Goods>();
        public MainWindow()
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
                            if (reader.GetInt32(6) > 0)
                            {
                                Goods item = new();
                                item.Id = reader.GetInt32(0);
                                item.Name = reader.GetString(1);
                                item.Price = reader.GetInt32(2);
                                item.Manufacturer = reader.GetString(3);
                                item.Barcode = reader.GetInt32(4);
                                item.Type = reader.GetString(5);
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
      /// закрывает окно
      /// </summary>
      /// <param name="sender">кнопка</param>
      /// <param name="e">объект события</param>
        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
            new LoggerClass().MLogg("закрытие окна товаров в зале");
        }
      /// <summary>
      /// переход на окно корзины
      /// </summary>
      /// <param name="sender">кнопка</param>
      /// <param name="e">объект события</param>
        private void ButtonBasket(object sender, RoutedEventArgs e)
        {
            basket basket = new basket();
            basket.Show();
            new LoggerClass().MLogg("переход в корзину");
        }
        /// <summary>
        /// переход на окно склада
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonWarehouse(object sender, RoutedEventArgs e)
        {
            Warehouse warehouse = new Warehouse();
            warehouse.Show();
            new LoggerClass().MLogg("переход на окно склад");
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
