using UnityEngine;
using System.Collections;

public class GreenEmerald : Jewel {


    //These methods override the ones in the base clase
	public	override	string	Name {
		get	{
			return	"Green Emerald";
		}
	}

	public override uint SpriteIndex {       //The object knows which sprite index to use for rendering
		get {
			return 1;
		}
	}

    protected override void OnTriggerEnter2D(Collider2D vOther) {
        base.OnTriggerEnter2D(vOther);
    }
}
