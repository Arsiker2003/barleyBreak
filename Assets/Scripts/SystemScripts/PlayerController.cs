using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject qObject;

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SeeQ()
    {
        qObject.SetActive(true);
    }
    public void HideQ()
    {
        qObject.SetActive(false);
    }

    public void GoByUrl(string url)
    {
        Application.OpenURL(url);
    }

    [SerializeField]
    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
}
