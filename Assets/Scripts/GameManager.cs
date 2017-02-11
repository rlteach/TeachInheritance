using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;          //Used for String builder

public class GameManager : Singleton {

    readonly static  public string SpriteFolder = "Sprites/";           //Keep the folders stored here for easy maintainance
    readonly static public string PrefabFolder = "Prefab/";             //Other scripts can reference these, makes it easy to change 

    private static GameManager GM;          //Keep reference to singleton here
    private List<Player>     mPlayers;

    void    Awake() {
        if (CreateSingleton<GameManager>(ref GM)) {       //uses ref to allow variable to be passed back to caller, this creates a singleton of this type
            mPlayers = new List<Player>();
        }
    }

    public  static  int PlayerCount {           //Get number of players in game
        get {
            return GM.mPlayers.Count;
        }
    }

    public  static  void    AddPlayer(Player vPlayer) {               //Add player to game
        GM.mPlayers.Add(vPlayer);
    }

    public static void      RemovePlayer(Player vPlayer) {           //Remove player from game
        GM.mPlayers.Remove(vPlayer);
        vPlayer.Remove();       //Tell player to remove themselves
    }

    public  static  Player    GetPlayer(int vIndex) {
        if(vIndex>=0 && vIndex<GM.mPlayers.Count) {
            return GM.mPlayers[vIndex];
        }
        return null;
     }

    void Update () {
	}

    public  static  string  DebugText {     //Get Debug text
        get {
            StringBuilder tString = new StringBuilder();        //Faster than string + concat
            tString.AppendFormat("Players:{0}", PlayerCount);
            return tString.ToString();
        }
    }

    public  static  Vector2 GameSize {
        get {
            return new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
        }
    }
}
