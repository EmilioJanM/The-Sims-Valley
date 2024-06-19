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
    public static bool _dialoguePlaying = false;
    private bool _dialogueFinished = false;


    void Start()
    {
        if(instance != null)
        {
            Debug.Log("There is already an instance of dialogue ink");
            Destroy(gameObject);
        }
        instance = this;
    }

    void Update()
    {
        if (!_dialoguePlaying)
            return;

        if (Input.GetKeyDown(KeyCode.E) && _dialogueFinished)
        {
            ContinueStory();
        }
    }

    /// <summary>
    /// Enters the dialogue, activated with triggers
    /// </summary>
    /// <param name="ink"></param>
    public void EnterDialogue(TextAsset ink)
    {
        StoreManager.inStore = true;
        _nextButton.GetComponent<TextMeshProUGUI>().text = "Press E to continue";

        _currentStory = new Story(ink.text);
        _dialoguePanel.SetActive(true);
        _dialoguePlaying = true;

        ContinueStory();
    }

    /// <summary>
    /// Continues through the lines of the dialogue
    /// </summary>
    public void ContinueStory()
    {
        index = 0;
        _nextButton.SetActive(false);
        _dialogueFinished = false;

        if (_currentStory.canContinue)
        {
            _dialogueTextMesh.text = "";

            StartCoroutine(AddCharsToDialogue(_currentStory.Continue()));
        }
        else
        {
            StoreManager.instance.ExitAllScreens();

            ExitDialogue();
        }
    }

    public void ExitDialogue()
    {
        _dialoguePlaying = false;
        _dialoguePanel.SetActive(false);
        _dialogueTextMesh.text = "";
        _dialogueFinished = false;
        StopAllCoroutines();
    }

    /// <summary>
    /// "Animates" the dialogue adding one letter at a time
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
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
            if (!_currentStory.canContinue)
            {
                _nextButton.GetComponent<TextMeshProUGUI>().text = "Press E to exit";
                StoreManager.instance.ActivateOptionsScreen();
            }
            _nextButton.SetActive(true);
            _dialogueFinished = true;
        }
    }
}
