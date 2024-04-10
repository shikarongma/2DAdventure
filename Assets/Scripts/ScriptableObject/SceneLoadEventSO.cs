using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject//���ݳ������ز������¼�
{
    //boolֵ�ж��Ƿ�Ҫ���Ž��뽥���Ķ���
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;
    //��������
    /// <summary>
    /// 
    /// </summary>
    /// <param name="locationToLoad">Ҫ���صĳ���</param>
    /// <param name="posToGO">Player��Ŀ������</param>
    /// <param name="fadeScreen">�Ƿ��뽥��</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad,Vector3 posToGO,bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGO, fadeScreen);
    }
}
