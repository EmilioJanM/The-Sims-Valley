using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class DialogueInk : MonoBehaviour
{
    public static DialogueInk instance;

    [SerializeField] GameObject _dialoguePanel;
    [SerializeField] TextMeshProUGUI _dialogueTextMesh;
    [SerializeField] GameObject _nextButton;
    [SerializeField] float _timeToAdd;
    private int index = 0;

    private Story _currentStory;
    private bool _dialoguePlaying = false;


    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Debug.Log("There is already an instance of dialogue ink");
            Destroy(gameObject);
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dialoguePlaying)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }

    }

    public void EnterDialogue(TextAsset ink)
    {
        _currentStory = new Story(ink.text);
        _dialoguePanel.SetActive(true);
        _dialoguePlaying = true;

        ContinueStory();
    }

    public void ContinueStory()
    {
        index = 0;
        _nextButton.SetActive(false);

        if (_currentStory.canContinue)
        {
            _dialogueTextMesh.text = "";

            StartCoroutine(AddCharsToDialogue(_currentStory.Continue()));
        }
        else
        {
            ExitDialogue();
        }
    }

    public void ExitDialogue()
    {
        _dialoguePlaying = false;
        _dialoguePanel.SetActive(false);
        _dialogueTextMesh.text = "";
    }

    IEnumerator AddCharsToDialogue(string text)
    {
        foreach (char x in text)
        {
            yield return new WaitForSeconds(_timeToAdd);

            _dialogueTextMesh.text += text[index];
            index++;
        }
        if(index == text.Length)
        {
            _nextButton.SetActive(true);
        }
    }
}
