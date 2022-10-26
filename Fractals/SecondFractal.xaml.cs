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
    /// Interaction logic for SecondFractal.xaml
    /// </summary>
    public partial class SecondFractal : Window
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
        public SecondFractal()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Кнопка для проверки коррекности ввода и дальнейшей отрисовки. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (int.TryParse(textBox1.Text, out int tempRecursionDepth) && tempRecursionDepth <= depthMax && tempRecursionDepth > 0)
            {
                i = 1;
                recursionDepth = tempRecursionDepth;
                CompositionTarget.Rendering += StartAnimation;
            }
            else
            {
                AdditionalMethods.ShowMessageBox("Некорректный ввод для глубины рекурсии(нижнее поле для ввода).\nКорректным вводом считается целое число на промежутке [1,10]");
            }
        }
        /// <summary>
        /// Начало отрисовки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartAnimation(object sender, EventArgs e)
        {
            if (i == 1)
            {
                Rectangle rect = new Rectangle();
                rect.Width = canvas1.Width*4/6;
                rect.Height = canvas1.Width*4/6;
                rect.Fill = Brushes.Gray;
                canvas1.Children.Add(rect);
                Canvas.SetTop(rect, canvas1.Width/6);
                Canvas.SetLeft(rect, canvas1.Width/6);
            }
            if (i > recursionDepth)
            {
                CompositionTarget.Rendering -= StartAnimation;
            }
            else
            {
                DrawSquare(canvas1,i,new Square(canvas1.Width/6,canvas1.Width/6,canvas1.Width*4/6));
                i += 1;
            }
        }
        /// <summary>
        /// Рекурсивная функция.
        /// </summary>
        /// <param name="canvas">Текущее поле для отрисовки</param>
        /// <param name="depth">Глубина рекурсии</param>
        /// <param name="square">Координаты квадрата</param>
        public void DrawSquare(Canvas canvas,int depth,Square square)
        {
            Rectangle rect = new Rectangle();
            rect.Width = square.side/3;
            rect.Height = square.side/3;
            rect.Fill = Brushes.White;
            canvas1.Children.Add(rect);
            Canvas.SetTop(rect, square.x1+square.side/3);
            Canvas.SetLeft(rect, square.y1+square.side/3);
            if (depth > 1)
            {
                DrawSquare(canvas, depth - 1, new Square(square.x1, square.y1, square.side / 3));
                DrawSquare(canvas, depth - 1, new Square(square.x1+square.side/3, square.y1, square.side / 3));
                DrawSquare(canvas, depth - 1, new Square(square.x1 +square.side*2/3, square.y1, square.side / 3));
                DrawSquare(canvas, depth - 1, new Square(square.x1, square.y1 + square.side/3, square.side / 3));
                DrawSquare(canvas, depth - 1, new Square(square.x1 + square.side*2/3 , square.y1 + square.side/3, square.side / 3));
                DrawSquare(canvas, depth - 1, new Square(square.x1, square.y1, square.side / 3));
                DrawSquare(canvas, depth - 1, new Square(square.x1,square.y1 +square.side*2/3,square.side/3));
                DrawSquare(canvas, depth - 1, new Square(square.x1 + square.side/3, square.y1+square.side*2/3, square.side / 3));
                DrawSquare(canvas, depth - 1, new Square(square.x1 + square.side*2/3, square.y1 + square.side*2/3, square.side / 3));
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
    }
    /// <summary>
    /// Вспомогательный класс для инициализации квадрата.
    /// </summary>
    public class Square
    {
        // Координаты верхнего левого угла квадрата и длины стороны.
        public double x1, y1,side;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1">координата x левого верхнего угла квадрата</param>
        /// <param name="y1">координата y левого верхнего угла квадрата</param>
        /// <param name="side">длина стороны квадрата</param>
        public Square(double x1, double y1,double side)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.side = side;
        }

    }
}
