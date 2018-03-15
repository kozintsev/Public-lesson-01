using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncStart
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = new WebClient().DownloadString("http://habrahabr.ru/");
        }

        private async void ButtonAcync_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = await new WebClient().DownloadStringTaskAsync("http://habrahabr.ru/");
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Убираем возможность повторного нажатия на кнопку
            StartButton.IsEnabled = false;

            // Вызываем новую задачу, на этом выполнение функции закончится
            // а остаток функции установится в продолжение
            TextBox.Text = await new WebClient().DownloadStringTaskAsync("http://habrahabr.ru/");
            StatusLabel.Content = "Загрузка страницы завершена, начинается обработка";

            // В продолжении можно также запускать асинхронные операции со своим продолжением
            var result = await Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(5000); // Имитация длительной обработки...
                return "Результат обработки";
            });

            // Продолжение второй асинхронной операции
            StatusLabel.Content = result;
            StartButton.IsEnabled = true;
        }
    }
}
