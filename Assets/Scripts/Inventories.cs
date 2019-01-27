using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventories : MonoBehaviour {

	public List<string> HeroInventory;

	
	public void HeroDeath(string deadHero){
		HeroInventory.Remove(deadHero);
	}
}
