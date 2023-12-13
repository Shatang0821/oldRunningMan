using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    [SerializeField] UnityEngine.UI.Image transitionImage;
    [SerializeField] float fadeTime = 3.5f;

    Color color;

    const string GAMEPLAY = "Gameplay";
    const string MAIN_MENU = "MainMenu";
    const string SCORING = "Scoring";

    void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadingCoroutine(string sceneName)
    {
        //�V�[���̃��[�h���������Ă��邩�`�F�b�N
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;  //���[�h�����̃V�[���̃A�N�e�B�u��Ԑ؂�ւ�

        transitionImage.gameObject.SetActive(true);

        //Fade out
        while (color.a < 1f)
        {
            //fade�^�C���ɂ���Ď����I�ɑ����Z����Clamp01 0�`1�Ԃɐ����ł���
            color.a = Mathf.Clamp01( color.a += Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = color;

            yield return null;
        }

        //�V�[���̃��[�h�����S�ɏI���܂ő҂�
        yield return new WaitUntil(() => loadingOperation.progress >= 0.9f);

        loadingOperation.allowSceneActivation = true;

        //Fade in
        while (color.a > 0f)
        {
            //fade�^�C���ɂ���Ď����I�ɑ����Z����Clamp01 0�`1�Ԃɐ����ł���
            color.a = Mathf.Clamp01(color.a -= Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = color;

            yield return null;
        }
        transitionImage.gameObject.SetActive(false);
    }

    public void LoadGamePlayScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(GAMEPLAY));
    }

    public void LoadMainMenuScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(MAIN_MENU));
    }

    public void LoadScoringScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(SCORING));
    }
}
