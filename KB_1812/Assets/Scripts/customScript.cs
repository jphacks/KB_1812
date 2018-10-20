using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customScript : MonoBehaviour {
    private Animator animator;
   
	// Use this for initialization
	void Start () {
        animator = transform.GetChild(1).GetComponentInChildren<Animator>();
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
}
