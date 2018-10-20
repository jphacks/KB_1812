using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SigninScript : MonoBehaviour {

    InputField inputMail, inputPass;
    string MailText, PassText;

	// Use this for initialization
	void Start () {
        inputMail = GameObject.Find("MailInput").GetComponent<InputField>();
        inputPass = GameObject.Find("PassInput").GetComponent<InputField>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick(){
        MailText = inputMail.text; //入力したMailAdressを取得
        PassText = inputPass.text; //入力したPasswordを取得

        //debug用
        Debug.Log(MailText);
        Debug.Log(PassText);
    }
}
