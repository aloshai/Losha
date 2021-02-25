using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> Items;
    public ItemManager itemManager;
    public float ItemDropDistance = 1.2f;
    private Player player;

    private GameObject primary = null;

    void Start()
    {
        Items = new List<string>();
        this.player = GetComponent<Player>();
    }

    public GameObject GetWeapon() => primary;

    public GameObject DropItem(int index)
    {
        var item = Items[index];
        Items.RemoveAt(index);
        // TODO: Eşyanın bırakılacağı mesafe Unity'den ayarlanabilecek. Yakın zamanda bu ekleme yapılmalı. Eşyayı bırakırken ne kadar uzağımıza bırakacağımızı belirteceğiz.
        return Instantiate(itemManager.GetItem(item).gameObject,
            (this.player.transform.position + (this.player.transform.forward * ItemDropDistance)), Quaternion.LookRotation(this.player.transform.forward));
    }

    public void UseItem(int index)
    {
        var item = Items[index];
        primary = Instantiate(itemManager.GetItem(item).gameObject, player.hand.position, Quaternion.LookRotation(this.player.transform.forward), player.hand);
        Destroy(primary.GetComponent<Rigidbody>());
    }

    public void DeuseItem()
    {
        Destroy(primary);
        primary = null;
    }

    public void TakeItem(Transform transform)
    {
        var item = transform.GetComponent<Item>();
        Items.Add(item.Id);
        Destroy(transform.gameObject);
    }
}
