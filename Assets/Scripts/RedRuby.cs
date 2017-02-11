using UnityEngine;
using System.Collections;

public class RedRuby : Jewel {

	public	override	string	Name {
		get	{
			return	"Red Ruby";
		}
	}

	protected override uint SpriteIndex {
		get {
			return 0;
		}
	}
}
