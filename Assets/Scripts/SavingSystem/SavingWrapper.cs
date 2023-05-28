using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingWrapper : MonoBehaviour
{
    private const string currentSaveKey = "currentSaveName";
    [SerializeField] int firstLevelBuildIndex = 1;
    [SerializeField] int menuLevelBuildIndex = 0;

    private void Awake()
    {
        Load();
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadLastScene());
    }

    public void NewGame(string saveFile)
    {
        SetCurrentSave(saveFile);
        StartCoroutine(LoadFirstScene());
    }

    public void LoadGame(string saveFile)
    {
        SetCurrentSave(saveFile);
        ContinueGame();
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadMenuScene());
    }

    private void SetCurrentSave(string saveFile)
    {
        PlayerPrefs.SetString(currentSaveKey, saveFile);
    }

    private string GetCurrentSave()
    {
        return PlayerPrefs.GetString(currentSaveKey);
    }

    private IEnumerator LoadLastScene()
    {
        yield return GetComponent<SavingSystem>().LoadLastScene(GetCurrentSave());
    }

    private IEnumerator LoadFirstScene()
    {
        yield return SceneManager.LoadSceneAsync(firstLevelBuildIndex);
    }

    private IEnumerator LoadMenuScene()
    {
        yield return SceneManager.LoadSceneAsync(menuLevelBuildIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Delete();
        }
    }

    public void Load()
    {
        GetComponent<SavingSystem>().Load(GetCurrentSave());
    }

    public void Save()
    {
        GetComponent<SavingSystem>().Save(GetCurrentSave());
    }

    public void Delete()
    {
        GetComponent<SavingSystem>().Delete(GetCurrentSave());
    }

    public IEnumerable<string> ListSaves()
    {
        return GetComponent<SavingSystem>().ListSaves();
    }
}
