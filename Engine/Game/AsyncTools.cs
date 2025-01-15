using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class AsyncTools
{
    public class State
    {
        public CancellationToken Token;
        public IEnumerator Current => Stack.Peek();
        public Stack<IEnumerator> Stack = new Stack<IEnumerator>();
        public bool Done => Stack.Count == 0;

        public State()
        {
        }

        public State(IEnumerator enumerator)
        {
            Stack.Push(enumerator);
        }

        public bool Tick(double delta)
        {
            if (Done)
                return true;
            try
            {
                Token.ThrowIfCancellationRequested();
                bool shouldMove = false;
                switch (Current.Current)
                {
                    case null:
                        shouldMove = true;
                        break;
                    case IEnumerator enumerator:
                        Stack.Push(enumerator);
                        return false;
                    case IAsyncStateCallback callback:
                        shouldMove = callback.Tick(this, delta);
                        break;
                }
                if (shouldMove)
                {
                    if (!Current.MoveNext())
                    {
                        Stack.Pop();
                        // Done = true;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                // Done = true;
                Stack.Clear();
                Log.Error(nameof(AsyncTools), e.ToString());
                return true;
            }
            return false;
        }
    }

    private List<State> states = new List<State>();

    public void Tick(double dt)
    {
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i] == null)
                continue;
            states[i].Tick(dt);
        }
        states.RemoveAll(x => x == null || x.Done);
    }

    public void Run(IEnumerator enumerator, CancellationToken ct = default)
    {
        states.Add(new State(enumerator)
        {
            Token = ct,
        });
    }
}

public interface IAsyncStateCallback
{
    bool Tick(AsyncTools.State state, double delta);
}

public class WaitForSeconds : IAsyncStateCallback
{
    public double Time { get; private set; }
    public double TimeLeft { get; private set; }

    public WaitForSeconds(double time)
    {
        Time = time;
        TimeLeft = time;
    }

    public bool Tick(AsyncTools.State state, double delta)
    {
        TimeLeft -= delta;
        return TimeLeft <= 0f;
    }
}