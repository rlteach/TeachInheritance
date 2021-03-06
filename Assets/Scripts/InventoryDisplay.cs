﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class InventoryDisplay : MonoBehaviour {

	public	static  readonly string InventoryTag = "Inventory";

    static  readonly string sPrefabName = "InventoryTile";

    List<GameObject> mItems;        //List of items to display in Inventory

	Player	mPlayer;

    void    Start() {
        mItems = new List<GameObject>();
        GameObject tPrefab = Resources.Load<GameObject>(GameManager.PrefabFolder + sPrefabName);
        for (int tI = 0; tI < 32; tI++) {       //16 inventory items to start with
            GameObject tGO = Instantiate(tPrefab, transform) as GameObject;
            tGO.name = "InventoryTile:" + (tI + 1).ToString();
            mItems.Add(tGO);    //Add Sprite renderer to parent panel
        }
    }

	public	Player	SetPlayer {
		set {
			mPlayer = value;
		}
	}

    void    Update() {
        if(mPlayer!=null && mPlayer.Inventory.NeedsDisplay) {
            UpdateInventory(mPlayer);
        }
    }

    public  void    UpdateInventory (Player vPlayer) {
        int tIndex=0;
        foreach(GameObject tGO in mItems) {
            Image tSR = tGO.GetComponent<Image>();
            if (tIndex < vPlayer.Inventory.Items.Count) {    //Does player have item for this slot
                uint tSpriteIndex = vPlayer.Inventory.Items[tIndex].SpriteIndex;
                tSR.sprite = Jewel.GetSpriteAtIndex(tSpriteIndex);
                tIndex++;
            } else {
                tSR.sprite = Jewel.BlankTile;
            }
        }
        vPlayer.Inventory.DidDisplay();     //Flag as displayed
    }
}
