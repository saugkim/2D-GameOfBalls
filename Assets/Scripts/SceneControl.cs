using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {

    public int currentLevel;
    public int finalLevel;

    void Start ()
    {
        DontDestroyOnLoad(this);
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        finalLevel = SceneManager.sceneCountInBuildSettings - 1;
    }
	
	void Update ()
    {
		
	}
   
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);    
    }

}
