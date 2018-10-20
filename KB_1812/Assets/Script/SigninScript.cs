using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class SigninScript : MonoBehaviour {

    // インプットフィールド
    InputField inputMail, inputPass;
    string mMailText, mPassText;


    // Firebaseを使うために必要
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference reference;

    bool isMove;

<<<<<<< HEAD
=======
    void OnEnable(){
        inputMail = GameObject.Find("MailInput").GetComponent<InputField>();
        inputPass = GameObject.Find("PassInput").GetComponent<InputField>();

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hamukatsu2-9bebb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (auth != null)
        {
            Debug.Log("start()");
        }

        isMove = false;
    }


>>>>>>> b290b82d048cb6f943584eb14656e75c66f61a39
	// Use this for initialization
	void Start () {
        inputMail = GameObject.Find("MailInput").GetComponent<InputField>();
        inputPass = GameObject.Find("PassInput").GetComponent<InputField>();
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		
	}
=======

        // TODO 飛ぶシーンが違う
        //if (isMove) SceneManager.LoadScene("Main");
        if (isMove) Debug.Log("シーン移動と切り替える！");
    }

>>>>>>> b290b82d048cb6f943584eb14656e75c66f61a39

    public void OnClick(){
        mMailText = inputMail.text; //入力したMailAdressを取得
        mPassText = inputPass.text; //入力したPasswordを取得

        //debug用
        Debug.Log(mMailText);
        Debug.Log(mPassText);

        login(mMailText, mPassText);

    }

    void login(string email, string password)
    {

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Complete");
            }

            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;

            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);

            Debug.Log("ログインできました");

            isMove = true;

        });
    }
}
