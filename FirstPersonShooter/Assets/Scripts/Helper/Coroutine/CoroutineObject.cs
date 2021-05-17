using System;
using UnityEngine;
using System.Collections;

namespace ExampleTemplate
{
    public sealed class CoroutineObject<T> : CoroutineBaseObject
    {
        #region Fields

        public Func<T,IEnumerator> Routine { get; private set; }

        public override event Action Finished;

        #endregion


        #region ClassLifeCycles

        public CoroutineObject(MonoBehaviour owner, Func<T,IEnumerator> routine)
        {
            Owner = owner;
            Routine = routine;
        }

        #endregion


        #region Methods

        private IEnumerator Process(T arg)
        {
            yield return Routine.Invoke(arg);

            Coroutine = null;

            Finished?.Invoke();
        }

        public void Start(T arg)
        {
            Stop();

            Coroutine = Owner.StartCoroutine(Process(arg));
        }

        public void Stop()
        {
            if (IsProcessing)
            {
                Owner.StopCoroutine(Coroutine);

                Coroutine = null;
            }
        }

        #endregion
    }
}
