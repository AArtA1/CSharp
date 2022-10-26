using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fractals3._0
{
    /// <summary>
    /// Interaction logic for ThirdFractal.xaml
    /// </summary>
    public partial class ThirdFractal : Window
    {
        // Глубина рекурсии,заданная пользователем.
        int recursionDepth;
        // Максимальная глубина рекурсии.
        readonly int depthMax = 10;
        /// <summary>
        /// Инициализация окна
        /// </summary>
        public ThirdFractal()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Проверка коррекности данных после ввода пользователя и нажатия кнопки генерации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int tempRecursionDepth) && tempRecursionDepth <= depthMax && tempRecursionDepth > 0)
            {
                recursionDepth = tempRecursionDepth;
                canvas1.Children.Clear();
                var point1 = new Point(canvas1.Width/6, canvas1.Width/2);
                var point2 = new Point(canvas1.Width*5/6, canvas1.Width/2);
                var point3 = new Point(canvas1.Width*3/6, canvas1.Width/2+ canvas1.Width*2*Math.Sqrt(3)/6);
                canvas1.Children.Add(new Line() { Stroke = Brushes.Gray, X1 = point1.X, Y1 = point1.Y, X2 = point2.X, Y2 = point2.Y });
                DrawPolyLine(point1, point2, point3, recursionDepth);
            }
            else
            {
                AdditionalMethods.ShowMessageBox("Некорректный ввод для глубины рекурсии(нижнее поле для ввода).\nКорректным вводом считается целое число на промежутке [1,10]");
            }
        }
        /// <summary>
        /// Рекурсивная функция для отрисовки кривой кохи.
        /// </summary>
        /// <param name="p1">Первая точка равностороннего треугольника</param>
        /// <param name="p2">Вторая точка равностороннего треугольника</param>
        /// <param name="p3">Третья точка равностороннего треугольника</param>
        /// <param name="depth">Глубина рекурсии</param>
        public void DrawPolyLine(Point p1, Point p2, Point p3, int depth)
        {
            if (depth > 0)
            {
                var p4 = new Point((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 = new Point((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                var ps = new Point((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn = new Point((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);
                canvas1.Children.Add(new Line() { Stroke = Brushes.Gray, X1 = p4.X, Y1 = p4.Y, X2 = pn.X, Y2 = pn.Y });
                canvas1.Children.Add(new Line() { Stroke = Brushes.Gray, X1 = p5.X, Y1 = p5.Y, X2 = pn.X, Y2 = pn.Y });
                canvas1.Children.Add(new Line() { Stroke = Brushes.White, X1 = p4.X, Y1 = p4.Y, X2 = p5.X, Y2 = p5.Y });
                DrawPolyLine(p4, pn, p5, depth - 1);
                DrawPolyLine(pn, p5, p4, depth - 1);
                DrawPolyLine(p1, p4, new Point((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3), depth - 1);
                DrawPolyLine(p5, p2, new Point((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3), depth - 1);
            }
            return;
        }
        /// <summary>
        /// Выделение всего текста при двойном клике по TextBox для удобства.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.SelectAll();
            e.Handled = true;
        }
    }
}
