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

    [Header("�¼�����")]
    public SceneLoadEventSO loadEventSO;

   
    [Header("�����¼�")]
    //����߽磬����¼�����
    public VoidEventSO AfterSceneLoadedEventSO;
    //���뽥�������¼�����
    public FadeCanvasSO fadeCanvasEventSO;

    [Header("����")]
    public GameSceneSO firstLoadScene;

    public GameSceneSO menuScene;

    private GameSceneSO currentLoadScene;//��ǰ����
    private GameSceneSO sceneToLoad;//��һ������
    private Vector3 positionToGo;
    private bool fadeScreen;

    private bool isLoading;//�жϵ�ǰ�Ƿ�����ת������

    //���뽥���Ⱥ�ʱ��
    public float fadeDuration;
    private void Awake()
    {
        //�첽���س���
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        currentLoadScene = firstLoadScene;
        //���ص�ǰ����
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
        //�����ǰ���ڽ�����������Ұ����������κβ���
        if (isLoading)
            return;
        isLoading = true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        //����Э��
        if (currentLoadScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
    }

    //Э�̼����Ƿ��뽥����ϲ���ж��ԭ�г��������³���
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //TODO ʵ�ֽ��뽥�� ���
            fadeCanvasEventSO.FadeIn(fadeDuration);
        }

        yield return new WaitForSeconds(fadeDuration);
        //ж�س���,Unity�Դ�����
        yield return currentLoadScene.sceneReference.UnLoadScene();

        playerTrans.gameObject.SetActive(false);

        //�����³���
        LoadNewScene();

    }

    //�����³�������
    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }
    /// <summary>
    /// ����������ɺ�
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadScene = sceneToLoad;//��ǰ�����л�Ϊ�������Ǹ�����
        //�ƶ���������ٽ�player����
        playerTrans.position = positionToGo;
        if(!(currentLoadScene.sceneType==SceneType.Menu))
            playerTrans.gameObject.SetActive(true);


        if (fadeScreen)//���뽥��Ч�� ��͸��
        {
            //TODO
            fadeCanvasEventSO.FadeOut(fadeDuration);
        }

        isLoading = false;

        //����������ɺ��¼�
        AfterSceneLoadedEventSO?.RaiseEvent();
    }
}
