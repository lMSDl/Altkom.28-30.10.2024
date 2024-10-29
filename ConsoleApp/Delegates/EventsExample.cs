namespace ConsoleApp.Delegates
{
    internal class EventsExample
    {
        public Action OddNumberDelegate { get; set; }

        public event Action OddNumbewrEvent;

        public EventsExample()
        {
            OddNumberDelegate += CountOdd;
            OddNumbewrEvent += CountOdd;
        }


        private int _counter;
        private void CountOdd()
        {
            _counter++;
        }

        public void Add(int a, int b)
        {
            int result = a + b;
            Console.WriteLine(result);

            if (result % 2 != 0)
            {
                OddNumberDelegate?.Invoke();
                OddNumbewrEvent?.Invoke();
            }
        }

        public void Test()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Add(i, j);
                }
            }

            Console.WriteLine("Counter: " + _counter);
        }
    }
}
