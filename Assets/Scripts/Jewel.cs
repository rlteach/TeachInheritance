using UnityEngine;
using System.Collections;


//abstract classes must be inherited, they cannot live on their own
abstract	public class Jewel : MonoBehaviour {


	static	Sprite[]	sSpriteSheet;		//Used to load sprites just once when first used

	readonly	string	SpriteFolder="Sprites/";

	readonly	string	sJewelSpriteFile="Jewels";

	readonly	string[]	sSpriteNames = {"Red Ruby"
											, "Orange Tigers Eye"
											, "Yellow Citrine"
											, "Green Emerald"
	};

	SpriteRenderer	mSR;
					

	protected	virtual	void	Awake() {
		if (sSpriteSheet == null) {
			sSpriteSheet = Resources.LoadAll<Sprite> (SpriteFolder+sJewelSpriteFile);
			Debug.Log(string.Format("{0} sprites loaded",sSpriteSheet.Length));
		}
	}

	//procected means only this & derived classess can access this member, virtual means this can be overidded in derived classes
	protected	virtual	void Start () {
	
	}
	
	protected	virtual	void Update () {
	
	}

	protected	static	 Sprite	GetSpriteAtIndex(uint vIndex) {		//Allow inherted classes to get access to sprite sheet
		if (sSpriteSheet != null) {
			if (vIndex < sSpriteSheet.Length) {
				return sSpriteSheet [vIndex];		//Get Sprite tile
			}
		}
		Debug.Log ("Invalid Index:" + vIndex);		//Invalid sprite index asked for
		return	null;
	}


	//Make a static creation method, which allows for any Object dervied from Jewel to be created
	public	static	T Create<T>(Vector2 tPosition) where T:Jewel {
		GameObject	tGO=new GameObject();		//Construct a new game object
		T tJewel=tGO.AddComponent<T> ();		//Add Jewel script of correct derived type to this object
		tJewel.mSR = tJewel.gameObject.AddComponent<SpriteRenderer> ();		//Attach sprite renderer
		tJewel.mSR.sprite=GetSpriteAtIndex(tJewel.SpriteIndex);		//Will call correct inherited version of the code to get its sprite index
		return	tJewel;
	}


	protected	abstract	uint	SpriteIndex {	//Base clase does not implement these, but inherited classes must
		get;
	}

	public	abstract	string Name {		//Needs to be defined in inheritted class
		get;
	}
}
