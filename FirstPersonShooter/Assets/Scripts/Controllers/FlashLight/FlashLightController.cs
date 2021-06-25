using UnityEngine;

namespace ExampleTemplate
{
    public class FlashLightController : IExecute, IInitialization, IListenerScreen
    {
		#region Fields

		private bool _isActive;

		#endregion


		#region IInitialization

		public void Initialization()
		{
			ScreenInterface.GetInstance().AddObserver(ScreenType.GameMenu, this);
		}

		#endregion


		#region IExecute

		public void Execute()
		{

			if (!_isActive) return;

			var tempFlashLight = Services.Instance.FlashLightService.FlashLight;

            if (!Services.Instance.FlashLightService.IsFlashLight)
            {
				if(tempFlashLight is FlashLightBehaviour)
                {
					tempFlashLight.BatteryRecharge();
				}
                return;
            }

			if (tempFlashLight.EditBatteryCharge())
			{
				tempFlashLight.Rotation();
			}
			else
			{
				Services.Instance.FlashLightService.Off();
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