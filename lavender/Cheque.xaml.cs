using LavLibrary2;
using logger;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
namespace lavender
{
    /// <summary>
    /// Логика взаимодействия для Cheque.xaml
    /// </summary>
    public partial class Cheque : Window
    {
        List<Goods> list = new List<Goods>();
        public Cheque()
        {
            InitializeComponent();
            string text = "";
            int sum =0;
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
            for(int i = 0; i < list.Count; i++)
            {
                string text2 = $"Название: {list[i].Name} Цена: {list[i].Price}";
                text += text2 + "\n";
                sum += list[i].Price;
            }
            text += $"Итог: {sum}";
            ProdCheque.Text = text;
        }
        /// <summary>
        /// закрывает окно
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonBack(object sender, RoutedEventArgs e)
        {
         this.Close();
            new LoggerClass().MLogg("закрытие окна чек");
        }
        /// <summary>
        /// печать чека
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonSeal(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Печать");
            new LoggerClass().MLogg("печать чека");
        }
    }
}
