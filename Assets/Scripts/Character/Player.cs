using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public PlayerInventory PlayerInventory;

    public Transform hand;

    public Transform Bullet;

    [Header("Item Viewer")]
    public Camera ItemViewerCamera;
    public float ItemViewDistance = 2f;
    public LayerMask ItemLayerMask;

    public Transform ItemPickView;

    private bool itemLastActive;

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.C))
        {
            // TODO: index parametresi düzeltilecek.
            PlayerInventory.DropItem(0);
            if(PlayerInventory.Items.Count <= 0) PlayerInventory.DeuseItem();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && PlayerInventory.GetWeapon() == null)
        {
            // TODO: index parametresi düzeltilecek.
            PlayerInventory.UseItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && PlayerInventory.GetWeapon() != null)
        {
            // TODO: index parametresi düzeltilecek.
            PlayerInventory.DeuseItem();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var barrel = PlayerInventory.GetWeapon();
            if (barrel == null) return;
            var barrelTransform = barrel.transform.Find("Barrel");
            if (Physics.Raycast(new Ray(barrelTransform.position, barrelTransform.forward), out var hitInfo2, 50f))
            {
                Instantiate(Bullet, hitInfo2.point, Quaternion.identity);
            }
        }
        Vector3 lookAtPosition = ItemViewerCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, ItemViewerCamera.nearClipPlane));

        var flag = Physics.Raycast(new Ray(lookAtPosition, ItemViewerCamera.transform.forward), out var hitInfo,
            ItemViewDistance, ItemLayerMask);

        if (flag)
        {
            var item = hitInfo.transform.GetComponent<Item>();
            if (item != null)
            {
                if(Input.GetKeyDown(KeyCode.F)) PlayerInventory.TakeItem(hitInfo.transform);
            }

            ItemPickView.gameObject.SetActive(true);
            itemLastActive = true;
        }
        else if (itemLastActive)
        {
            itemLastActive = false;
            ItemPickView.gameObject.SetActive(false);
        }
    }
}
