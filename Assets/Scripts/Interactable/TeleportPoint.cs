using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public GameSceneSO SceneToGo;//下一个场景
    public Vector3 PositionToGo;//去到下一个场景的位置坐标

    public SceneLoadEventSO loadEventSO;//发出事件
    private AudioDefination audioDefination;//加载音乐

    private void Awake()
    {
        audioDefination = GetComponent<AudioDefination>();
    }
    public void TiggerAction()
    {
        //呼叫事件
        loadEventSO.RaiseLoadRequestEvent(SceneToGo, PositionToGo, true);
        audioDefination.PlayAudioClip();
    }

    public void OnExitGame()//定义一个退出游戏的方法
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//如果是在unity编译器中
#else
        Application.Quit();//否则在打包文件中
#endif
    }
}
