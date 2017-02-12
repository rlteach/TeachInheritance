using UnityEngine;
using System.Collections;


//abstract classes must be inherited, they cannot live on their own
abstract	public class Jewel : MonoBehaviour {

	static	Sprite[]	sSpriteSheet;		    //Used to load sprites just once when first used

    private static  Sprite mBlankTile;

    readonly static string  sJewelSpriteFile ="Jewels";
    readonly static string  sBlankSpriteFile = "EmptyTile";
    readonly static string  sTilePrefabName = "EmptyJewel";
	public	static	readonly	string	JewelTag = "Jewel";

    static bool sStaticsLoaded = false;

	SpriteRenderer	mSR;    //Cache sprite renderer    

    static GameObject mTilePrefab;      //Blank Jewel time prefab

    public  static  int JewelTypeCount {
        get {
            if(sSpriteSheet!=null) {
                return sSpriteSheet.Length;
            }
            return 0;
        }
    }

    public  static Sprite BlankTile {    //Used to get a blank tile sprite
           get {
            return mBlankTile;
        }
    }

    void Update () {        //This method won't be exposed in higher layers
		UpdateJewel (GameManager.OffScreen(transform.position));			//Get Item to Update and flag if offscreen
	}

	protected	virtual	void UpdateJewel(bool vOffscreen) {		//Called to Update Object, with Offscreen flag, replaces Update() method
        if(vOffscreen) {
            GameManager.RemoveJewel(this);      //Default action is to remove jewel off screen
        }
	}
		
	public	static	 Sprite	GetSpriteAtIndex(uint vIndex) {		//Allow inherted classes to get access to sprite sheet
		if (sSpriteSheet != null) {
			if (vIndex < sSpriteSheet.Length) {
				return sSpriteSheet [vIndex];		//Get Sprite tile
			}
		}
		Debug.Log ("Invalid Index:" + vIndex);		//Invalid sprite index asked for
		return	null;
	}

    //Called to set up static assets, code makes sure it only happens once
    static  void    SetupPrefabs() {
        if (!sStaticsLoaded) {
            if (sSpriteSheet == null) {
                sSpriteSheet = Resources.LoadAll<Sprite>(GameManager.SpriteFolder + sJewelSpriteFile);
            }
            if (mBlankTile == null) {
                mBlankTile = Resources.Load<Sprite>(GameManager.SpriteFolder + sBlankSpriteFile);
            }
            if (mTilePrefab == null) {
                mTilePrefab = Resources.Load<GameObject>(GameManager.PrefabFolder + sTilePrefabName);
            }
            sStaticsLoaded = (sSpriteSheet != null && mBlankTile != null && mTilePrefab != null);
            if(!sStaticsLoaded) {
                GameManager.DebugMsg("Load Error");
            }
        }
    }

    //Make a static creation method, which allows for any Object dervied from Jewel to be created
    public	static	T Create<T>(Vector2 tPosition) where T:Jewel {
        SetupPrefabs();     //Make sure we have prefabs before we start, only actually loads them once
        GameManager.DebugMsg("Create:" + typeof(T).Name);
        GameObject tGO =Instantiate<GameObject>(mTilePrefab);		//Construct a new game object
		T tJewel=tGO.AddComponent<T> ();		//Add Jewel script of correct derived type to this object
        tJewel.transform.position = tPosition;
		tJewel.mSR = tGO.GetComponent<SpriteRenderer>();		//get sprite renderer
		tJewel.mSR.sprite=GetSpriteAtIndex(tJewel.SpriteIndex);		//Will call correct inherited version of the code to get its sprite index
        tJewel.name = tJewel.Name + "-Cloned";
		return	tJewel;
	}


	public	abstract	uint	SpriteIndex {	//Base clase does not implement these, but inherited classes must
		get;
	}

	public	abstract	string Name {		//Needs to be defined in inheritted class
		get;
	}

    void OnTriggerEnter2D(Collider2D vOther) {        //Default handler, this add the item to the players inventory
		Jewel tJewel = GetComponent<Jewel>();		//Get Jewel component
		if (vOther.tag == Player.PlayerTag) {
			Player tPlayer = vOther.GetComponent<Player> ();		//Get player we collided with
			CollidedWithPlayer (tPlayer);
		} else if (vOther.tag == Jewel.JewelTag) {
			Jewel tOtherJewel = vOther.GetComponent<Jewel> ();		//Get Jewel we collided with
			CollidedWithJewel(tOtherJewel);
		}
    }

	protected	virtual void	CollidedWithPlayer(Player vPlayer) {		//Default action
		vPlayer.PlayerHitJewel (this);		//Notify player
		GameManager.RemoveJewel(this);		//Tell Game manager to remove Jewel
	}

	protected	virtual void	CollidedWithJewel(Jewel vOtherJewel) {
        if (vOtherJewel.AllowJewelToDie) {
            GameManager.RemoveJewel(vOtherJewel);       //Tell Game manager to remove other Jewel
        }
	}

    protected   virtual bool    AllowJewelToDie {       //Default is yes
        get {
            return true;
        }
    }

	public virtual void Removed() {		//Called when GameManager has removed Jewel
		Destroy(gameObject);		//Default action is to destroy it
	}


	void OnDestroy() {			//Print Debug Message
		GameManager.DebugMsg (GetType ().Name + " Destroyed");
	}
}
