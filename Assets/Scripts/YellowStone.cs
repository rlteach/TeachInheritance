using UnityEngine;
using System.Collections;

public class YellowStone : Jewel {

    float mSize = 0f;


    //These methods override the ones in the base clase
	public	override	string	Name {
		get	{
			return	"Yellow Stone";
		}
	}


	public override uint SpriteIndex {       //The object knows which sprite index to use for rendering
		get {
			return 6;
		}
	}

    //Yellow Jewels Scale
    protected override void UpdateJewel(bool vOffscreen) {
        transform.localScale=Vector3.one*Mathf.Sin(mSize*Mathf.Deg2Rad);
        mSize = GameManager.fmod(mSize + Time.deltaTime * 100f, 360f);
        if (vOffscreen) {
            base.UpdateJewel(vOffscreen);       //Let Base code deal with offscreen
        }
    }
}
