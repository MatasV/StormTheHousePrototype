using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquippedGunDisplay : MonoBehaviour, IDropHandler
{
    [FormerlySerializedAs("firstGun")] [SerializeField] private SharedGun gun;
    [SerializeField] private Image gunDisplay;
    private void Awake()
    {
        gun.valueChangeEvent.AddListener(DisplayGun);
    }

    private void DisplayGun()
    {
        if(gun.Value != null)
            gunDisplay.sprite = gun.Value.gunData.sprite;
    }

    private void OnDestroy()
    {
        gun.valueChangeEvent.RemoveListener(DisplayGun);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Gun draggedGun = eventData.pointerDrag.GetComponent<GunPurchase>().GetGun();

        if (eventData.pointerDrag != null && draggedGun.gunData.purchased == true)
        {
            gun.Value = draggedGun;

            Sprite gunSprite = eventData.pointerDrag.transform.Find("Image").GetComponent<Image>().sprite;
            gunDisplay.sprite = gunSprite;
        }
    }
}
