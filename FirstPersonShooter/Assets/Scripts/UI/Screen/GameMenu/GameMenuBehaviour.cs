using UnityEngine;
using UnityEngine.UI;


namespace ExampleTemplate
{
    public sealed class GameMenuBehaviour : BaseUi
    {
        #region Fields

        [SerializeField] private Text _clipCount;
        [SerializeField] private Text _ammunitionCount;
        [SerializeField] private Image _enemyHealth;
        
        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            EnemyBehaviour.OnHealthChanged += EnemyHealth;
            WeaponService.OnAmmunitionChange += ShowInfo;
        }

        private void OnDisable()
        {
            EnemyBehaviour.OnHealthChanged -= EnemyHealth;
            WeaponService.OnAmmunitionChange -= ShowInfo;
        }

        #endregion
        

        #region Methods

        public override void Show()
        {
            gameObject.SetActive(true);
            ShowUI.Invoke();
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            HideUI.Invoke();
        }
        private void ShowInfo(int clip,int ammunition)
        {
            _clipCount.text = clip.ToString();
            _ammunitionCount.text = ammunition.ToString();
        }

        private void EnemyHealth(float health)
        {
            _enemyHealth.fillAmount = health/Data.Instance.EnemiesData.GetHealth() ;
        }

        private void Call()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.MainMenu);
        }

        #endregion
    }
}
