using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class FadeScreenEffect : MonoBehaviour
{
    [SerializeField] Canvas gameOver;
    [SerializeField] Image fadeImage;

    float deathTargetAlpha;
    float deathStartAlpha;

    float respownTargetAlpha;
    float respownStartAlpha;

    [SerializeField] float duration;

    

    private void Awake()
    {
        deathTargetAlpha = 1f;
        respownTargetAlpha = 0f;
        gameOver.enabled = false;
        deathStartAlpha = 0f;
        respownStartAlpha = 1f;
    }

    private void OnEnable()
    {
        GameManager.onGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.onGameOver -= OnGameOver;
    }

    void OnGameOver()
    {
        gameOver.enabled = true;
        StartCoroutine(nameof(DeathEffectRoutine));
    }

    IEnumerator DeathEffectRoutine()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(deathStartAlpha, deathTargetAlpha, t));
            fadeImage.color = newColor;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, deathTargetAlpha);

        StartCoroutine(nameof(RespownEffectRoutine));
    }

    IEnumerator RespownEffectRoutine()
    {
        GameManager.GameState = GameState.Respown;

        GameManager.onRespown?.Invoke();

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(respownStartAlpha, respownTargetAlpha, t));
            fadeImage.color = newColor;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, respownTargetAlpha);
    }
}
