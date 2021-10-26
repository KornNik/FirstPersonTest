using System;
using UnityEngine;

namespace ExampleTemplate
{
    public sealed class GranadeService : Service
    {
        #region Fields

        public static Action<int> GranadeChanged;

        private GranadeBehaviour _granade;
        private bool _isGranade;

        #endregion


        #region Properties

        public GranadeBehaviour Granade { get { return _granade; } private set { } }
        public bool IsGranade { get { return _isGranade; } private set { } }

		#endregion


		#region Methods

		public void On(GranadeBehaviour granade)
		{
			if (_isGranade) return;
			_isGranade = true;
			_granade = granade as GranadeBehaviour;
			if (_granade == null) return;
			if (!_granade.IsReady) { return; }
			_granade.IsVisible = true;
			_granade.IsColliderActive = true;

		}

		public void Off()
		{
            if (!_isGranade) return;
            if (!_granade.IsReady) { return; }
			_isGranade = false;
			_granade.IsVisible = false;
			_granade.IsColliderActive = false;

		}

		#endregion
	}
}