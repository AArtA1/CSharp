using System.Windows;
/// <summary>
/// Основной метод пространств 
/// </summary>
namespace Fractals3._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализация главного окна 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Создание окна для первого фрактала 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FirstFractal firstFractal = new FirstFractal();
            firstFractal.Show();
        }
        /// <summary>
        /// Создание окна для второго фрактала 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SecondFractal secondFractal = new SecondFractal();
            secondFractal.Show();
        }
        /// <summary>
        /// Создание окна для четвертого фрактала 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FourthFractal fourthFractal = new FourthFractal();
            fourthFractal.Show();
        }
        /// <summary>
        /// Создание окна для пятого фрактала 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FifthFractal fifthFractal = new FifthFractal();
            fifthFractal.Show();
        }
        /// <summary>
        /// Создание окна для третьего фрактала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ThirdFractal thirdFractal = new ThirdFractal();
            thirdFractal.Show();
        }
    }
    /// <summary>
    /// Класс для дополнительных,частовстречающихся методов 
    /// </summary>
    static public class AdditionalMethods
    {
        /// <summary>
        /// Создание экземпляра MessageBox
        /// </summary>
        /// <param name="messageToUser">Передача вспомогательных подсказок для пользователя через параметр метода в виде строки</param>
        public static void ShowMessageBox(string messageToUser)
        {
            MessageBox.Show(messageToUser);
        }
    }
}