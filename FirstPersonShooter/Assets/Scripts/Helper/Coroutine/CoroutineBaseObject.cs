using UnityEngine;
using Action = System.Action;

namespace ExampleTemplate
{
    public abstract class CoroutineBaseObject
    {
        #region Fields

        public MonoBehaviour Owner { get; protected set; }
        public Coroutine Coroutine { get; protected set; }

        public bool IsProcessing => Coroutine != null;

        public abstract event Action Finished;

        #endregion
    }
}
