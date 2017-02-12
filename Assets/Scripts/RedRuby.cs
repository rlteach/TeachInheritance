using UnityEngine;
using System.Collections;

public class RedRuby : Jewel {


    //These methods override the ones in the base class to give a name
	public	override	string	Name {
		get	{
			return	"Red Ruby";
		}
	}

	public override uint SpriteIndex {       //The object knows which sprite index to use for rendering
		get {
			return 4;
		}
	}

    Vector3 mVelocity = Vector3.up;

    //Best to not override Awake() as Create<T> sets up a lot of stuff which wont be in place until Start()

    void Start() {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
    }


    protected override bool AllowJewelToDie {       //Red Jeweles cannot be killed by other Jewels
        get {
            return false;
        }
    }

    //Red Jewels move
    protected override void    UpdateJewel(bool vOffscreen) {
        transform.position += transform.rotation*mVelocity * Time.deltaTime;
        if(vOffscreen) {
            transform.position = GameManager.ClampOnScreen(transform.position);     //Like the wrap script
        }
    }
}
