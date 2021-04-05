using UnityEngine;

namespace ExampleTemplate
{
    public class WeaponController : IExecute, IInitialization, IListenerScreen
    {

        #region Fields

        private KeyCode _reloadWeapon = KeyCode.R;
        private CharacterData _characterData;
        private float _handWeight;
        private bool _isActive;

        #endregion

        #region IInitialization

        public void Initialization()
        {
            ScreenInterface.GetInstance().AddObserver(ScreenType.GameMenu, this);
            _characterData = Data.Instance.Character;
        }

        #endregion

        #region IExecute

        public void Execute()
        {
            if (!_isActive) return;
            if (!Services.Instance.WeaponService.IsWeapon) return;

            var tempWeapon = Services.Instance.WeaponService.Weapon;

            if (Input.GetAxis(AxisManager.FIRE1) != 0 && _handWeight!=0)
            {
                tempWeapon.Fire();
                WeaponService.OnAmmunitionChange?.Invoke(tempWeapon.CountClip, tempWeapon.Clip.CountAmmunition);
            }

            if (Input.GetKeyDown(_reloadWeapon))
            {
                Services.Instance.WeaponService.ReloadClip();
            }

            float newHandWeight = 0f;
            if (Input.GetAxis(AxisManager.FIRE2) != 0)
            {
                newHandWeight = 1f;
            }
            _handWeight = newHandWeight;
            _characterData.CharacterAnimationBehaviour.SetHandWeight(_handWeight);
        }

        #endregion


        #region IListenerScreen

        public void ShowScreen()
        {
            _isActive = true;
        }

        public void HideScreen()
        {
            _isActive = false;
        }

        #endregion
    }
}