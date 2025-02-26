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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR3part2_Tsyguleva_Zokirova
{
  
        public partial class MainWindow : Window
        {
            public MainWindow()
            {
                InitializeComponent();

                // Подписка на событие изменения выбора RadioButton
                rbRectangle.Checked += RadioButton_Checked;
                rbCircle.Checked += RadioButton_Checked;
                rbTriangle.Checked += RadioButton_Checked;
            }

            // Обработчик события выбора фигуры
            private void RadioButton_Checked(object sender, RoutedEventArgs e)
            {
                // Показываем панель ввода
                inputPanel.Visibility = Visibility.Visible;

                // Скрываем все поля ввода
                rectangleInputs.Visibility = Visibility.Collapsed;
                circleInputs.Visibility = Visibility.Collapsed;
                triangleInputs.Visibility = Visibility.Collapsed;

                // Показываем только нужные поля в зависимости от выбранной фигуры
                if (rbRectangle.IsChecked == true)
                {
                    rectangleInputs.Visibility = Visibility.Visible;
                }
                else if (rbCircle.IsChecked == true)
                {
                    circleInputs.Visibility = Visibility.Visible;
                }
                else if (rbTriangle.IsChecked == true)
                {
                    triangleInputs.Visibility = Visibility.Visible;
                }
            }

            // Проверка ввода данных
            private bool ValidateInput()
            {
                if (rbRectangle.IsChecked == true)
                {
                    // Проверка для прямоугольника
                    if (string.IsNullOrWhiteSpace(txtSide1.Text) || string.IsNullOrWhiteSpace(txtSide2.Text))
                    {
                        MessageBox.Show("Заполните все поля для прямоугольника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

                    // Проверка на числовые значения
                    if (!double.TryParse(txtSide1.Text, out double side1) || !double.TryParse(txtSide2.Text, out double side2))
                    {
                        MessageBox.Show("Введите числовые значения для сторон прямоугольника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

                    // Проверка на положительные значения
                    if (side1 <= 0 || side2 <= 0)
                    {
                        MessageBox.Show("Стороны прямоугольника должны быть больше нуля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else if (rbCircle.IsChecked == true)
                {
                    // Проверка для круга
                    if (string.IsNullOrWhiteSpace(txtRadius.Text))
                    {
                        MessageBox.Show("Введите радиус круга!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

                    // Проверка на числовое значение
                    if (!double.TryParse(txtRadius.Text, out double radius))
                    {
                        MessageBox.Show("Введите числовое значение для радиуса!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

                    // Проверка на положительное значение
                    if (radius <= 0)
                    {
                        MessageBox.Show("Радиус должен быть больше нуля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else if (rbTriangle.IsChecked == true)
                {
                    // Проверка для треугольника
                    if (string.IsNullOrWhiteSpace(txtA.Text) || string.IsNullOrWhiteSpace(txtB.Text) || string.IsNullOrWhiteSpace(txtC.Text))
                    {
                        MessageBox.Show("Заполните все стороны треугольника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

                    // Проверка на числовые значения
                    if (!double.TryParse(txtA.Text, out double a) || !double.TryParse(txtB.Text, out double b) || !double.TryParse(txtC.Text, out double c))
                    {
                        MessageBox.Show("Введите числовые значения для сторон треугольника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

                    // Проверка на положительные значения
                    if (a <= 0 || b <= 0 || c <= 0)
                    {
                        MessageBox.Show("Стороны треугольника должны быть больше нуля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

                // Проверка на существование треугольника (по неравенству треугольника)
                if (!(a + b > c && a + c > b && b + c > a))
                    {
                        MessageBox.Show("Треугольник с такими сторонами не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    // Если ни одна фигура не выбрана
                    MessageBox.Show("Выберите фигуру!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            }

            // Обработчик нажатия на кнопку "Вычислить"
            private void btnCalculate_Click(object sender, RoutedEventArgs e)
            {
                // Проверка ввода
                if (!ValidateInput()) return;

                try
                {
                    double result = 0;

                    // Вычисление периметра для прямоугольника
                    if (rbRectangle.IsChecked == true)
                    {
                        double side1 = double.Parse(txtSide1.Text);
                        double side2 = double.Parse(txtSide2.Text);
                        result = 2 * (side1 + side2);
                    }
                    // Вычисление длины окружности для круга
                    else if (rbCircle.IsChecked == true)
                    {
                        double radius = double.Parse(txtRadius.Text);
                        result = 2 * Math.PI * radius;
                    }
                    // Вычисление периметра для треугольника
                    else if (rbTriangle.IsChecked == true)
                    {
                        double a = double.Parse(txtA.Text);
                        double b = double.Parse(txtB.Text);
                        double c = double.Parse(txtC.Text);
                        result = a + b + c;
                    }

                // Вывод результата
                tbResult.Text = $"Периметр = {Math.Floor(result * 10) / 10}"; // Ошибка: округление вниз до одного знака после запятой
            }
                catch (Exception ex)
                {
                    // Обработка исключений
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }