using UnityEngine;


namespace ExampleTemplate
{
    public sealed class CameraServices : Service
    {

        #region Properties

        public Camera CameraMain { get; private set; }

        #endregion


        #region ClassLifeCycles

        public CameraServices()
        {
            SetCamera(Camera.main);
        }

        #endregion
        
        
        #region Methods

        public void SetCamera(Camera camera)
        {
            CameraMain = camera;
        }

        #endregion
    }
}
