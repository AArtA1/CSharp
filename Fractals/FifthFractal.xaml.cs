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
    /// Interaction logic for FifthFractal.xaml
    /// </summary>
    public partial class FifthFractal : Window
    {
        // Переменная для прохода по рекурсии.
        int i;
        // Глубина рекурсии заданная пользователем.
        int recursionDepth;
        // Расстояние между отрекзами итераций рекурсии.
        double lengthInLines;
        // Максимальная глубина рекурсии.
        readonly int depthMax = 10;
        /// <summary>
        /// Инициализация окна.
        /// </summary>
        public FifthFractal()
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
                DrawLineSegment(canvas1, i, new LineSegment(canvas1.Width/6,canvas1.Width/6,5*canvas1.Width/6,canvas1.Width/6));
                i += 1;
            }
        }
        /// <summary>
        /// Рекурсивная функция.
        /// </summary>
        /// <param name="canvas">Поле для отрисовки</param>
        /// <param name="depth">Глубина рекурсии</param>
        /// <param name="lineSegment">Координаты отрезка</param>
        private void DrawLineSegment(Canvas canvas, int depth, LineSegment lineSegment)
        {

            canvas1.Children.Add(new Line { StrokeThickness = 5, Stroke = Brushes.Gray, X1 = lineSegment.x1, Y1 = lineSegment.y1, X2 = lineSegment.x2, Y2 = lineSegment.y2 });
            if (depth > 1)
            {
                DrawLineSegment(canvas1, depth - 1, new LineSegment(lineSegment.x1, lineSegment.y1+lengthInLines, lineSegment.x1 + (lineSegment.x2 - lineSegment.x1) / 3, lineSegment.y2+lengthInLines));
                DrawLineSegment(canvas1, depth - 1, new LineSegment(lineSegment.x2 - (lineSegment.x2 - lineSegment.x1) / 3, lineSegment.y1 + lengthInLines, lineSegment.x2, lineSegment.y2+ lengthInLines));
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// Проверка корректности ввода при нажатии на кнопку старта отрисовки и дальнейшее начало отрисовки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double tmpLength) && tmpLength >= 10 && tmpLength <= 20)
            {
                lengthInLines = tmpLength;
                if (int.TryParse(textBox2.Text, out int tempRecursionDepth) && tempRecursionDepth <= depthMax && tempRecursionDepth > 0)
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
            else
            {
                    AdditionalMethods.ShowMessageBox("Некорректный ввод для расстояния между отрезками отераций(нижнее поле для ввода).\nКорректным вводом считается число на промежутке [10,20] в пикселях");
            }
        }
        /// <summary>
        /// Выделение всего текста при двойном клике по TextBox для удобства.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.SelectAll();
            e.Handled = true;
        }
    }
    /// <summary>
    /// Вспомогательный класс для хранения координат отрезков.
    /// </summary>
    class LineSegment
    {
        // Координаты отрезков.
        public double x1, y1, x2, y2;
        /// <summary>
        /// Инициализация координат отрезков.
        /// </summary>
        /// <param name="x1">Координата x1</param>
        /// <param name="y1">Координата y1</param>
        /// <param name="x2">Координата x2</param>
        /// <param name="y2">Координата y2</param>
        public LineSegment(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
        }
    }
}
