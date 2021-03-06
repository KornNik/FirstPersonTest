using UnityEngine;
using UnityEngine.UI;


namespace ExampleTemplate
{
    public sealed class GameMenuBehaviour : BaseUi
    {
        #region Fields

        [SerializeField] private Text _clipCount;
        [SerializeField] private Text _ammunitionCount;
        [SerializeField] private Text _flashLightChargeNum;

        [SerializeField] private Image _enemyHealth;
        [SerializeField] private Image _playerHealth;
        [SerializeField] private Image _flashLightChargeFill;
        
        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            EnemyBehaviour.EnemyHealthChanged += OnEnemyHealthChanged;
            WeaponService.AmmunitionChanged += OnAmmunitionChange;
            FlashLightBehaviour.ChargeChange += OnChargeChanged;
            CharacterBehaviour.CharacterHealthChanged += OnPlayerHealthChanged;
        }

        private void OnDisable()
        {
            EnemyBehaviour.EnemyHealthChanged -= OnEnemyHealthChanged;
            WeaponService.AmmunitionChanged -= OnAmmunitionChange;
            FlashLightBehaviour.ChargeChange -= OnChargeChanged;
            CharacterBehaviour.CharacterHealthChanged -= OnPlayerHealthChanged;
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
        private void OnAmmunitionChange(int clip,int ammunition)
        {
            _clipCount.text = clip.ToString();
            _ammunitionCount.text = ammunition.ToString();
        }

        private void OnEnemyHealthChanged(float health)
        {
            _enemyHealth.fillAmount = health;
        }
        private void OnChargeChanged(float charge)
        {
            _flashLightChargeFill.fillAmount = charge;
            _flashLightChargeNum.text = charge.ToString();
        }
        private void OnPlayerHealthChanged(float health)
        {
            _playerHealth.fillAmount = health;
        }

        private void Call()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.MainMenu);
        }

        #endregion
    }
}
