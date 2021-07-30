using System.Linq;

namespace ExampleTemplate
{
	public class EnemyController : IExecute, IInitialization, IListenerScreen
	{
		#region Fields

		private bool _isActive;
		private EnemiesData _enemiesData;

		#endregion


		#region IInitialization

		public void Initialization()
		{
			ScreenInterface.GetInstance().AddObserver(ScreenType.GameMenu, this);
			_enemiesData = Data.Instance.EnemiesData;
		}

		#endregion


		#region IExecute

		public void Execute()
		{
			if (!_isActive) return;
			for (var i = 0; i < _enemiesData.GetAiList.Count; i++)
			{
				var bot = _enemiesData.GetAiList.ElementAt(i);
				bot.Tick();
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