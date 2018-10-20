using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class roomScript : MonoBehaviour {
    InputField roomInput;
    string roomNumber;
    Text outText;

	// Use this for initialization
    void OnEnable () {
        roomInput = GameObject.Find("InputField").GetComponent<InputField>();
        outText = GameObject.Find("outText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void madeRoomButton(){
        SceneManager.LoadScene("");
    }

    public void goRoomButton(){

        string room_text = roomInput.text;
        Debug.Log(room_text.Length);
        if(room_text.Length == 6){
            SceneManager.LoadScene("AstronautGame");
        }else{
            outText.text = "ルームナンバーが違います";
            roomInput.text = "";
        }

    }
}
