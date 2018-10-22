using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

using Firebase.Database;
using Firebase.Unity.Editor;

public class SigninScript : MonoBehaviour {

    // 入力要素
    InputField inputMail, inputPass;
    string mMailText, mPassText;

    // Firebaseを使うために必要
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference reference;

    bool isMove;

    void OnEnable()
    {
        inputMail = GameObject.Find("MailInput").GetComponent<InputField>();
        inputPass = GameObject.Find("PassInput").GetComponent<InputField>();

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hamukatsu3-aba8b.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (auth != null)
        {
            Debug.Log("start()");
        }

        isMove = false;
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        // TODO 飛ぶシーンが違う
        //if (isMove) SceneManager.LoadScene("Main");
        if (isMove) SceneManager.LoadScene("SelectRoom");
    }

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
