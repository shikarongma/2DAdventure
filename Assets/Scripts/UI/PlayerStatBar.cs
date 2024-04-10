using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;//��ɫѪ��
    public Image healthDelayIamge;//��ɫ�ӳ�Ѫ��
    public Image powerUmage;//������

    private void Update()
    {
        if (healthDelayIamge.fillAmount > healthImage.fillAmount)
        {
            healthDelayIamge.fillAmount -= Time.deltaTime;
        }
    }

    /// <summary>
    /// ����Health�ı���ٷֱ�
    /// </summary>
    /// <param name="persentage"></param>�ٷֱȣ�Current/Max
    public void OnHealthChange(float persentage)//�ٷֱ�
    {
        healthImage.fillAmount = persentage;
    }
}
