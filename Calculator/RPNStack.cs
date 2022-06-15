using Calculator.RPNComponents;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Calculator
{
    internal class RPNStack : IRPNStack
    {
        private readonly List<ICalculationTarget> _targets = new();

        public ICalculationTarget Pop()
        {
            var item = _targets.LastOrDefault();

            if (item == null)
                throw new InvalidOperationException("Itemが空です。");

            _targets.RemoveAt(_targets.Count - 1);
            return item;
        }

        public void Push(ICalculationTarget item)
            => _targets.Add(item);

        public bool TryPeek([NotNullWhen(true)] out ICalculationTarget item)
        {
            var targetItem = _targets.ElementAtOrDefault(_targets.Count - 1);

            if (targetItem is not null)
            {
                item = targetItem;
                return true;
            }

            item = default!;
            return false;
        }

        public ICalculationTarget[] ToArray() => _targets.ToArray();

        IEnumerator IEnumerable.GetEnumerator() => new RPNStackEnumerator(this);

        public IEnumerator<ICalculationTarget> GetEnumerator() => new RPNStackEnumerator(this);

        public class RPNStackEnumerator : IEnumerator<ICalculationTarget>
        {
            private int currentIndex;

            private readonly RPNStack _stack;

            public RPNStackEnumerator(RPNStack stack)
            {
                _stack = stack;
                currentIndex = 0;
            }

            public ICalculationTarget Current
            {
                get
                {
                    if (currentIndex < 0 || currentIndex >= _stack._targets.Count)
                        throw new ArgumentOutOfRangeException();
                    return _stack._targets[currentIndex];
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                currentIndex = 0;
                GC.SuppressFinalize(this);
            }

            public bool MoveNext()
            {
                var nextIndex = currentIndex + 1;
                if (nextIndex < 0 || nextIndex >= _stack._targets.Count)
                    return false;

                currentIndex++;
                return true;
            }

            public void Reset() => currentIndex = 0;
        }
    }
}