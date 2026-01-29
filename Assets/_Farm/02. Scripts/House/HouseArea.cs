using UnityEngine;

namespace Farm
{
    public class HouseArea : MonoBehaviour, ITriggerEvent
    {
        [SerializeField] private GameObject roof;
        
        [SerializeField] private GameObject door;
        [SerializeField] private Animator anim;

        public void InteractionEnter()
        {
            CameraManager.OnChangedCamera("Player", "House");
            roof.SetActive(false);
            anim.SetTrigger("DoorOpen");
        }

        public void InteractionExit()
        {
            CameraManager.OnChangedCamera("House", "Player");
            roof.SetActive(true);
            anim.SetTrigger("DoorClose");
        }
    }
}
