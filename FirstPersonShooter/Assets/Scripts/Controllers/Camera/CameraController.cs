using UnityEngine;

namespace ExampleTemplate
{
    class CameraController : IExecute, IListenerScreen
    {
        #region Fields

        private CharacterData _characterData;
        private CameraData _cameraData;
        private CameraBehaviuor _cameraBehaviuor;

        private bool _isActive;

        #endregion


        #region ClassLifeCycles

        public CameraController ()
        {
            ScreenInterface.GetInstance().AddObserver(ScreenType.GameMenu, this);
            _characterData = Data.Instance.Character;
            _cameraData = Data.Instance.Camera;
            _cameraBehaviuor = Services.Instance.CameraServices.CameraMain.GetComponent<CameraBehaviuor>();
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!_isActive) return;
            Vector2 mouseAxis;
            mouseAxis.x = Input.GetAxis("Mouse X") * _cameraData.GetXSensitivity();
            mouseAxis.y = Input.GetAxis("Mouse Y") * _cameraData.GetYSensitivity();
            if (mouseAxis.x != 0 || mouseAxis.y != 0) 
            {
                _cameraBehaviuor.LookRotation(mouseAxis, _characterData.CharacterBehaviour.transform);
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
