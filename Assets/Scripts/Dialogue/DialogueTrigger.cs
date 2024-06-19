using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject _pressKey;

    [SerializeField] TextAsset jsonInk;
    private bool inTrigger;

    void Start()
    {
        
    }

    void Update()
    {
        if (inTrigger && _pressKey.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DialogueInk.instance.EnterDialogue(jsonInk);
                _pressKey.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _pressKey.SetActive(true);
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _pressKey.SetActive(false);
            DialogueInk.instance.ExitDialogue();
            inTrigger = false;
        }
    }
}