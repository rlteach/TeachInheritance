using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PurpleStone : Jewel {

    int mHitCount = 3;

    readonly static string sTextPrefabName = "TextOverlay";

    static GameObject sTextOverlayPrefab;

    Text mItemText;

    //These methods override the ones in the base clase
    public override string Name {
        get {
            return "Purple Stone";
        }
    }

    public override uint SpriteIndex {       //The object knows which sprite index to use for rendering
        get {
            return 3;
        }
    }

    void Start() {
        if (sTextOverlayPrefab == null) {       //Only load it once
            sTextOverlayPrefab = Resources.Load<GameObject>(GameManager.PrefabFolder + sTextPrefabName);
        }
        GameObject tTextGO = Instantiate(sTextOverlayPrefab);       //Make new text Prefab
        tTextGO.transform.SetParent(transform);     //Parent it to us
        tTextGO.transform.localPosition = Vector3.zero;     //Move back to local orgin
        mItemText = tTextGO.GetComponentInChildren<Text>();     //Give us a text link to write to
        mItemText.text = mHitCount.ToString();
        Invoke("TimeOut", 10f);
    }

    void    TimeOut() {     //Stone destroys itself after timeout
        GameManager.RemoveJewel(this);        //Tell Game manager to remove Jewel
    }

    protected override void CollidedWithPlayer(Player vPlayer) {       //Purple stones warp 3 times until destroyed
        if (mHitCount>0) {
            mItemText.text = mHitCount.ToString();
            transform.position = GameManager.OnScreenRandomPosition;        //Warp it
            mHitCount--;
        } else {
            vPlayer.PlayerHitJewel(this);     //Notify player
            GameManager.RemoveJewel(this);        //Tell Game manager to remove Jewel
        }
    }

    protected override void CollidedWithJewel(Jewel vOtherJewel) {
        GameManager.RemoveJewel(vOtherJewel);       //Tell Game manager to remove other Jewel
    }
}
