using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Game Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject//Addressable������Ϣ
{
    public SceneType sceneType;//��������
    public AssetReference sceneReference;//Asset����
}
