using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemGrid : MonoBehaviour {

    public InventoryItem ivenItem;

    void Start()
    {
        ivenItem = transform.FindChild("InventoryItem").GetComponent<InventoryItem>();
    }
}
