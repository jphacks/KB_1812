using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;

public class MakeScorePoint : MonoBehaviour
{
  /* 日本において
   * 経度 0.01 = 1km // longitude
   * 緯度 0.009 = 1km // latitude
   * 
   * つまり，
   * 経度 0.002 = 200m
   * 緯度 0.0018 = 200m
   */

	[SerializeField]
	private Vector2d destipoint;

	[SerializeField]
	private int count;// スコア地点の数

	[SerializeField]
	private GameObject score_object;

	private AbstractMap _map;

	// Use this for initialization
	void Start()
	{
		_map = GameObject.Find("Map").GetComponent<AbstractMap>();
		//目的地の緯度経度を取りに行く
		for (int i = 0; i < count; i++)
		{
			double rnd_longi = Random.Range(-0.002000f, 0.002000f);
			double rnd_latit = Random.Range(-0.0018000f, 0.0018000f);
			Vector2d rnd_geo = new Vector2d(rnd_longi, rnd_latit);
			Debug.Log(rnd_geo);
			Vector3 rnd_pos = _map.GeoToWorldPosition(rnd_geo, false);
			Vector2d score_geo = rnd_geo + destipoint;
			Vector3 score_point = _map.GeoToWorldPosition(score_geo, true);
			Debug.Log(rnd_pos);
			Instantiate(score_object, score_point, Quaternion.identity);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
