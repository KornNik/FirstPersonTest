using UnityEngine;

namespace ExampleTemplate {
    public class SnowBrush : MonoBehaviour
    {
        public CustomRenderTexture SnowHeightMap;
        public Material HeightMapUpdate;

        public float SecondsToRestore = 100;

        private Camera mainCamera;
        private int tireIndex;

        private float timeToRestoreOneTick;

        private static readonly int DrawPosition = Shader.PropertyToID("_DrawPosition");
        private static readonly int DrawAngle = Shader.PropertyToID("_DrawAngle");
        private static readonly int RestoreAmount = Shader.PropertyToID("_RestoreAmount");

        private void Start()
        {
            SnowHeightMap.Initialize();
            mainCamera = Camera.main;
        }

        private void Update()
        {
            // Считаем таймер до восстановления каждого пикселя текстуры на единичку 
            timeToRestoreOneTick -= Time.deltaTime;
            if (timeToRestoreOneTick < 0)
            {
                // Если в этот update мы хотим увеличить цвет всех пикселей карты высот на 1
                HeightMapUpdate.SetFloat(RestoreAmount, 1 / 250f);
                timeToRestoreOneTick = SecondsToRestore / 250f;
            }
            else
            {
                // Если не хотим
                HeightMapUpdate.SetFloat(RestoreAmount, 0);
            }

            // Обновляем текстуру вручную, можно это убрать и поставить Update Mode: Realtime
            SnowHeightMap.Update();
        }
        private void OnCollisionStay(Collision collision)
        {
            DrawWithPaws(collision.transform);
            Debug.Log(collision);
        }

        private void DrawWithPaws(Transform touchTransform)
        {
            Ray ray = new Ray(touchTransform.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 1f))
            {
                Vector2 hitTextureCoord = hit.textureCoord;
                float angle = 180 + touchTransform.rotation.eulerAngles.y;

                HeightMapUpdate.SetVector(DrawPosition, hitTextureCoord);
                HeightMapUpdate.SetFloat(DrawAngle, angle * Mathf.Deg2Rad);
            }
        }
    }
}
