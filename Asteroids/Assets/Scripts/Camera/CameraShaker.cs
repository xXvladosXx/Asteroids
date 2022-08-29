using System.Collections;
using Data.Camera;
using UnityEngine;

namespace Camera
{
    public class CameraShaker : MonoBehaviour
    {
        [field: SerializeField] public CameraShakerData CameraShakerData { get; private set; }

        private UnityEngine.Camera _camera;
        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        public void StartShaking(float duration, float magnitude)
        {
            StartCoroutine(Shake(duration, magnitude));
        }

        private IEnumerator Shake(float duration, float magnitude)
        {
            Vector3 startPos = _camera.transform.localPosition;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                _camera.transform.localPosition = new Vector3(x, y, startPos.z);
                elapsed += Time.deltaTime;

                yield return null;
            }

            _camera.transform.localPosition = startPos;
        }
    }
}