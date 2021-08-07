using UnityEngine;

namespace ExampleTemplate
{
    public class GranadeController : IExecute,IListenerScreen
    {
        #region Fields

        private bool _isActive;

        #endregion


        #region ClassLyfeCycle

        public GranadeController()
        {
            ScreenInterface.GetInstance().AddObserver(ScreenType.GameMenu, this);
        }

        #endregion


        #region IExecute

        public void Execute() 
        {
            if (!_isActive) return;
            if (!Services.Instance.GranadeService.IsGranade) return;

            var tempGranade = Services.Instance.GranadeService.Granade;

            if (Input.GetKeyUp(KeyManager.THROW_GRANADE))
            {
                tempGranade.Throw();
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
