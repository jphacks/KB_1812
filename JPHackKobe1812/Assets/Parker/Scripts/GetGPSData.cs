using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class GetGPSData : MonoBehaviour
{
	[SerializeField]
	private Text latitude;//緯度表示Text
	[SerializeField]
	private Text longitude;//経度表示Text

	private AbstractMap abstractMap;

	private void Start()
	{
		InvokeRepeating("CallGetData", 1.0f, 3.0f);
		abstractMap = GameObject.Find("Map").GetComponent<AbstractMap>();

	}

	void CallGetData()
	{
		StartCoroutine("GetData");
	}


	/* ---------- GetData ----------
	 * GPSのデータを取得するコルーチン
	 */
	IEnumerator GetData()
	{
		Debug.Log("GPS情報参照開始");

		//--- 端末のGPS機能が使えないとき ---
		if (!Input.location.isEnabledByUser)
		{
			Debug.Log("GPS is cannot use.");
			latitude.text = "GPSをONにしてね";
			longitude.text = "GPSをONにしてね";

			yield break;
		}


		Input.location.Start();
		int maxWait = 3;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		if (maxWait < 1)
		{
			Debug.Log("Timed out");
			yield break;
		}
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			Debug.Log("Unable to determine device location");
			latitude.text = "GPS情報を取得できません";
			longitude.text = "GPS情報を取得できません";

			yield break;
		}
		else
		{
      
			latitude.text = "緯度 : " + Input.location.lastData.latitude;
			longitude.text = "経度 : " + Input.location.lastData.longitude;

			//--- 地図の中心に緯度経度を指定 ---
      
			Vector2d pos = new Vector2d(Input.location.lastData.latitude, Input.location.lastData.longitude);
			//abstractMap.CenterLatitudeLongitude = pos;
			abstractMap.SetCenterLatitudeLongitude(pos);

			Debug.Log("Location: " +
			Input.location.lastData.latitude + " " +
			Input.location.lastData.longitude + " " +
			Input.location.lastData.altitude + " " +
			Input.location.lastData.horizontalAccuracy + " " +
			Input.location.lastData.timestamp);
		}
		Input.location.Stop();
	}
}
