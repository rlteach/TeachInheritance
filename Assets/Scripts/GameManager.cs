using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;          //Used for String builder

public class GameManager : Singleton {

    readonly static  public string SpriteFolder = "Sprites/";           //Keep the folders stored here for easy maintainance
    readonly static public string PrefabFolder = "Prefab/";             //Other scripts can reference these, makes it easy to change 

    private static GameManager GM;          //Keep reference to singleton here
    private List<Player>     mPlayers;

	private List<Jewel>     mJewels;

    void    Awake() {
        if (CreateSingleton<GameManager>(ref GM)) {       //uses ref to allow variable to be passed back to caller, this creates a singleton of this type
            mPlayers = new List<Player>();
			mJewels = new List<Jewel> ();
        }
    }

	#region Player		//Allow section to be collapsed for easier reading

    public  static  int PlayerCount {           //Get number of players in game
        get {
            return GM.mPlayers.Count;
        }
    }

	public	static	int		PlayerNumber(Player vPlayer) {
		return	GM.mPlayers.IndexOf (vPlayer);
	}

    public  static  void    AddPlayer(Player vPlayer) {               //Add player to game
		GameObject[]	InventoryDisplayGO=GameObject.FindGameObjectsWithTag(InventoryDisplay.InventoryTag);		//Get all Inventory Display Objects
        GM.mPlayers.Add(vPlayer);		//Add player to list of players
		int	tIndex = GM.mPlayers.IndexOf (vPlayer);			//Find player index to link with Inventory
		if (tIndex < InventoryDisplayGO.Length) {					//Do we have enough inventories
			InventoryDisplay tID=InventoryDisplayGO[tIndex].GetComponent<InventoryDisplay>();
			vPlayer.SetInventoryDisplay = tID;		//Link Inventory display to player
		}
    }

    public static void      RemovePlayer(Player vPlayer) {           //Remove player from game
        GM.mPlayers.Remove(vPlayer);
        vPlayer.Removed();       //Tell player to remove themselves
		vPlayer.SetInventoryDisplay=null;		//Unlink from Inventory
    }
	#endregion

	#region Jewel

	public	static	int	JewelCount {		//Get number of Jewels
		get {
			return	GM.mJewels.Count;
		}
	}

	public static void AddJewel(Jewel vJewel) {
		GM.mJewels.Add (vJewel);
	}

	public static void RemoveJewel(Jewel vJewel) {
		GM.mJewels.Remove(vJewel);		//Remove Jewel from list
		vJewel.Removed ();			//Tell Jewel its been removed
	}
	#endregion


    public  static  string  DebugText {     //Get Debug text
        get {
            StringBuilder tString = new StringBuilder();        //Faster than string + concat
			tString.AppendFormat("Jewels:{0}",GM.mJewels.Count);
			tString.AppendLine ();
			for (int tI = 0; tI < GM.mPlayers.Count; tI++) {
				tString.AppendFormat("Player {0} Items:{1}",tI,GM.mPlayers[tI].Inventory.Items.Count);
				if (tI < GM.mPlayers.Count - 1) {		//Only add line if not on last one
					tString.AppendLine ();
				}
			}
            return tString.ToString();
        }
    }

    public  static  Vector2 GameSize {		//Central place to get screen bound from
        get {
            return new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
        }
    }

	public  static  bool OffScreen (Vector3 vPosition) {		//Check if outside bounds
		Vector2 tGameSize = GameSize;		//Cache for speed
		return (vPosition.x < -tGameSize.x		//Check far left
			|| vPosition.x > tGameSize.x		//Check far right
			|| vPosition.y < -tGameSize.y		//Check bottom
			|| vPosition.y > tGameSize.y);		//Check top
	}

}
