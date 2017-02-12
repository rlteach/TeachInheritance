using UnityEngine;
using System.Collections;

public class RedRuby : Jewel {


    //These methods override the ones in the base clase
	public	override	string	Name {
		get	{
			return	"Red Ruby";
		}
	}

	public override uint SpriteIndex {       //The object knows which sprite index to use for rendering
		get {
			return 4;
		}
	}

}
