using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassCheckScript : MonoBehaviour {

    InputField PassInput;
    bool is_PassType = false;
    string secrettext;
    Toggle toggle;

	// Use this for initialization
	void Start () {
        PassInput = GameObject.Find("PassInput").GetComponent<InputField>();
        toggle = GameObject.Find("PassCheckToggle").GetComponent<Toggle>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChengeToggle(){
        if(!is_PassType){
            PassInput.contentType = InputField.ContentType.Standard;
            PassInput.textComponent.text = PassInput.text;
            is_PassType = !is_PassType;
        }else{
            secrettext = "";
            foreach(char c in PassInput.text){
                secrettext += "*";
            }
            PassInput.contentType = InputField.ContentType.Password;
            PassInput.textComponent.text = secrettext;
            is_PassType = !is_PassType;
        }
    }
}
