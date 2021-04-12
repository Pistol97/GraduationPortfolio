﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    //필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;

    [SerializeField]
    private GameObject go_SlotParent;

    //슬롯들
    private Slot[] slots;

    private void Start()
    {
        slots = go_SlotParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if(inventoryActivated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item item, int count = 1)
    {
        //이미 존재하는 아이템일 경우
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i]._item != null)
            {
                  if(slots[i]._item.itemName == item.itemName)
                  {
                        slots[i].SetSlotCount(count);
                        return;
                  }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i]._item == null)
            {
                slots[i].AddItem(item,count);
                return;
            }
        }
    }
}