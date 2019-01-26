using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour {
	public GameObject go_Clash,go_Heroes,go_SkelePortrait,go_ArcherPortrait,go_ChiefPortrait,go_WolfPortrait,go_Fire;
    public string enemyType;
	public int enemyNumber;
    
   
    public static DungeonManager instance = null;              

        void Awake()
        {
            if (instance == null)
            instance = this;
            
            else if (instance != this)   
            Destroy(gameObject);    
            
            DontDestroyOnLoad(gameObject);
        }
		public void BattleBegin(string enemy,int num){
            enemyType = enemy;
            enemyNumber = num;
            go_Clash.SetActive(true);
            go_Heroes.SetActive(true);
            go_Fire.SetActive(true);
            if(enemy == "Sword"){
                go_SkelePortrait.SetActive(true);
            }
             if(enemy == "Archer"){
                go_ArcherPortrait.SetActive(true);
            }
             if(enemy == "Chief"){
                go_ChiefPortrait.SetActive(true);
            }
             if(enemy == "Wolf"){
                go_WolfPortrait.SetActive(true);
            }
            StartCoroutine("Combat");
		
		}

       

        IEnumerator Combat(){
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("CombatScene");
            go_Clash.SetActive(false);
            go_Heroes.SetActive(false);
            go_SkelePortrait.SetActive(false);
            go_ArcherPortrait.SetActive(false);
            go_WolfPortrait.SetActive(false);
            go_ChiefPortrait.SetActive(false);
        }

          
}
