using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class customScript : MonoBehaviour {
    private Animator animator;
    public GameObject subCanvas;
   
	// Use this for initialization
	void Start () {
        animator = subCanvas.GetComponentInChildren<Animator>();
        animator.SetBool("customOpen", false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void customButton(){
        animator.SetBool("customOpen",true);
    }

    public void closeButton(){
        animator.SetBool("customOpen", false);
    }

    public void OnClickDelete(){
        SceneManager.LoadScene("Login");
    }
}
