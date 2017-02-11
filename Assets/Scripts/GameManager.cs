using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	static	GameManager	GM;

	void Awake () {
		if (GM == null) {
			GM = this;
			DontDestroyOnLoad (gameObject);
		} else if (GM != this) {
			Destroy (gameObject);
		}
	}

	static	void	CreateJewel() {
	}

	// Update is called once per frame
	void Update () {
	
	}
}
