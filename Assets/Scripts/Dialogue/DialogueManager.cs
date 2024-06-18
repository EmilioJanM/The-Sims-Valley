using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _dialogueTextMesh;
    [SerializeField] float _timeToAdd;

    string text = "ustry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five c";
    int index = 0;

    void Start()
    {
        StartDialogue();
    }

    void Update()
    {
        
    }

    void StartDialogue()
    {
        _dialogueTextMesh.text = "";
        StartCoroutine(AddCharsToDialogue());
    }

    IEnumerator AddCharsToDialogue()
    {
        foreach (char x in text)
        {
            yield return new WaitForSeconds(_timeToAdd);

            _dialogueTextMesh.text += text[index];
            index++;
        }        
    }
}
