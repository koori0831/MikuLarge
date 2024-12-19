using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextTrigger : MonoBehaviour
{
	private MainUI _textBoxOn;
    private BoxCollider2D _boxCollider;
    public UnityEvent OnShowText;
    public UnityEvent OnCloseText;

    private void Awake()
    {
        _textBoxOn = FindAnyObjectByType<MainUI>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Animal"))
        OnShowText?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Animal"))
        {
            _boxCollider.gameObject.SetActive(false);
            OnCloseText?.Invoke();
        }
    }
}
