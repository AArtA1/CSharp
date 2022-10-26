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
    /// Interaction logic for FourthFractal.xaml
    /// </summary>
    public partial class FourthFractal : Window
    {
        // Переменная для прохода по рекурсии.
        int i;
        // Глубина рекурсии заданная пользователем.
        int recursionDepth;
        // Максимальная глубина рекурсии.
        readonly int depthMax = 10;

        /// <summary>
        /// Метод для инициализации окна.
        /// </summary>
        public FourthFractal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Начало отрисовки фрактала.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartAnimation(object sender, EventArgs e)
        {
            if (i > recursionDepth)
            {
                CompositionTarget.Rendering -= StartAnimation;
            }
            else
            {
                DrawTrinagle(canvas1, i, new Triangle(canvas1.Width / 6, canvas1.Height * 4 / 5, 5 * canvas1.Width / 6, 4 * canvas1.Width / 5, canvas1.Width / 2, 4 * canvas1.Height / 5 - canvas1.Width * Math.Sqrt(3) / 3));
                i += 1;
            }
        }
        /// <summary>
        /// Рекурсивная функция для отрисвоки фрактала.
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="depth"></param>
        /// <param name="triangle"></param>
        private void DrawTrinagle(Canvas canvas, int depth, Triangle triangle)
        {
            canvas1.Children.Add(new Line { Stroke = Brushes.Gray, X1 = triangle.x1, Y1 = triangle.y1, X2 = triangle.x2, Y2 = triangle.y2});
            canvas1.Children.Add(new Line { Stroke = Brushes.Gray, X1 = triangle.x1, Y1 = triangle.y1,X2= triangle.x3,Y2 = triangle.y3 });
            canvas1.Children.Add(new Line { Stroke = Brushes.Gray, X1 = triangle.x2, Y1 = triangle.y2, X2= triangle.x3, Y2 = triangle.y3});
            if (depth > 1)
            {
                DrawTrinagle(canvas, depth - 1, new Triangle(triangle.x1, triangle.y1, triangle.middleLineX12, triangle.middleLineY12, triangle.middleLineX13, triangle.middleLineY13));
                DrawTrinagle(canvas, depth - 1, new Triangle(triangle.middleLineX12, triangle.middleLineY12, triangle.x2, triangle.y2, triangle.middleLineX23, triangle.middleLineY23));
                DrawTrinagle(canvas, depth - 1, new Triangle(triangle.middleLineX13, triangle.middleLineY13, triangle.middleLineX23, triangle.middleLineY23, triangle.x3, triangle.y3));

            }
            else
            {
                return;
            }
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
        /// <summary>
        /// Проверка корректности ввода после заполнения TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int tempRecursionDepth) && tempRecursionDepth <= depthMax && tempRecursionDepth > 0)
            {
                i = 1;
                recursionDepth = tempRecursionDepth;
                canvas1.Children.Clear();
                CompositionTarget.Rendering += StartAnimation;
            }
            else
            {
                AdditionalMethods.ShowMessageBox("Некорректный ввод для глубины рекурсии(нижнее поле для ввода).\nКорректным вводом считается целое число на промежутке [1,10]");
            }
        }
    }
    /// <summary>
    /// Вспомогательный класс отрисовки треугольника.
    /// </summary>
    class Triangle
    {
        /// <summary>
        /// Координаты треугольника.
        /// </summary>
        public double x1, y1, x2, y2, x3, y3;
        /// <summary>
        /// Координаты общих точек средних линий и сторон равностороннего треугольника.
        /// </summary>
        public double middleLineX12, middleLineY12, middleLineX13, middleLineY13, middleLineX23, middleLineY23;
        /// <summary>
        /// Инициализация координат и средних линий.
        /// </summary>
        /// <param name="x1">Координата x1</param>
        /// <param name="y1">Координата y1</param>
        /// <param name="x2">Координата x2</param>
        /// <param name="y2">Координата y2</param>
        /// <param name="x3">Координата x3</param>
        /// <param name="y3">Координата y3</param>
        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.x3 = x3;
            this.y3 = y3;
            middleLineX12 = x1 + (x2 - x1) / 2;
            middleLineY12 = y1;
            middleLineX13 = x1 + ((x2 - x1) * Math.Cos(Math.PI / 3)) / 2;
            middleLineY13 = y1 - ((x2 - x1) * Math.Sin(Math.PI / 3))/2;
            middleLineX23 = middleLineX13+(x2-x1)/2;
            middleLineY23 = middleLineY13;
        }
    }
}