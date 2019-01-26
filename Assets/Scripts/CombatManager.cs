using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

	private int enemyCount;
	public GameObject go_SwordPrefab,go_WolfPrefab,go_ChiefPrefab,go_ArcherPrefab;

	public List<GameObject> EnemySpawns;

	// Use this for initialization
	void Start () {
		enemyCount = DungeonManager.instance.enemyNumber;
		SpawnEnemies(DungeonManager.instance.enemyType,enemyCount);


	}
	
	// Update is called once per frame
	void Update () {
		
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
