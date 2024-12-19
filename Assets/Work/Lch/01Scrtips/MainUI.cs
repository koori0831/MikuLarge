using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private TextListSO _list;
    private int Count = 0;
    public bool isTextTrigger = false;

    private void Start()
    {
        _image.gameObject.SetActive(false);
    }

    public void ShowBox()
    {
        if (!isTextTrigger) 
        {
            _image.gameObject.SetActive(true);
            StartCoroutine(TextShow());
        }
    }

    private IEnumerator TextShow()
    {
        isTextTrigger = true; 
        _tmp.text = "";

        string currentText = _list._textList[Count].Text;

        if(currentText.Length > 20)
        {
            _tmp.alignment = TextAlignmentOptions.Midline;
        }

        for (int i = 0; i < currentText.Length; i++)
        {
         
                _tmp.text += currentText[i];
            yield return new WaitForSeconds(0.02f); 
        }
    }

    public void CloseShow()
    {
        _image.gameObject.SetActive(false);
        Count++;
        isTextTrigger = false;
    }
}