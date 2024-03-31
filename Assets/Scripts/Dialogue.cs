using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private List<CharacterData> characters;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image characterImage;
    [SerializeField] private Sprite[] characterSprites;
    [TextArea(3, 3)] [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private GameObject door;

    private int index;

    private void Awake()
    {
        text.text = string.Empty;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (text.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        gameObject.SetActive(true);
        index = 0;
        SetCharacterImage(index);
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            SetCharacterImage(index);
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            door.SetActive(false);
        }
    }
    private void SetCharacterImage(int index)
    {
        if (index >= 0 && index < lines.Length)
        {
            string[] parts = lines[index].Split(' ');
            string characterName = parts[0];

            foreach (CharacterData character in characters)
            {
                if (character.characterName == characterName)
                {
                    characterImage.sprite = character.characterSprite;
                    return;
                }
            }
        }
    }

}
