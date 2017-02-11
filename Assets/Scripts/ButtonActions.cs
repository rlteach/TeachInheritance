using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour {

    int mCount = 0;

    public void Seed() {
        mCount += 5;
        ReSeed();
    }

    public void    ReSeed() {
        if (mCount > 0) {
            Vector2 tSize = GameManager.GameSize;
            Vector2 tPosition = new Vector2(Random.Range(-tSize.x, tSize.x), Random.Range(-tSize.y, tSize.y));
            Jewel tJewel = RandomItem(tPosition);
            if (tJewel != null) {
                mCount--;
            }
            Invoke("ReSeed", 1f);
        }
    }

    public	Jewel   RandomItem(Vector2 vPosition) {     //Make up random Jewels
        int tRandom = Random.Range(0, 7);
        switch(tRandom) {
			case    0:
				return  Jewel.Create<BlueStone>(vPosition);
            case    1:
                return  Jewel.Create<GreenEmerald>(vPosition);
            case    2:
                return Jewel.Create<OrangeTigersEye>(vPosition);
			case    3:
				return Jewel.Create<PurpleStone>(vPosition);
			case    4:
				return Jewel.Create<RedRuby>(vPosition);
			case    5:
				return Jewel.Create<WhileDiamond>(vPosition);
			case    6:
				return Jewel.Create<YellowStone>(vPosition);
        }
        return null;
    }
}
