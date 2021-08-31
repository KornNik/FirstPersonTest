using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace ExampleTemplate
{
    public sealed class SequenceSettings
    {
        #region Fields

        private Sequence _sequence;
        private SettingsPanelTween _panel;

        #endregion

        #region ClassLifeCycle

        public SequenceSettings(SettingsPanelTween panel)
        {
            _panel = panel;
        }

        #endregion


        #region Methods

        public Sequence Move(MoveMode mode)
        {
            float timeScale = 1.0f;

            if (_sequence != null)
            {
                timeScale = _sequence.position / _sequence.Duration();
                _sequence.Kill();
            }

            _sequence = DOTween.Sequence();
            _sequence.Join(_panel.Move(mode, timeScale));
            _sequence.AppendCallback(() =>
            {
                _sequence = null;

            });

            return _sequence;
        }

        #endregion
    }
}