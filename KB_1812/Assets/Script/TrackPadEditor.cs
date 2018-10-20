using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TrackPadScript))]
public class TrackPadEditor : Editor {

    //プロパティーを編集する用
    private SerializedProperty _radiusProperty;
    private SerializedProperty _shouldResetPositionProperty;
    private SerializedProperty _positionProperty;

    //=================================================================================
    //初期化
    //=================================================================================

    private void OnEnable()
    {
        //プロパティ読み込み
        _radiusProperty = serializedObject.FindProperty("_radius");
        _shouldResetPositionProperty = serializedObject.FindProperty("_shouldResetPosition");
        _positionProperty = serializedObject.FindProperty("_position");
    }

    //=================================================================================
    //更新
    //=================================================================================

    //Inspectorを更新する
    public override void OnInspectorGUI()
    {
        //更新開始
        serializedObject.Update();

        //スティックが動く範囲の半径
        float radius = EditorGUILayout.FloatField("Range Radius", _radiusProperty.floatValue);
        if (radius != _radiusProperty.floatValue)
        {
            _radiusProperty.floatValue = radius;
        }

        //指を離した時にスティックが中心に戻るか
        bool shouldResetPosition = EditorGUILayout.Toggle("Return", _shouldResetPositionProperty.boolValue);
        if (shouldResetPosition != _shouldResetPositionProperty.boolValue)
        {
            _shouldResetPositionProperty.boolValue = shouldResetPosition;
        }

        //現在地を表示
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField(
          "Position(-1~1)   X : " +
          _positionProperty.vector2Value.x.ToString("F2") + ",  Y : " +
          _positionProperty.vector2Value.y.ToString("F2")
        );
        EditorGUILayout.EndVertical();

        //更新終わり
        serializedObject.ApplyModifiedProperties();
    }
}
