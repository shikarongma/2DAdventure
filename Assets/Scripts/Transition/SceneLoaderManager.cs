using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public Transform playerTrans;

    public Vector3 firstPosition;

    public Vector3 menuPosition;

    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;

   
    [Header("发出事件")]
    //相机边界，相机事件发出
    public VoidEventSO AfterSceneLoadedEventSO;
    //渐入渐出场景事件发出
    public FadeCanvasSO fadeCanvasEventSO;

    [Header("场景")]
    public GameSceneSO firstLoadScene;

    public GameSceneSO menuScene;

    private GameSceneSO currentLoadScene;//当前场景
    private GameSceneSO sceneToLoad;//下一个场景
    private Vector3 positionToGo;
    private bool fadeScreen;

    private bool isLoading;//判断当前是否正在转换场景

    //渐入渐出等候时间
    public float fadeDuration;
    private void Awake()
    {
        //异步加载场景
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        currentLoadScene = firstLoadScene;
        //加载当前场景
        currentLoadScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }

    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }



    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        //如果当前正在交换场景，玩家按键不会有任何操作
        if (isLoading)
            return;
        isLoading = true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        //调用协程
        if (currentLoadScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
    }

    //协程计算是否渐入渐出完毕并且卸载原有场景加载新场景
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //TODO 实现渐入渐出 变黑
            fadeCanvasEventSO.FadeIn(fadeDuration);
        }

        yield return new WaitForSeconds(fadeDuration);
        //卸载场景,Unity自带方法
        yield return currentLoadScene.sceneReference.UnLoadScene();

        playerTrans.gameObject.SetActive(false);

        //加载新场景
        LoadNewScene();

    }

    //加载新场景方法
    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }
    /// <summary>
    /// 场景加载完成后
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadScene = sceneToLoad;//当前场景切换为换到的那个场景
        //移动完坐标后，再将player启动
        playerTrans.position = positionToGo;
        if(!(currentLoadScene.sceneType==SceneType.Menu))
            playerTrans.gameObject.SetActive(true);


        if (fadeScreen)//渐入渐出效果 变透明
        {
            //TODO
            fadeCanvasEventSO.FadeOut(fadeDuration);
        }

        isLoading = false;

        //场景加载完成后事件
        AfterSceneLoadedEventSO?.RaiseEvent();
    }
}
