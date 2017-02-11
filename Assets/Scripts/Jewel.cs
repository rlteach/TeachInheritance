using UnityEngine;
using System.Collections;


//abstract classes must be inherited, they cannot live on their own
abstract	public class Jewel : MonoBehaviour {


	static	Sprite[]	sSpriteSheet;		    //Used to load sprites just once when first used

    private static  Sprite mBlankTile;

    readonly static string  sJewelSpriteFile ="Jewels";
    readonly static string  sBlankSpriteFile = "EmptyTile";
    readonly static string  sTilePrefabName = "EmptyJewel";

    static bool StaticsLoaded = false;

    readonly string[]	sSpriteNames = {"Red Ruby"
											, "Orange Tigers Eye"
											, "Yellow Citrine"
											, "Green Emerald"
	};

	SpriteRenderer	mSR;    //Cache sprite renderer    

    static GameObject mTilePrefab;

    public  static Sprite BlankTile {    //Used to get a blank tile
           get {
            return mBlankTile;
        }
    }

    protected virtual	void	Awake() {
	}

	//procected means only this & derived classess can access this member, virtual means this can be overidded in derived classes
	protected	virtual	void Start () {
	
	}
	
	protected	virtual	void Update () {
	
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

    static  void    SetupPrefabs() {
        if (!StaticsLoaded) {
            if (sSpriteSheet == null) {
                sSpriteSheet = Resources.LoadAll<Sprite>(GameManager.SpriteFolder + sJewelSpriteFile);
            }
            if (mBlankTile == null) {
                mBlankTile = Resources.Load<Sprite>(GameManager.SpriteFolder + sBlankSpriteFile);
            }
            if (mTilePrefab == null) {
                mTilePrefab = Resources.Load<GameObject>(GameManager.PrefabFolder + sTilePrefabName);
            }
            if(sSpriteSheet!=null && mBlankTile!=null && mTilePrefab!=null) {
                StaticsLoaded = true;
            } else {
                GameManager.DebugMsg("Load Error");
            }
        }
    }

    //Make a static creation method, which allows for any Object dervied from Jewel to be created
    public	static	T Create<T>(Vector2 tPosition) where T:Jewel {
        SetupPrefabs();
		GameObject	tGO=Instantiate<GameObject>(mTilePrefab);		//Construct a new game object
		T tJewel=tGO.AddComponent<T> ();		//Add Jewel script of correct derived type to this object
        tJewel.transform.position = tPosition;
		tJewel.mSR = tGO.GetComponent<SpriteRenderer>();		//get sprite renderer
		tJewel.mSR.sprite=GetSpriteAtIndex(tJewel.SpriteIndex);		//Will call correct inherited version of the code to get its sprite index
		return	tJewel;
	}


	public	abstract	uint	SpriteIndex {	//Base clase does not implement these, but inherited classes must
		get;
	}

	public	abstract	string Name {		//Needs to be defined in inheritted class
		get;
	}

    protected virtual void OnTriggerEnter2D(Collider2D vOther) {        //Default handler, this add the item to the players inventory
        if (vOther.tag == "Player") {
            Destroy(gameObject);        //Destroy item
            Player tPlayer = GameManager.GetPlayer(0);
            if(tPlayer) {
                Jewel tJewel = GetComponent<Jewel>();
                tPlayer.Inventory.Add(tJewel);  //Add to player inventory
            }
        }
    }
}
