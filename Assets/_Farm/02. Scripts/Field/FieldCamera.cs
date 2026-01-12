using UnityEngine;

namespace Farm
{
    public class FieldCamera : MonoBehaviour
    {
        private Transform target;

        [SerializeField] private Vector3 offset, minBounds, maxBounds;
        [SerializeField] private float smoothSpeed = 5f;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            if (target == null)
                return;

            Vector3 destination = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, destination, smoothSpeed * Time.deltaTime);

            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
            smoothedPosition.z = Mathf.Clamp(smoothedPosition.z, minBounds.z, maxBounds.z);
            
            transform.position = smoothedPosition;
        }
    }
}