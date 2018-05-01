using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Akyuu.UI.Components {
    public partial class NumericUpDown : UserControl {
        public int Value {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var control = d as NumericUpDown;
            int newValue = (int)e.NewValue;

            newValue = Math.Min(newValue, control.MaxValue);
            newValue = Math.Max(newValue, control.MinValue);

            control._Text = $"{newValue}";
        }

        public int MinValue {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0, OnMinValueChanged));

        private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var control = d as NumericUpDown;
            var newValue = (int)e.NewValue;

            if(control.Value < newValue)
                control.Value = newValue;
        }

        public int MaxValue {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericUpDown), new PropertyMetadata(10, OnMaxValueChanged));

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var control = d as NumericUpDown;
            var newValue = (int)e.NewValue;

            if(control.Value > newValue)
                control.Value = newValue;
        }

        public int Step {
            get { return (int)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }
        
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(int), typeof(NumericUpDown), new PropertyMetadata(1));

        private bool _Valid {
            get { return (bool)GetValue(_ValidProperty); }
            set { SetValue(_ValidProperty, value); }
        }

        /*
        private static readonly DependencyPropertyKey _ValidPropertyKey =
            DependencyProperty.RegisterReadOnly("_Valid", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(true));

        public static readonly DependencyProperty _ValidProperty = _ValidPropertyKey.DependencyProperty;
        */

        private static readonly DependencyProperty _ValidProperty =
            DependencyProperty.Register("_Valid", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(true));

        private string _Text {
            get { return (string)GetValue(_TextProperty); }
            set { SetValue(_TextProperty, value); }
        }

        private static readonly DependencyProperty _TextProperty =
            DependencyProperty.Register("_Text", typeof(string), typeof(NumericUpDown), new PropertyMetadata("", OnTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var control = d as NumericUpDown;
            var newValue = (string)e.NewValue;

            if(int.TryParse(newValue, out int result) && result >= control.MinValue && result <= control.MaxValue) {
                control._Valid = true;
                control.Value = result;
            } else {
                control._Valid = false;
            }
        }

        public NumericUpDown() {
            InitializeComponent();
        }

        private void Increment_Click(object sender, RoutedEventArgs e) {
            Value = Math.Min(MaxValue, Value + Step);
        }

        private void Decrement_Click(object sender, RoutedEventArgs e) {
            Value = Math.Max(MinValue, Value - Step);
        }
    }
}
