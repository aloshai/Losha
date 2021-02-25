using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> Items;
    public ItemManager itemManager;
    private Player player;

    void Start()
    {
        Items = new List<string>();
        this.player = GetComponent<Player>();
    }

    public GameObject DropItem(int index)
    {
        var item = Items[index];
        Items.RemoveAt(index);

        return Instantiate(itemManager.GetItem(item).gameObject,
            (this.player.transform.position + this.player.transform.forward), Quaternion.identity);
    }

    public void TakeItem(Transform transform)
    {
        var item = transform.GetComponent<Item>();
        Items.Add(item.Id);
        Destroy(transform.gameObject);
    }
}
