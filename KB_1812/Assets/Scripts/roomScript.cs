using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class roomScript : MonoBehaviour {
    InputField roomInput;
    string roomNumber;
    Text outText;

    //Firebaseの設定用変数
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser loginUser;
    DatabaseReference reference;

	// Use this for initialization
    void OnEnable () {
        roomInput = GameObject.Find("InputField").GetComponent<InputField>();
        outText = GameObject.Find("outText").GetComponent<Text>();
	}

    void Start()
    {
        //auth関係初期設定
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        loginUser = auth.CurrentUser;
        //database関係初期設定
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://kb-1812.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;    

        if (loginUser != null)
        {
            Debug.Log("ログイン出来てるんご");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void madeRoomButton(){
        //ここではFirebase関係の処理はなし
        //次のシーンで、与えられた緯度、経度
        //Parkerからここのシーンはいただいてちょ
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
        //firebase関係の処理メソッド
        joinRoom(room_text);

    }

    void joinRoom(string room_id) {
        if (loginUser == null) {
            Debug.Log("ログイン出来てないよin JoinRoom");
        }
        User user = new User(loginUser.DisplayName, false, 0);
        string userJson = JsonUtility.ToJson(user);
        reference.Child("rooms").Child(room_id).Child("users").Child(loginUser.UserId).SetRawJsonValueAsync(userJson);  
    }
}
