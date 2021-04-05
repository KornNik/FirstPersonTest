using UnityEngine;


namespace ExampleTemplate
{
    public sealed class InputController : IExecute, IListenerScreen
    {
        #region Fields

        private readonly CharacterData _characterData;

        private KeyCode _activeMainMenu = KeyCode.Escape;
        private KeyCode _deactivateWeapon = KeyCode.Alpha0;
        private KeyCode _firstWeapon = KeyCode.Alpha1;
        private KeyCode _secondWeapon = KeyCode.Alpha2;
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

            if (Input.GetKey(_activeMainMenu))
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

            _characterData.CharacterBehaviour.CharacterMove(inputAxis);
            _characterData.CharacterBehaviour.GamingGravity();

            if (Input.GetAxis(AxisManager.MousScrollWheel) > 0)
            {
                MouseScroll(MouseScrollWheel.Up);
            }
            if (Input.GetAxis(AxisManager.MousScrollWheel) < 0)
            {
                MouseScroll(MouseScrollWheel.Down);
            }

            if (Input.GetKeyDown(_firstWeapon))
            {
                SelectWeapon(0);
            }
            if (Input.GetKeyDown(_secondWeapon))
            {
                SelectWeapon(1);
            }
            if (Input.GetKeyDown(_deactivateWeapon))
            {
                Services.Instance.WeaponService.Off();
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

            var tempWeapon = _characterData.InventoryBehaviour.SelectWeapon(value);
            if (tempWeapon is WeaponBehaviour)
            {
                SelectWeapon(tempWeapon);
            }
        }

        private void MouseScroll(MouseScrollWheel value)
        {
            var tempWeapon = _characterData.InventoryBehaviour.SelectWeapon(value);
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
