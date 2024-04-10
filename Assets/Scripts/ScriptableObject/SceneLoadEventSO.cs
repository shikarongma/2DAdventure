using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject//传递场景加载参数的事件
{
    //bool值判断是否要播放渐入渐出的动画
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;
    //启动方法
    /// <summary>
    /// 
    /// </summary>
    /// <param name="locationToLoad">要加载的场景</param>
    /// <param name="posToGO">Player的目的坐标</param>
    /// <param name="fadeScreen">是否渐入渐出</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad,Vector3 posToGO,bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGO, fadeScreen);
    }
}
