using Calculator;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RPNCalculator.WPF
{
    public class RPNViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

        private string _input = "";

        public string Input {
            get => _input;
            set {
                if (_input != value)
                {
                    _input = value;
                    PropertyChanged?.Invoke(this, new(nameof(Input)));
                }
            }
        }

        private string _error = "";
        public string Error {
            get => _error;
            set {
                if (_error != value)
                {
                    _error = value;
                    PropertyChanged?.Invoke(this, new(nameof(Error)));
                    PropertyChanged?.Invoke(this, new(nameof(ErrorVisibility)));
                }
            }
        }

        public Visibility ErrorVisibility {
            get => string.IsNullOrEmpty(Error) ? Visibility.Collapsed : Visibility.Visible;
        }

        public ICalculator Calculator { get; }

        public RPNViewModel(ICalculator calculator)
        {
            Calculator = calculator;
            Calculator.StackChanged += CalculatorStackChanged;
            Push = new DelegateCommand(_ => {
                Error = "";
                try
                {
                    Calculator.Push(Input);
                }
                catch (Exception e)
                {
                    Error = e.Message;

                }
                Input = "";
            });
            Execute = new DelegateCommand(_ => {
                Error = "";
                try
                {
                    Calculator.Run();
                }
                catch (Exception e)
                {
                    Error = e.Message;

                }
            });
            Pop = new DelegateCommand(_ => {
                Error = "";
                Input = Calculator.Pop();
            });
            Clear = new DelegateCommand(_ => {
                Error = "";
                Calculator.Clean();
                Input = "";
            });
        }



        private void CalculatorStackChanged(object? sender, string[] e)
        {
            Items.Clear();
            Items.AddRange(e.Where(each => !string.IsNullOrEmpty(each)));
        }

        public ICommand Push { get; }

        public ICommand Execute { get; }

        public ICommand Pop { get; }

        public ICommand Clear { get; }
    }
}
