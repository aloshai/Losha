using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public PlayerInventory PlayerInventory;

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
            PlayerInventory.DropItem(0);
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
