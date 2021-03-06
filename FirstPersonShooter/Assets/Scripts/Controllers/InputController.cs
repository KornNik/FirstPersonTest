using UnityEngine;
using System;

namespace ExampleTemplate
{
    public sealed class InputController : IExecute, IListenerScreen
    {
        #region Fields

        private readonly CharacterData _characterData;

        private bool _isActive;

        #endregion


        #region ClassLifeCycles

        public InputController()
        {
            _characterData = Data.Instance.Character;

            ScreenInterface.GetInstance().AddObserver(ScreenType.GameMenu, this);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!_isActive) return;

            if (Input.GetAxis(AxisManager.CANCEL) != 0)
            {
                ActiveMainMenu();
            }

            if (Input.GetAxis(AxisManager.FIRE1) != 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            Vector2 inputAxis;
            inputAxis.x = Input.GetAxis(AxisManager.HORIZONTAL);
            inputAxis.y = Input.GetAxis(AxisManager.VERTICAL);

            _characterData.CharacterBehaviour.CharacterMovement.Move(inputAxis);
            _characterData.CharacterBehaviour.CharacterMovement.GamingGravity();

            if (Input.GetAxis(AxisManager.JUMP) != 0)
            {
                _characterData.CharacterBehaviour.CharacterMovement.CharacterJump();
            }
            if (Input.GetAxis(AxisManager.MOUSE_SCROLL_WHEEL) > 0)
            {
                MouseScroll(MouseScrollWheel.Up);
            }
            if (Input.GetAxis(AxisManager.MOUSE_SCROLL_WHEEL) < 0)
            {
                MouseScroll(MouseScrollWheel.Down);
            }
            if (Input.GetKeyDown(KeyManager.RUN))
            {
                _characterData.CharacterBehaviour.CharacterMovement.Run();
            }

            if (Input.GetKeyDown(KeyManager.FIRST_WEAPON))
            {
                SelectWeapon(0);
            }
            if (Input.GetKeyDown(KeyManager.SECOND_WEAPON))
            {
                SelectWeapon(1);
            }
            if (Input.GetKeyDown(KeyManager.CANCEL_WEAPON))
            {
                Services.Instance.WeaponService.Off();
            }
            if (Input.GetKeyUp(KeyManager.FLASH_LIGHT_SWITCH))
            {
                SwitchFlashLight();
            }
            if (Input.GetKeyUp(KeyManager.GRANADE_SELECT))
            {
                SelectGranade();
            }
        }

        #endregion


        #region Methods

        private void ActiveMainMenu()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ScreenInterface.GetInstance().Execute(ScreenType.MainMenu);
        }

        private void SelectWeapon(int value)
        {

            var tempWeapon = _characterData.CharacterBehaviour.Inventory.SelectWeapon(value);
            if (tempWeapon is WeaponBehaviour)
            {
                SelectWeapon(tempWeapon);
            }
        }

        private void MouseScroll(MouseScrollWheel value)
        {
            var tempWeapon = _characterData.CharacterBehaviour.Inventory.SelectWeapon(value);
            SelectWeapon(tempWeapon);
        }

        private void SelectWeapon(WeaponBehaviour weapon)
        {
            Services.Instance.WeaponService.Off();
            if (weapon != null)
            {
                Services.Instance.WeaponService.On(weapon);
            }
        }

        private void SwitchFlashLight()
        {
            var tempFlashLight = _characterData.CharacterBehaviour.Inventory.FlashLight;
            if(tempFlashLight != null)
            {
                Services.Instance.FlashLightService.Switch(tempFlashLight);
            }
        }

        private void SelectGranade()
        {
            Services.Instance.GranadeService.Off();
            var tempGranade = _characterData.CharacterBehaviour.Inventory.SelectGranade();
            if (tempGranade != null)
            {
                Services.Instance.GranadeService.On(tempGranade);
            }
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
