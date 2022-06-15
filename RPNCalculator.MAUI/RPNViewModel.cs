using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Calculator;

namespace RPNCalculator.MAUI
{
    internal class RPNViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ICalculator _calculator = CalculatorFactory.CreateCalculator();

        private string _input;
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

        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                if(_error != value)
                {
                    _error = value;
                    PropertyChanged?.Invoke(this, new(nameof(Error)));
                    PropertyChanged?.Invoke(this, new(nameof(HasError)));
                }
            }
        }

        public bool HasError { get => !string.IsNullOrEmpty(_error); }

        public ICommand Push { get; }

        public ICommand Execute { get; }

        public ICommand Pop { get; }

        public ICommand Clear { get; }

        public ObservableCollection<string> Items { get; } = new();

        public RPNViewModel()
        {
            _calculator.StackChanged += CalculatorStackChanged;

            Push = new DelegateCommand(_ => {
                Error = "";
                try
                {
                    _calculator.Push(Input);
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
                    _calculator.Run();
                }
                catch (Exception e)
                {
                    Error = e.Message;

                }
            });
            Pop = new DelegateCommand(_ => {
                Error = "";
                Input = _calculator.Pop();
            });
            Clear = new DelegateCommand(_ => {
                Error = "";
                _calculator.Clean();
                Input = "";
            });
        }

        private void CalculatorStackChanged(object sender, string[] e)
        {
            Items.Clear();
            foreach (var item in e.Where(each => !string.IsNullOrEmpty(each)))
                Items.Add(item);
        }
    }
}
