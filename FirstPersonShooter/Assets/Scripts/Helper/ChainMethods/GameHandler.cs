using System.Collections;
using UnityEngine;

namespace ExampleTemplate
{
    public abstract class GameHandler : IGameHandler
    {
        private IGameHandler _nextHandler;

        public IGameHandler SetNext(IGameHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(request);
            }
            else return null;
        }
    }
}