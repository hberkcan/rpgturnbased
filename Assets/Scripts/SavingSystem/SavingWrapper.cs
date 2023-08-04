using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SavingSystem))]
public class SavingWrapper : MonoBehaviour
{
    private SavingSystem savingSystem;
    private const string currentSaveKey = "currentSaveName";
    [SerializeField] int firstLevelBuildIndex = 1;
    [SerializeField] int menuLevelBuildIndex = 0;

    private void Awake()
    {
        savingSystem = GetComponent<SavingSystem>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
    }

    private void OnApplicationQuit()
    {
        //Save();
    }

    private void SceneManager_sceneLoaded(Scene scn, LoadSceneMode mode)
    {
        if (scn.buildIndex == 0)
        {
            LoadGame(currentSaveKey);
            return;
        }

        Load();
    }

    private void SceneManager_sceneUnloaded(Scene scn)
    {
        //Save();
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
        yield return savingSystem.LoadLastScene(GetCurrentSave());
    }

    private IEnumerator LoadFirstScene()
    {
        yield return SceneManager.LoadSceneAsync(firstLevelBuildIndex);
    }

    private IEnumerator LoadMenuScene()
    {
        yield return SceneManager.LoadSceneAsync(menuLevelBuildIndex);
    }

    public void Load()
    {
        savingSystem.Load(GetCurrentSave());
    }

    public void Save()
    {
        savingSystem.Save(GetCurrentSave());
    }

    public void Delete()
    {
        savingSystem.Delete(GetCurrentSave());
    }

    public IEnumerable<string> ListSaves()
    {
        return savingSystem.ListSaves();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        Save();
    //    }
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        Load();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Delete))
    //    {
    //        Delete();
    //    }
    //}
}
