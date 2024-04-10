using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;//绿色血量
    public Image healthDelayIamge;//红色延迟血量
    public Image powerUmage;//能量条

    private void Update()
    {
        if (healthDelayIamge.fillAmount > healthImage.fillAmount)
        {
            healthDelayIamge.fillAmount -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 接受Health的变更百分比
    /// </summary>
    /// <param name="persentage"></param>百分比：Current/Max
    public void OnHealthChange(float persentage)//百分比
    {
        healthImage.fillAmount = persentage;
    }
}
