using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class LoginedScript : MonoBehaviour {

    // TODO
    // ログインの後に，名前を変える処理を行う．
    // そのときに，値が変わったのを取れるかどうか

    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference reference;

    public Text userName;

    // Use this for initialization
    void Start () {

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;

        if(user != null){
            // テキストの表示
            this.userName.text = user.DisplayName;
            Debug.Log(user.DisplayName);
        }

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hamukatsu2-9bebb.firebaseio.com/");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        Debug.Log(user.UserId);
        Debug.Log(user.DisplayName);


        User newUser = new User(user.DisplayName);

        string json = JsonUtility.ToJson(newUser);

        reference.Child("users").Child(user.UserId).SetRawJsonValueAsync(json);

        FirebaseDatabase.DefaultInstance.GetReference("users").ValueChanged += HandleValueChanged;

    }

    // Update is called once per frame
    void Update () {
		
	}

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        // Do something with the data in args.Snapshot
        Debug.Log("名前が変わりました");
        Debug.Log(args.Snapshot.GetRawJsonValue());

        // TODO ここでの効率のよい値の取得の仕方



    }

    public void ChangeName(){

        Dictionary<string, object> result = new Dictionary<string, object>();
        result["name"] = "パーカー";

        Debug.Log("ちぇんじねいむ");

        //User newUser = new User(user.UserId, "Parker");
        //string json = JsonUtility.ToJson(newUser);

        //reference.Child("users").SetRawJsonValueAsync(json);

        reference.Child("users").Child(user.UserId).UpdateChildrenAsync(result);

    }
}
