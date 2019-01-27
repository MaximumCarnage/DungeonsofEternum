using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour {
	private BaseCombatCharClass ParentCombChar;
	private CombatManager combatMgr;


	void Start()
	{
		ParentCombChar = gameObject.GetComponentInParent<BaseCombatCharClass>();
		combatMgr = ParentCombChar.combatMgr;
	}
	public void EndAttack(){
		if(combatMgr.SelectedChar.charType == "Knight"){
			GameObject.Instantiate(combatMgr.go_SwordImpact,ParentCombChar.EnemyTarget.transform.position,Quaternion.identity);
		}
		if(combatMgr.SelectedChar.charType == "GunWoman"){
			GameObject.Instantiate(combatMgr.go_GunImpact,ParentCombChar.EnemyTarget.transform.position,Quaternion.identity);
		}
		if(combatMgr.SelectedChar.charType == "Mage"){
			GameObject.Instantiate(combatMgr.go_MageImpact,ParentCombChar.EnemyTarget.transform.position,Quaternion.identity);
		}
		combatMgr.InRange = false;
		combatMgr.SelectedChar.anim.SetBool("Attacking",false);
	}
}
