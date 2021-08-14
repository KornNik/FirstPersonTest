using System.Collections;
using UnityEngine;

namespace ExampleTemplate
{
    public class CameraShakeRotation : MonoBehaviour
    {
        public void ShakeRotateCamera(float duration, float angleDeg, Vector2 direction)
        {
            //Запускаем корутину вращения камеры
            StartCoroutine(ShakeRotateCor(duration, angleDeg, direction));
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
            direction = direction.normalized;
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
                Vector3 resDirection = ((Vector3)currentDirection + Vector3.forward).normalized;
                //С помощью Quaternion.FromToRotation получаем новое вращение
                //Изменяем локальное вращение, дабы во время вращения, если игрок будет управлять камерой
                //Все работало корректно
                transform.localRotation = Quaternion.FromToRotation(Vector3.forward, resDirection);

                elapsed += Time.deltaTime;
                yield return null;
            }
            //Восстанавливаем вращение
            transform.localRotation = startRotation;
        }
    }
}