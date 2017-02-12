using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour {

    int mCount = 0;

    public void SeedMore(int vMore) {
        mCount += vMore;
        ReSeed();
    }

    public  void    Clear() {
        mCount = 0;
        GameManager.RemoveAllJewels();
    }

    public void    ReSeed() {
        if (mCount > 0) {
            Jewel tJewel = CreateJewel();
            mCount--;
            Invoke("ReSeed", 1f);
        }
    }

    public  void    SeedJewel(int vNumber) {
        CreateJewel(vNumber);
    }
    public Jewel CreateJewel() {     //Make Random Jewel at random position
        int tNumber = Random.Range(0, Jewel.JewelTypeCount);
        return CreateJewel(tNumber);
    }

    public Jewel CreateJewel(int vNumber) {     //Make a Jewel at random Position
        Vector2 tSize = GameManager.GameSize;
        Vector2 tPosition = new Vector2(Random.Range(-tSize.x, tSize.x), Random.Range(-tSize.y, tSize.y));
        return  CreateJewel(vNumber,tPosition);
    }

    public Jewel   CreateJewel(int vNumber,Vector2 vPosition) {     //Make a jewel at a position
        Jewel tJewel = null;
        switch(vNumber) {
			case    0:
                tJewel = Jewel.Create<BlueStone>(vPosition);
                break;
            case    1:
                tJewel = Jewel.Create<GreenEmerald>(vPosition);
                break;
            case 2:
                tJewel = Jewel.Create<OrangeTigersEye>(vPosition);
                break;
            case 3:
                tJewel = Jewel.Create<PurpleStone>(vPosition);
                break;
            case 4:
                tJewel = Jewel.Create<RedRuby>(vPosition);
                break;
            case 5:
                tJewel = Jewel.Create<WhileDiamond>(vPosition);
                break;
            case 6:
                tJewel = Jewel.Create<YellowStone>(vPosition);
                break;
        }
        if(tJewel!=null) {
            GameManager.AddJewel(tJewel);		//Tell Game manager about new Jewel
        }
        return tJewel;
    }
}
