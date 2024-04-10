using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public PlayerInputControl inputControl;

    [Header("UI组件")]
    public GameObject TalkUI;
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;

    //第几行
    public int index;

    [Header("头像")]
    public Sprite face01, face02;

    //判断一行是否输出完毕
    public bool textFinished;
    //取消打字
    bool cancelTyping;
    //每个字输出速度
    public float textSpeed;
    readonly List<string> textList = new();
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        GetTextFormFile(textFile);
        index = 0;
    }
    private void Update()
    {
        inputControl.Gameplay.TalkUI.started += RoundOutput;
    }

    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        /*协程*/
        textFinished = true;
        StartCoroutine(SetTextUI());

        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void RoundOutput(InputAction.CallbackContext obj)
    {
        if (index == textList.Count)
        {
            TalkUI.SetActive(false);
            index = 0;
            return;
        }
        //if(textFinished)
        //{
        //    //textLabel.text = textList[index];
        //    //index++;
        //    /*协程*/
        //    StartCoroutine(SetTextUI());
        //}
        if (textFinished && !cancelTyping)
        {
            StartCoroutine(SetTextUI());
        }
        else if (!textFinished && !cancelTyping)
            cancelTyping = true;

    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        switch (textList[index])
        {
            case "A\r":
                faceImage.sprite = face01;
                index++;
                break;
            case "B\r":
                faceImage.sprite = face02;
                index++;
                break;
            default:
                break;
        }

        //for(int i = 0; i < textList[index].Length; i++)
        //{
        //    textLabel.text += textList[index][i];

        //    yield return new WaitForSeconds(textSpeed);
        //}

        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length -1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();

        var lineDate = file.text.Split('\n');
        foreach(var line in lineDate)
        {
            textList.Add(line);
        }
    }
}
