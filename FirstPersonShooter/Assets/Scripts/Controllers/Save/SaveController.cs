using UnityEngine;

namespace ExampleTemplate
{
    public class SaveController: IExecute
    {
        #region Fields

        private DeserializationSavingData _savingData;

        #endregion


        #region ClassLifeCycle

        public SaveController()
        {
            _savingData = new DeserializationSavingData();
        }

        #endregion

        #region IExecute

        public void Execute()
        {
            if (Input.GetKeyDown(KeyManager.SAVE))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyManager.LOAD))
            {
                Load();
            }
        }

        #endregion


        #region Methods

        private void Save()
        {
            _savingData.Save();
        }

        private void Load()
        {
            _savingData.Load();
        }

        #endregion
    }
}
