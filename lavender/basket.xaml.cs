using LavLibrary2;
using logger;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
namespace lavender
{
    /// <summary>
    /// Логика взаимодействия для basket.xaml
    /// </summary>
    public partial class basket : Window
    {
        List<Goods> list = new List<Goods>();
        public basket()
        {
            InitializeComponent();
            using (var connection = new SQLiteConnection("Data Source=lavender.db"))
            {
                connection.Open();

                string sqlExpression = "SELECT * FROM Basket";
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
                            
                                Goods item = new();
                                item.Id = reader.GetInt32(0);
                                item.Name = reader.GetString(1);
                                item.Price = reader.GetInt32(2);
                                list.Add(item);
                        }
                    }
                }
            }
           TableBasket.ItemsSource = list;
        }
        /// <summary>
        /// закрывает окно
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            this.Close();
            new LoggerClass().MLogg("закрывает окно корзины");
            
        }
        /// <summary>
        /// открывает окно чека
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonBuy(object sender, RoutedEventArgs e)
        {
            Cheque cheque = new Cheque();
            cheque.Show();
            new LoggerClass().MLogg("открывает окно чека");
        }
        /// <summary>
        /// удаление из корзины
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonDel(object sender, RoutedEventArgs e)
        {
            Goods SelectedItem = (Goods)TableBasket.SelectedItem;
            if (SelectedItem == null)
            {
                MessageBox.Show("Выберете элемент");
            }
            else
            {
                using (var connection = new SQLiteConnection("Data Source=Lavender.db"))
                {
                    connection.Open();
                    string sqlExpression = $"DELETE FROM Basket WHERE idBasket = {SelectedItem.Id}";
                    SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Позиция удалена");
                }
            }
            new LoggerClass().MLogg("удаляет из корзины");
        }
    }
}
