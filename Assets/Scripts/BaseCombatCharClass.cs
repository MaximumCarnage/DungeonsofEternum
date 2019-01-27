using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCombatCharClass : MonoBehaviour {

	public bool selected;
	public GameObject selectArrow,rangeCirc,moveCirc,AttackMode;
	public GameObject EnemyTarget;
	public CombatManager combatMgr;
	public bool canTakeTurn;

	public Animator anim;

	

	public string charType;

	void Start()
	{
		canTakeTurn = true;
		Physics.queriesHitTriggers = true;
		anim = gameObject.GetComponentInChildren<Animator>();
	}
	
	
	


	// void OnMouseDown()
	// {
	// 	if(combatMgr.SelectedChar !=null){
	// 		combatMgr.SelectedChar.anim.SetBool("Walking",false);
	// 	}
	// 	combatMgr.MovingChar = false;
	// 	combatMgr.SelectCharacter(this);
	// }
}
