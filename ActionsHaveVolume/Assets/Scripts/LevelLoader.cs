using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public int[] levelsIndex;

    public Slider progressSlider;

    void Start()
    {
        StartCoroutine(LoadAcync());
    }

    IEnumerator LoadAcync()
    {
        Debug.Log("Loading Scene");
        Debug.Log(PlayerPrefs.GetInt("Level", 0));
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelsIndex[PlayerPrefs.GetInt("Level", 0)]);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressSlider.value = progress;

            yield return null;
        }
    }
}
