using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private string key; // ìYâ¡èdê∂ì_?å^éöíi
    public Canvas tutorialCanvas;

    private void Start()
    {
        tutorialCanvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RespawnPointManager.Instance.UpdateRespawnPoint(key);
            tutorialCanvas.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialCanvas.enabled = false;
        }
    }
}
