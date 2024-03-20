using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private float delayBeforeDialogue = 3f; 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            StartCoroutine(StartDialogueWithDelay());
        }
    }

    private IEnumerator StartDialogueWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeDialogue); 

        if (dialogue != null)
        {
            dialogue.StartDialogue(); 
            gameObject.SetActive(false);
        }
    }
}
