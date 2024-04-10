using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public GameSceneSO SceneToGo;//��һ������
    public Vector3 PositionToGo;//ȥ����һ��������λ������

    public SceneLoadEventSO loadEventSO;//�����¼�
    private AudioDefination audioDefination;//��������

    private void Awake()
    {
        audioDefination = GetComponent<AudioDefination>();
    }
    public void TiggerAction()
    {
        //�����¼�
        loadEventSO.RaiseLoadRequestEvent(SceneToGo, PositionToGo, true);
        audioDefination.PlayAudioClip();
    }

    public void OnExitGame()//����һ���˳���Ϸ�ķ���
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�������unity��������
#else
        Application.Quit();//�����ڴ���ļ���
#endif
    }
}
