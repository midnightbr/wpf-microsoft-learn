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

namespace CalculadoraWpf
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private double? FirstNumber { get; set; }

        private double? SecondNumber { get; set; }

        private Func<double, double, double> selectedMathFunction { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }


        // Criando os métodos da calculadora
        private double Add(double a, double b)
        {
            double result = a + b;
            return result;
        }

        private double Subtract(double a, double b)
        {
            double result = a - b;
            return result;
        }

        private double Multiply(double a, double b)
        {
            double result = a * b;
            return result;
        }

        private double Divide(double a, double b)
        {
            double result = a / b;
            return result;
        }

        // Obtendo o primeiro valor
        private void FirstNumberBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstNumberBox?.Text))
            {
                FirstNumber = null;
                return;
            }

            if (double.TryParse(FirstNumberBox?.Text, out double parsedNumber))
            {
                FirstNumber = parsedNumber;
            }

            else
            {
                FirstNumberBox.Text = FirstNumberBox.Text.TrimEnd(FirstNumberBox.Text.LastOrDefault());
            }
        }

        // Obtendo a operação escolhida pela usuario
        private void RadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;

            string radioButtonContent = radioButton?.Content?.ToString();

            switch (radioButtonContent)
            {
                case "Add":
                    selectedMathFunction = Add;
                    break;
                case "Subtract":
                    selectedMathFunction = Subtract;
                    break;
                case "Multiply":
                    selectedMathFunction = Multiply;
                    break;
                case "Divide":
                    selectedMathFunction = Divide;
                    break;
                default:
                    selectedMathFunction = null;
                    break;
            }
        }

        // Obtendo o segundo valor para a operação
        private void SecondNumberBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SecondNumberBox?.Text))
            {
                SecondNumber = null;
                return;
            }

            if (double.TryParse(SecondNumberBox?.Text, out double parsedNumber))
            {
                SecondNumber = parsedNumber;
            }

            else
            {
                SecondNumberBox.Text = SecondNumberBox.Text.TrimEnd(SecondNumberBox.Text.LastOrDefault());
            }
        }

        // Slider para selecionar o segundo valor
        private void SecondNumberSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SecondNumberBox.Text = e.NewValue.ToString("N");
        }


        // Coletando se o usuario quer o resultado com a data atual ou não
        private void IncludeDateCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            CalculationDatePicker.Visibility = Visibility.Visible;
            CalculationDatePicker.SelectedDate = DateTime.Now;
        }

        private void IncludeDateCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            CalculationDatePicker.Visibility = Visibility.Collapsed;
        }

        private void EqualsButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (FirstNumber == null || SecondNumber == null)
            {
                MessageBox.Show("You need to set both numbers to calculate a result.");
                return;
            }

            if (SecondNumber == 0 && selectedMathFunction == Divide)
            {
                MessageBox.Show("You cannot divide from zero, please pick a differente 2nd number.");
                return;
            }

            // Realizando a operação selecionada pelo usuario
            double result = selectedMathFunction((double)FirstNumber, (double)SecondNumber);

            // Imprimindo o resultado
            if (IncludeDateCheckBox.IsChecked == true)
            {
                ResultsTextBlock.Text = $"Result: {result:N2} Date: {CalculationDatePicker.SelectedDate:d}";
            }
            else
            {
                ResultsTextBlock.Text = $"Result: {result:N2}";
            }
        }
    }
}
