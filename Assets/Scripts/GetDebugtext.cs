using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetDebugtext : MonoBehaviour {

    Text mText;     //Link to text

    void    Start() {
        mText = GetComponent<Text>();
        InvokeRepeating("ShowDebugText", 0, 0.5f);      //Update rate
    }

    void    ShowDebugText() {       //Show Debug text
        mText.text = GameManager.DebugText;                
    }

}
