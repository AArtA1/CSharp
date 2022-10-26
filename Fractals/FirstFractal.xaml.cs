using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals3._0
{
    /// <summary>
    /// Interaction logic for FirstFractal.xaml
    /// </summary>
    public partial class FirstFractal : Window
    {
        // Коэффицент для последующих дочерних ветвей.
        double lengthScale;
        // Левый угол ветви.
        double leftAngle;
        // Правый угол ветви.
        double rightAngle;
        // Переменная для прохода по глубине рекурсии.
        int i;
        // Глубина рекурсии пользователя.
        int recursionDepth;
        // Максимальная глубина рекурсии.
        readonly int depthMax = 10;

        /// <summary>
        /// Инициализация окна
        /// </summary>
        public FirstFractal()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Проверка на корректность при заполнении всех TextBox для генерации фрактала.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double tempLengthScale) && tempLengthScale > 0 && tempLengthScale <= 0.7)
            {
                lengthScale = tempLengthScale;
                if (double.TryParse(textBox2.Text, out double tempRightAngle) && tempRightAngle > 0 && tempRightAngle <= 90)
                {
                    rightAngle = tempRightAngle * 2 * Math.PI / 360;
                    if (double.TryParse(textBox3.Text, out double tempLeftAngle) && tempLeftAngle > 0 && tempLeftAngle <= 90)
                    {
                        leftAngle = tempLeftAngle * 2 * Math.PI / 360;
                        if (int.TryParse(textBox4.Text, out int tempRecursionDepth) && tempRecursionDepth <= depthMax && tempRecursionDepth > 0)
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
                        AdditionalMethods.ShowMessageBox("Некорректный ввод для левого угла(второе снизу поле для ввода).\nКорректным вводом считается угол на полуинтервале (0,90] градусов");
                    }
                }
                else
                {
                    AdditionalMethods.ShowMessageBox("Некорректный ввод для правого угла(второе сверху поле для ввода).\nКорректным вводом считается угол на полуинтервале (0,90] градусов");
                }
            }
            else
            {
                AdditionalMethods.ShowMessageBox("Некорректный ввод для коэфффицента (первое сверху поле для ввода).\nКорректным вводом считается число на полуинтервале (0,0.7]");
            }
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
                DrawBinaryTree(canvas1, i, new Point(canvas1.Width / 2, 0.99 * canvas1.Height), 0.2 * canvas1.Width, -Math.PI / 2);
                i += 1;
            }
        }
        /// <summary>
        /// Рекурсивная функция.
        /// </summary>
        /// <param name="canvas">Текущий Canvas для отрисовки на нем</param>
        /// <param name="depth">Оставшяяся глубина рекурсии</param>
        /// <param name="pt">Точка для отрисовки отрезка</param>
        /// <param name="length">Длина отризка</param>
        /// <param name="theta">Угол для отрисовки</param>
        private void DrawBinaryTree(Canvas canvas, int depth, Point pt, double length, double theta) 
        {
            double x1 = pt.X + length * Math.Cos(theta); 
            double y1 = pt.Y + length * Math.Sin(theta);
            Line line = new Line();
            line.Stroke = Brushes.Gray;
            line.X1 = pt.X;
            line.Y1 = pt.Y;
            line.X2 = x1;
            line.Y2 = y1;
            canvas.Children.Add(line);

            if (depth > 1)
            {
                DrawBinaryTree(canvas, depth - 1,
                new Point(x1, y1),
                length * lengthScale, theta + rightAngle);
                DrawBinaryTree(canvas, depth - 1,
                new Point(x1, y1),
                length * lengthScale, theta - leftAngle);
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
}
