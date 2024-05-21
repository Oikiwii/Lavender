using LavLibrary2;
using logger;
using System;
using System.Data.SQLite;
using System.Windows;
namespace lavender
{
    /// <summary>
    /// Логика взаимодействия для editing.xaml
    /// </summary>
    public partial class editing : Window
    {
        Goods item;
        public editing()
        {
            InitializeComponent();
        }
        public editing(Goods a)
        {
            InitializeComponent();
            Name.Text = a.Name;
            Price.Text = a.Price.ToString();
            QuantityHall.Text =a.Quantity.ToString();
            QuantityWarehouse.Text = a.QuantityWarehouse.ToString();
            item = a;
        }
        /// <summary>
        /// закрывает окно
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
            private void ButtonBack(object sender, RoutedEventArgs e)
        {
            this.Close();
            new LoggerClass().MLogg("закрытие окна редактирования");
        }
        /// <summary>
        /// сохраняет изменение
        /// </summary>
        /// <param name="sender">кнопка</param>
        /// <param name="e">объект события</param>
        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            if(Name.Text==string.Empty|| Price.Text==string.Empty|| QuantityHall.Text == string.Empty|| QuantityWarehouse.Text == string.Empty)
            {
                MessageBox.Show("Введите данные");
            }
            else
            {
                    using (var connection = new SQLiteConnection("Data Source=lavender.db"))
                    {
                        connection.Open();
                        string sqlExpression = $"UPDATE Product SET Name = '{Name.Text}', Price = {int.Parse(Price.Text)}, QuantityHall = {int.Parse(QuantityHall.Text)}, QuantityWarehouse = {int.Parse(QuantityWarehouse.Text)} WHERE idProduct = {item.Id}";
                        SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                    MessageBox.Show("Данные изменены");

                    }
                new LoggerClass().MLogg("сохрание редактирования");
            }
        }
    }
}
