using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

	 public static DungeonManager instance = null;              

        void Awake()
        {
            if (instance == null)
            instance = this;
            
            else if (instance != this)   
            Destroy(gameObject);    
            
            DontDestroyOnLoad(gameObject);
        }
		public void BattleBegin(){
			
		}
}
