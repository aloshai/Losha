using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Transform> Items;

    public Transform GetItem(string id)
    {
        return Items.Find(e => e.GetComponent<Item>().Id == id);
    }
}
