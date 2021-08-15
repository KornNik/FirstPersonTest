using System.Collections;
using UnityEngine;

namespace ExampleTemplate
{
    public sealed class CameraBehaviuor : MonoBehaviour
    {
        #region Fields

        private CameraData _cameraData;
        private Quaternion _cameraTargetRot;
        private Coroutine _shakeCoroutine;

        #endregion


        #region UnityMethods

        private void Awake()
		{
			_cameraData = Data.Instance.Camera;
            _cameraTargetRot = gameObject.transform.localRotation;
        }

        #endregion


        #region Methods

        public void LookRotation(Vector2 mouseAxis, Transform character)
        {
            character.localRotation *= Quaternion.Euler(0f, mouseAxis.x, 0f);
            _cameraTargetRot *= Quaternion.Euler(-mouseAxis.y, 0f, 0f);

            if (_cameraData.GetIsClampRotation())
            { _cameraTargetRot = ClampRotationAroundXAxis(_cameraTargetRot); }

            gameObject.transform.localRotation = _cameraTargetRot;
        }

        private Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, _cameraData.GetMimimumX(), _cameraData.GetMaximumX());

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

        public void ShakeRotateCamera(float duration, float angleDeg, Vector2 direction)
        {
            if (_shakeCoroutine == null)
            {
                _shakeCoroutine = StartCoroutine(ShakeRotateCor(duration, angleDeg, direction));
            }
        }


        private IEnumerator ShakeRotateCor(float duration, float angleDeg, Vector2 direction)
        {
            //Счетчик прошедшего времени
            float elapsed = 0f;
            //Запоминаем начальное вращение камеры по аналогии с вибрацией камеры
            Quaternion startRotation = transform.localRotation;
            //Для удобства добавляем переменную середину нашего таймера
            //Ибо сначала отклонение будет идти на увеличение, а затем на уменьшение
            float halfDuration = duration / 2;
            //Приводим направляющий вектор к единичному вектору, дабы не портить вычисления
            direction = (transform.forward+(Vector3)direction).normalized;

            while (elapsed < duration)
            {
                //Сохраняем текущее направление ибо мы будем менять данный вектор
                Vector2 currentDirection = direction;
                //Подсчёт процентного коэффициента для функции Lerp[0..1]
                //До середины таймера процент увеличивается, затем уменьшается
                float t = elapsed < halfDuration ? elapsed / halfDuration : (duration - elapsed) / halfDuration;
                //Текущий угол отклонения
                float currentAngle = Mathf.Lerp(0f, angleDeg, t);
                //Вычисляем длину направляющего вектора из тангенса угла.
                //Недостатком данного решения будет являться то
                //Что угол отклонения должен находится в следующем диапазоне (0..90)
                currentDirection *= Mathf.Tan(currentAngle * Mathf.Deg2Rad);
                //Сумма векторов - получаем направление взгляда на текущей итерации
                Vector3 resDirection = ((Vector3)currentDirection + transform.forward).normalized;
                //С помощью Quaternion.FromToRotation получаем новое вращение
                //Изменяем локальное вращение, дабы во время вращения, если игрок будет управлять камерой
                //Все работало корректно
                transform.localRotation = Quaternion.FromToRotation(transform.forward, resDirection);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localRotation = startRotation;
            _shakeCoroutine = null;
        }

        #endregion
    }
}
