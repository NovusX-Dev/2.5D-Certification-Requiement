using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] Image _loadingProgress;
    [SerializeField] GameObject _startGameText;

    void Start()
    {
        StartCoroutine(LoadingSceneRoutine());
    }

    IEnumerator LoadingSceneRoutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {

            _loadingProgress.fillAmount += Mathf.Lerp(0f, 1f, 0.35f * Time.deltaTime);

            if (_loadingProgress.fillAmount >= 1f)
            {
                if (operation.progress >= 0.9f)
                {
                    _startGameText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        yield return new WaitForSeconds(0.5f);
                        operation.allowSceneActivation = true;
                    }
                }
            }

            yield return null;
        }
    }
}
