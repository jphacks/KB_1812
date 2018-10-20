using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccountScript : MonoBehaviour {

    InputField inputMail, inputPass, inputName;
    string MailText, PassText, NameText;

    void OnEnable(){
        inputName = GameObject.Find("DisplayNameInput").GetComponent<InputField>();
        inputMail = GameObject.Find("MailInput").GetComponent<InputField>();
        inputPass = GameObject.Find("PassInput").GetComponent<InputField>();        
    }
    /*
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    */

    public void OnClick(){
        NameText = inputName.text; //入力したDisplayNameを取得
        MailText = inputMail.text; //入力したMailAdressを取得
        PassText = inputPass.text; //入力したPasswordを取得

        //debug用
        Debug.Log(NameText);
        Debug.Log(MailText);
        Debug.Log(PassText);
    }
}
