using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    public ShowInventory ShowInventory;        // Link to to this player Inventory display

    Inventory   mInventory;


    [Header("Set Speed")]
    public  float   Speed=10f;
    public float    RotateSpeed = 100f;

    Rigidbody2D mRB;

    // Use this for initialization
    void Start () {
        mInventory = new Inventory();	        //Give player a new inventory
        GameManager.AddPlayer(this);
        mRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float tY = InputController.GetInput(InputController.Directions.MoveY);
        float tX = InputController.GetInput(InputController.Directions.MoveX);
        transform.Rotate(0,0,360f*tX*Time.deltaTime*RotateSpeed);
        transform.position += transform.rotation * Vector3.up * tY * Speed*Time.deltaTime;
    }

    public  Inventory Inventory {       //Allow Access to player Inventory
        get {
            return mInventory;
        }
    }

    public void Remove() {
        Destroy(gameObject);
    }
}
