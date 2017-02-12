using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	InventoryDisplay mInventoryDisplay;        // Link to to this player Inventory display

    Inventory   mInventory;		//Link to players Inventory

	public	static	readonly	string	PlayerTag = "Player";


    [Header("Set Speed")]
    public  float   Speed=10f;
    public float    RotateSpeed = 100f;


    // Use this for initialization
    void Start () {
        mInventory = new Inventory();	        //Give player a new inventory
        GameManager.AddPlayer(this);
	}

	public	InventoryDisplay SetInventoryDisplay {
		set {
			if (value != null) {		//Cross link Inventory Display and Player
				mInventoryDisplay = value;
				mInventoryDisplay.SetPlayer = this;
			} else {
				if (mInventoryDisplay != null) {		//Unlink Inventory Display
					mInventoryDisplay.SetPlayer = null;
				}
				mInventoryDisplay = null;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
        float tY = InputController.GetInput(InputController.Directions.MoveY);
        float tX = InputController.GetInput(InputController.Directions.MoveX);
        transform.Rotate(0,0,360f*-tX*Time.deltaTime*RotateSpeed);
        transform.position += transform.rotation * Vector3.up * tY * Speed*Time.deltaTime;
    }

    public  Inventory Inventory {       //Allow Access to player Inventory
        get {
            return mInventory;
        }
    }

	public	void	PlayerHitGem(Jewel vJewel) {
		GameManager.DebugMsg ("Player hit Gem:" + vJewel.GetType ().Name);
		Inventory.Add(vJewel);  //Add to player inventory
	}

    public void Removed() {
        Destroy(gameObject);
    }

	public	int	PlayerNumber {		//Get Player number from GameManager
		get {
			return	GameManager.PlayerNumber(this);
		}
	}
}
