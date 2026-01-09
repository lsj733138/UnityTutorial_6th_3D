using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Farm
{
    public class CameraManager : MonoBehaviour
    {
        public static Action<int, int> cameraAction;
        
        [SerializeField] private CinemachineClearShot clearShot;
        [SerializeField] private CinemachineCamera[] cameras;

        private void OnEnable()
        {
            cameraAction += SetCamera;
        }

        private void OnDisable()
        {
            cameraAction -= SetCamera;
        }

        private void Start()
        {
            cameras = clearShot.GetComponentsInChildren<CinemachineCamera>();
        }

        private void SetCamera(int index, int priority)
        {
            cameras[index].Priority = priority;
        }
    }
}