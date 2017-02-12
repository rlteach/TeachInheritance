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


    static bool StaticsLoaded = false;

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
	
	void Update () {
		Move ();			//Get Item to Move
		if(GameManager.OffScreen(transform.position)) {		//Check if item off screen
			OffSceen ();		//Call OffScreen code
		}
	}

	protected	virtual	void OffSceen() {
		Destroy(gameObject);		//Default action is destroy game object
	}

	protected	virtual	void Move() {		//Called to move Object
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

    void OnTriggerEnter2D(Collider2D vOther) {        //Default handler, this add the item to the players inventory
		Jewel tJewel = GetComponent<Jewel>();		//Get Jewel component
		if (vOther.tag == Player.PlayerTag) {
			Player tPlayer = vOther.GetComponent<Player> ();		//Get player we collided with
			CollidedWithPlayer (tJewel, tPlayer);
		} else if (vOther.tag == Jewel.JewelTag) {
			Jewel tOtherJewel = vOther.GetComponent<Jewel> ();		//Get Jewel we collided with
			CollidedWithJewel(tOtherJewel);
		}
    }

	protected	virtual void	CollidedWithPlayer(Jewel vJewel,Player vPlayer) {		//Default action
		vPlayer.PlayerHitGem (vJewel);		//Notify player
		GameManager.RemoveJewel(vJewel);		//Tell Game manager to remove Jewel
	}

	protected	virtual void	CollidedWithJewel(Jewel vOtherJewel) {
		GameManager.RemoveJewel(vOtherJewel);		//Tell Game manager to remove other Jewel
	}


	public virtual void Removed() {		//Called when GameManager has removed Jewel
		Destroy(gameObject);		//Default action is to destroy it
	}


	void OnDestroy() {			//Print Debug Message
		GameManager.DebugMsg (GetType ().Name + " Destroyed");
	}
}
