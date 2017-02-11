using System.Collections;
using System.Collections.Generic;

public class Inventory  {		//Does not inherit from MonoBehaviour


    public  bool NeedsDisplay {     //Has the invenrory changed, so does it need to be displayed?
        get {
            return mNeedsDisplay;
        }
    }

    public  void    DidDisplay() {      //Flag that it has displayed
        mNeedsDisplay = false;
    }

    bool mNeedsDisplay = false;

    private List<Jewel> mJewels;       //Inventory of Gems

    public  List<Jewel> Items {         //Expose Inventory Item List
        get {
            return mJewels;
        }
    }

    //Set up New inventory
    public  Inventory() {       //Constructor used as this is not a game component, note its not derived from MonoBehavior
        mJewels = new List<Jewel>();
    }

    public  void    Add(Jewel vItem) {     //Add new Jewel
        Items.Add(vItem);
        mNeedsDisplay = true;            //Set Flag to shoiw update is needed
    }

    public  void    Remove(Jewel vItem) {       //Remove jewel
        Items.Remove(vItem);
        mNeedsDisplay = true;
    }
}
