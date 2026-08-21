using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ArtSystem
{
    /// <summary> ActionsProcessor use isolated thread to execute action in queue. </summary>
    public class ActionsProcessor
    {
        private ActionsProcessor()
        {
            _thread = new Thread(Run)
            {
                Name = "ActionsManagerThread",
            };
            _thread.Start();
        }

        private static object _objectLock = new object();

        private static ActionsProcessor _sgt;

        public static ActionsProcessor Sgt
        {
            get
            {
                lock (_objectLock)
                {
                    if (_sgt == null)
                    {
                        _sgt = new ActionsProcessor();
                    }
                }

                return _sgt;
            }
        }

        private Thread _thread;

        private bool _isTerminate = false;

        private AutoResetEvent _resetEvent = new AutoResetEvent(false);

        private object _queueLock = new object();

        private List<Action> _actionQueue = new List<Action>();

        private void Run()
        {
            while (!_isTerminate)
            {
                Monitor.Enter(_queueLock);

                var action = _actionQueue.FirstOrDefault();

                Monitor.Exit(_queueLock);

                if (action == null)
                {
                    _resetEvent.WaitOne();
                }
                else
                {
                    action.Invoke();

                    lock (_queueLock)
                    {
                        _actionQueue.RemoveAt(0);
                    }
                }
            }
        }

        /// <summary>
        /// To enqueue action in processor to execute.
        /// </summary>
        /// <param name="act"></param>
        public void Enqueue(Action act)
        {
            Monitor.Enter(_objectLock);

            _actionQueue.Add(act);

            Monitor.Exit(_objectLock);

            _resetEvent.Set();
        }

        internal void Terminate()
        {
            _isTerminate = true;

            _resetEvent.Set();
        }
    }
}
