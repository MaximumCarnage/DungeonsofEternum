using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonPlayer : MonoBehaviour {

	public Animator playerAnim;
	public float f_Speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)){
			playerAnim.SetInteger("Direction",4);
			playerAnim.SetBool("IsMoving",true);
			transform.Translate(new Vector3(0,f_Speed,0));
		}
		else if(Input.GetKey(KeyCode.A)){
			playerAnim.SetInteger("Direction",2);
			playerAnim.SetBool("IsMoving",true);
			transform.Translate(new Vector3(-f_Speed,0,0));
		}
		else if(Input.GetKey(KeyCode.S)){
			playerAnim.SetInteger("Direction",1);
			playerAnim.SetBool("IsMoving",true);
			transform.Translate(new Vector3(0,-f_Speed,0));
		}
		else if(Input.GetKey(KeyCode.D)){
			playerAnim.SetInteger("Direction",3);
			playerAnim.SetBool("IsMoving",true);
			transform.Translate(new Vector3(f_Speed,0,0));
		}
		else{
			playerAnim.SetBool("IsMoving",false);
			playerAnim.SetInteger("Direction",0);
		}
	}
}
