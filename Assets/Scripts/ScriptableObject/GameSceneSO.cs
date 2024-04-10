using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Game Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject//Addressable场景信息
{
    public SceneType sceneType;//场景类型
    public AssetReference sceneReference;//Asset引用
}
