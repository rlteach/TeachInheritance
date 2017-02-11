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

    Jewel   RandomItem(Vector2 vPosition) {     //Make up random Jewels
        int tRandom = Random.Range(1, 5);
        switch(tRandom) {
            case    1:
                return  Jewel.Create<RedRuby>(vPosition);
            case    2:
                return Jewel.Create<OrangeTigersEye>(vPosition);
        }
        return null;
    }
}
