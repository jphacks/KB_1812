using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class roomScript : MonoBehaviour {
    InputField roomInput;
    string roomNumber;
    Text outText;
    public string room_text;

	// Use this for initialization
    void OnEnable () {
        roomInput = GameObject.Find("InputField").GetComponent<InputField>();
        outText = GameObject.Find("outText").GetComponent<Text>();
	}

	void Start()
    {
        DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void madeRoomButton(){
        SceneManager.LoadScene("AstronautGame");
    }

    public void goRoomButton(){

        room_text = roomInput.text;
        Debug.Log(room_text.Length);
        if(room_text.Length == 6){
            SceneManager.LoadScene("AstronautGame");
        }else{
            outText.text = "ルームナンバーが違います";
            roomInput.text = "";
        }

    }
}
