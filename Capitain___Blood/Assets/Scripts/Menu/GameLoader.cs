using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace RetroJam.CaptainBlood
{
    public class GameLoader : MonoBehaviour
    {   
        [SerializeField] private TextMeshProUGUI loadingField;
        public void LoadGame(int _sceneIndex)
        {
            StartCoroutine(LoadAsynchronously(_sceneIndex));
            
        }

        IEnumerator LoadAsynchronously (int _sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneIndex);

            while (!operation.isDone)
            {
                int progress = Mathf.FloorToInt(Mathf.Clamp01(operation.progress/.9f)*100);

                loadingField.text =  progress.ToString();

                yield return null;
            }
        }
        
    }
}