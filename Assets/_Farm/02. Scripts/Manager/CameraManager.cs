using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Farm
{   
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform clearShot;

        private static event Action<string, string> onChangeCamera;

        private Dictionary<string, CinemachineCamera> cameraDics = new Dictionary<string, CinemachineCamera>();
        
        private void Awake()
        {
            if (clearShot == null)
                return;

            for (int i = 0; i < clearShot.childCount; i++)
            {
                Transform child = clearShot.GetChild(i);
                CinemachineCamera cam = child.GetComponent<CinemachineCamera>();

                if (!cameraDics.ContainsKey(child.name)) // 이미 저장된 키값이 있는지 확인
                {
                    cameraDics.Add(child.name, cam);
                    //Debug.Log($"{child.name} 카메라 등록");
                }
            }
        }

        private void OnEnable()
        {
            onChangeCamera += SetCamera;
        }

        private void OnDisable()
        {
            onChangeCamera -= SetCamera;
        }

        private void SetCamera(string from, string to)
        {
            cameraDics[from].Priority = 0;
            cameraDics[to].Priority = 10;
        }

        public static void OnChangedCamera(string from, string to)
        {
            onChangeCamera?.Invoke(from, to);
        }
    }
}