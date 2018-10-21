using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class GetTpuchedPositionGeo : MonoBehaviour
{
	public Camera camera;

	public GameObject sphe;

	Vector3 touchedpos;

	Vector2d destipos;

	private AbstractMap _map;

	GameObject insetance;

    //Firebaseの設定用変数
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser loginUser;
    DatabaseReference reference;

	// Use this for initialization
	void Start()
	{
		_map = GameObject.Find("Map").GetComponent<AbstractMap>();

		insetance = Instantiate(sphe, Vector3.zero, Quaternion.identity) as GameObject;

        //auth関係初期設定
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        loginUser = auth.CurrentUser;
        //database関係初期設定
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hamukatsu3-aba8b.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        if (loginUser != null)
        {
            Debug.Log("ログイン済");
        }

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();

			if (Physics.Raycast(ray, out hit, 10000))
			{
				Vector3 touchedpos = hit.point;
				insetance.transform.position = touchedpos;
				//Instantiate(sphe, touchedpos, Quaternion.identity);
				destipos = _map.WorldToGeoPosition(touchedpos);

       

			}
		}
	}
    // firebase周りの実装
    public void SetDestination()
    {
        // destiposをサーバに登録する

        // シーンをマップに移動(MapViewer)

        //メモ：緯度、経度はさかなから渡される予定なので、今は仮で置いてる
        //double latitude = 40.6892;
        //double longitude = -74.0445;

        double latitude = destipos.x;
        double longitude = destipos.y;

        //key発行用スクリプト
        //ランダムで発行
        string roomKey = Random.Range(100000, 999999).ToString();
        //Firebaseにデータ保存

        RoomClass room = new RoomClass(latitude, longitude);
        UserClass user = new UserClass(loginUser.DisplayName, false, 0);

        string roomJson = JsonUtility.ToJson(room);
        string userJson = JsonUtility.ToJson(user);

        reference.Child("rooms").Child(roomKey).SetRawJsonValueAsync(roomJson);
        reference.Child("rooms").Child(roomKey).Child("users").Child(loginUser.UserId).SetRawJsonValueAsync(userJson);

        // TODO シーンをMapViewerに移動おねがいしますー

        SceneManager.LoadScene("MapViewer");
    }

}
