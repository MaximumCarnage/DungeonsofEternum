using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

	private int enemyCount;
	public GameObject go_SwordPrefab,go_WolfPrefab,go_ChiefPrefab,go_ArcherPrefab;
	public GameObject go_MagePrefab,go_KnightPrefab,go_GunWomanPrefab;
	public GameObject go_MagePort,go_GunWomanPort,go_KnightPort;
	public GameObject go_MageImpact,go_SwordImpact,go_GunImpact;


	public BaseCombatCharClass SelectedChar;
	private Vector3 MouseLocWorld;
	public bool attackMode;

	public List<GameObject> EnemySpawns;
	public List<GameObject> HeroSpawns;
	public List<BaseCombatCharClass> HeroRefs;
	private int heroAmount;
	public bool MovingChar,AITurn;
	public bool canWalk,InRange;

	// Use this for initialization
	void Start () {
		enemyCount = DungeonManager.instance.enemyNumber;
		SpawnEnemies(DungeonManager.instance.enemyType,enemyCount);
		SpawnHeroes();

	}
	

	void Update () {

	
		
 		// if(Input.GetMouseButtonDown(1) && SelectedChar != null && attackMode){

		//  }
		if(Input.GetMouseButton(0)){
			Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        	RaycastHit2D[] hit=Physics2D.RaycastAll(rayPos, Vector2.zero, 0f);
			for(int i = 0;i<hit.Length;i++){
				if(hit[i].transform.gameObject.tag == "Hero"){
					
					if(SelectedChar !=null){
					MovingChar = false;
					SelectedChar.anim.SetBool("Walking",false);
				}
				if(hit[i].transform.gameObject.GetComponent<BaseCombatCharClass>().canTakeTurn){
					SelectCharacter(hit[i].transform.gameObject.GetComponent<BaseCombatCharClass>());
				}
				
			}
	
			}

		}
    	
		if(Input.GetMouseButtonDown(1) && SelectedChar != null && !attackMode && SelectedChar.canTakeTurn){
			Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        	RaycastHit2D[] hit=Physics2D.RaycastAll(rayPos, Vector2.zero, 0f);
			for(int i = 0;i<hit.Length;i++){
				if(hit[i].transform.gameObject.tag == "MoveCircle"){
					
					MovingChar = true;
			 		MouseLocWorld =  new Vector3(Camera.main.ScreenToWorldPoint (Input.mousePosition).x,Camera.main.ScreenToWorldPoint (Input.mousePosition).y,0);
					SelectedChar.canTakeTurn = false;
					heroAmount++;
					

				}

			}
		}
		if(Input.GetMouseButtonDown(1) && SelectedChar != null && attackMode &&  SelectedChar.canTakeTurn){
			 SelectedChar.canTakeTurn = false;
			 heroAmount++;
			Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        	RaycastHit2D[] hit=Physics2D.RaycastAll(rayPos, Vector2.zero, 0f);
			for(int i = 0;i<hit.Length;i++){
				if(hit[i].transform.gameObject.tag == "RangeCircle"){
					InRange = true;
				}
               if(hit[i].transform.gameObject.tag == "Enemy" && InRange){
				   SelectedChar.EnemyTarget = hit[i].transform.gameObject;
				   SelectedChar.anim.SetBool("Attacking",true);
			   }

			}
		}

		if(MovingChar){
			if(MouseLocWorld.x < SelectedChar.gameObject.transform.position.x){
				 SelectedChar.gameObject.transform.localScale=new Vector3(-1,1,1);
			}
			else{
				 SelectedChar.gameObject.transform.localScale=new Vector3(1,1,1);
			
			}
			SelectedChar.anim.SetBool("Walking",true);
			float step =  3f * Time.deltaTime; 
        	SelectedChar.gameObject.transform.position = Vector3.MoveTowards(SelectedChar.transform.position, MouseLocWorld, step);
			
		}
		if (MovingChar && Vector3.Distance(SelectedChar.transform.position, MouseLocWorld) < 0.001f)
        	{
			
				SelectedChar.anim.SetBool("Walking",false);
        		MovingChar = false;
        	}	
			
	}

	public void AttackButton(){
		if(attackMode){
			SelectedChar.AttackMode.SetActive(false);
			SelectedChar.selectArrow.SetActive(true);
			attackMode = false;
		}else{

			SelectedChar.AttackMode.SetActive(true);
			SelectedChar.selectArrow.SetActive(false);
			attackMode = true;
		}
		
	}

	
	public void SpawnHeroes(){
		List<GameObject> tempSpawns = HeroSpawns;
		int HeroNum = DungeonManager.instance.InventorySystem.HeroInventory.Count;

		for(int i = 0; i < HeroNum;i++){
			
			int randLoc = Random.Range(0,tempSpawns.Count);
			if(DungeonManager.instance.InventorySystem.HeroInventory[i] == "Mage"){
				GameObject temp = Instantiate(go_MagePrefab,tempSpawns[randLoc].transform.position,Quaternion.identity);
				temp.GetComponent<BaseCombatCharClass>().combatMgr = this;
				HeroRefs.Add(temp.GetComponent<BaseCombatCharClass>());
			}
			if(DungeonManager.instance.InventorySystem.HeroInventory[i] == "Knight"){
				GameObject temp = Instantiate(go_KnightPrefab,tempSpawns[randLoc].transform.position,Quaternion.identity);
				temp.GetComponent<BaseCombatCharClass>().combatMgr = this;
				HeroRefs.Add(temp.GetComponent<BaseCombatCharClass>());
			}
			if(DungeonManager.instance.InventorySystem.HeroInventory[i] == "GunWoman"){
				GameObject temp = Instantiate(go_GunWomanPrefab,tempSpawns[randLoc].transform.position,Quaternion.identity);
				temp.GetComponent<BaseCombatCharClass>().combatMgr = this;
				HeroRefs.Add(temp.GetComponent<BaseCombatCharClass>());
			}
			tempSpawns.RemoveAt(randLoc);
		}
	}

	public void SelectCharacter(BaseCombatCharClass Character){
		if(SelectedChar != null){

			SelectedChar.AttackMode.SetActive(false);
			SelectedChar.selectArrow.SetActive(false);
			SelectedChar.moveCirc.SetActive(false);
			SelectedChar.rangeCirc.SetActive(false);
			go_MagePort.SetActive(false);
			go_KnightPort.SetActive(false);
			go_GunWomanPort.SetActive(false);
			
			SelectedChar = Character;
			if(attackMode){
				SelectedChar.AttackMode.SetActive(true);
				SelectedChar.selectArrow.SetActive(false);
			}else{
				SelectedChar.AttackMode.SetActive(false);
				SelectedChar.selectArrow.SetActive(true);
			}
			
			SelectedChar.moveCirc.SetActive(true);
			SelectedChar.rangeCirc.SetActive(true);
		 	if(SelectedChar.charType == "Mage"){
				go_MagePort.SetActive(true);
			}
			if(SelectedChar.charType == "Knight"){
				go_KnightPort.SetActive(true);
			}
			if(SelectedChar.charType == "GunWoman"){
				go_GunWomanPort.SetActive(true);
			}
		}
		else{
			SelectedChar = Character;
			SelectedChar.selectArrow.SetActive(true);
			SelectedChar.moveCirc.SetActive(true);
			SelectedChar.rangeCirc.SetActive(true);

			if(SelectedChar.charType == "Mage"){
				go_MagePort.SetActive(true);
			}
			if(SelectedChar.charType == "Knight"){
				go_KnightPort.SetActive(true);
			}
			if(SelectedChar.charType == "GunWoman"){
				go_GunWomanPort.SetActive(true);
			}
		}
		
	
	}
	

	public void SpawnEnemies(string enemy,int count){
		Debug.Log("Test");
		List<GameObject> tempList = EnemySpawns; 
		if(enemy == "Sword"){
			for(int i = 0;i <count;i++)
			{
				int spawnLoc = Random.Range(1,tempList.Count);
				GameObject temp = Instantiate(go_SwordPrefab,tempList[spawnLoc].transform.position,Quaternion.identity);
				tempList.Remove(EnemySpawns[spawnLoc]);
			}
		}
		else if(enemy == "Archer"){
				for(int i = 0;i <count;i++)
				{
				int spawnLoc = Random.Range(1,tempList.Count);
				GameObject temp = Instantiate(go_ArcherPrefab,tempList[spawnLoc].transform.position,Quaternion.identity);
				tempList.Remove(EnemySpawns[spawnLoc]);
			}
			
		}
		else if(enemy == "Chief"){
			
				int spawnLoc = Random.Range(1,tempList.Count);
				GameObject temp = Instantiate(go_ChiefPrefab,tempList[spawnLoc].transform.position,Quaternion.identity);
				tempList.Remove(EnemySpawns[spawnLoc]);
				count--;
			for(int i = 0;i <count;i++)
			{
				int spawnLoc2 = Random.Range(1,tempList.Count);
				GameObject temp2 = Instantiate(go_SwordPrefab,tempList[spawnLoc].transform.position,Quaternion.identity);
				tempList.Remove(EnemySpawns[spawnLoc]);
			}
			
			
		}
		else if(enemy == "Wolf"){
			for(int i = 0;i <count;i++)
			{
				int spawnLoc = Random.Range(1,tempList.Count);
				GameObject temp = Instantiate(go_WolfPrefab,tempList[spawnLoc].transform.position,Quaternion.identity);
				tempList.Remove(EnemySpawns[spawnLoc]);
			}
			
		}
	}

}
