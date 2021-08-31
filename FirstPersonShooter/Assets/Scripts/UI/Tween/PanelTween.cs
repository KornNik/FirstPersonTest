using UnityEngine;
using DG.Tweening;

namespace ExampleTemplate
{
    public sealed class SettingsPanelTween
    {
        static readonly Vector2 InAnchorMin = new Vector2(0.0f, 0.5f);
        static readonly Vector2 InAnchorMax = new Vector2(1.0f, 0.5f);
        static readonly Vector2 OutAnchorMin = new Vector2(0.0f,1.0f);
        static readonly Vector2 OutAnchorMax = new Vector2(1.0f, 1.0f);

        readonly RectTransform _moveRoot;

        public SettingsPanelTween(RectTransform moveRoot)
        {
            _moveRoot = moveRoot;
        }

        public void GoToEnd(MoveMode mode)
        {
            switch (mode)
            {
                case MoveMode.Show:
                    _moveRoot.anchorMin = InAnchorMin;
                    _moveRoot.anchorMax = InAnchorMax;
                    break;
                case MoveMode.Hide:
                    _moveRoot.anchorMin = OutAnchorMin;
                    _moveRoot.anchorMax = OutAnchorMax;
                    break;
                default:
                    break;

            }
        }
        public Sequence Move(MoveMode mode, float timeScale)
        {
            Vector2 anchorMin = Vector2.zero;
            Vector2 anchorMax = Vector2.zero;

            switch (mode)
            {
                case MoveMode.Show:
                    anchorMin = InAnchorMin;
                    anchorMax = InAnchorMax;
                    break;
                case MoveMode.Hide:
                    anchorMin = OutAnchorMin;
                    anchorMax = OutAnchorMax;
                    break;
                default:
                    break;
            }

            const float totalMoveDuration = 0.5f;
            const Ease moveEase = Ease.InOutBack;
            return DOTween.Sequence()
                .Append(_moveRoot.DOAnchorMin(anchorMin, totalMoveDuration * timeScale).SetEase(moveEase))
                .Join(_moveRoot.DOAnchorMax(anchorMax,totalMoveDuration*timeScale).SetEase(moveEase));
        }
    }
}