using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Klukulator123
{
    public partial class MainPage : ContentPage
    {
        private CalculatorViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new CalculatorViewModel();
            BindingContext = viewModel;
        }
    }

    public class CalculatorViewModel : BindableObject
    {
        private string displayText = "0";
        private string currentInput = "";
        private ObservableCollection<string> history = new ObservableCollection<string>();

        public ICommand DigitCommand { get; private set; }
        public ICommand OperationCommand { get; private set; }
        public ICommand EqualsCommand { get; private set; }
        public ICommand PowerCommand { get; private set; }
        public ICommand ShowHistoryCommand { get; private set; }

        public string DisplayText
        {
            get { return displayText; }
            set
            {
                displayText = value;
                OnPropertyChanged();
            }
        }

        public CalculatorViewModel()
        {
            DigitCommand = new Command<string>(HandleDigit);
            OperationCommand = new Command<string>(HandleOperation);
            EqualsCommand = new Command(HandleEquals);
            PowerCommand = new Command(HandlePower);
            ShowHistoryCommand = new Command(ShowHistory);
        }

        private void HandleDigit(string digit)
        {
            if (displayText == "0" || currentInput == "Błąd")
            {
                currentInput = digit;
            }
            else
            {
                currentInput += digit;
            }

            DisplayText = currentInput;
        }

        private void HandleOperation(string operation)
        {
            if (currentInput != "")
            {
                history.Add(currentInput);
                history.Add(operation);
                currentInput = "";
            }
        }

        private void HandleEquals()
        {
            if (currentInput != "")
            {
                history.Add(currentInput);
                currentInput = "";
            }

            try
            {
                var result = EvaluateExpression();
                history.Add("=");
                history.Add(result.ToString());
                currentInput = result.ToString();
            }
            catch (Exception)
            {
                currentInput = "Błąd";
            }

            DisplayText = currentInput;
        }

        private void HandlePower()
        {
            if (currentInput != "")
            {
                history.Add(currentInput);
                history.Add("^");
                currentInput = "";
            }
        }

        private double EvaluateExpression()
        {
           
            return Convert.ToDouble(new System.Data.DataTable().Compute(string.Join("", history), ""));
        }

        private void ShowHistory()
        {
            
        }

    }
}
