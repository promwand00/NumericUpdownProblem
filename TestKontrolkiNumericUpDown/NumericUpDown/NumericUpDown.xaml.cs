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

namespace TestKontrolkiNumeriUpDown.NumericUpDown
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public double MinValue { get; set; } = 5D;

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double),typeof(NumericUpDown), 
            new FrameworkPropertyMetadata(
                0D, 
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnMyPropertyChanged,
                OnCoerceValue,
                false,
                UpdateSourceTrigger.Explicit
                ));

        private static object OnCoerceValue(DependencyObject d, object baseValue)
        {
            NumericUpDown nud = d as NumericUpDown;

            if (((double)baseValue) < nud.MinValue)
            {
                //OnMyPropertyChanged
                return nud.MinValue;
            }
            return baseValue;
        }
        private static void OnMyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            NumericUpDown numericUpDown = d as NumericUpDown;

            if (numericUpDown == null)
                return;

            BindingExpression bindingExpression = numericUpDown.GetBindingExpression(NumericUpDown.ValueProperty);
            if (bindingExpression != null)
                bindingExpression.UpdateSource();

            ((NumericUpDown)d).txtBoxValue.Text = ((double)e.NewValue).ToString();
        }

        public NumericUpDown()
        {
            InitializeComponent();
        }


        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            Value += 100;
        }

        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            Value -= 100;
        }



    }
}
