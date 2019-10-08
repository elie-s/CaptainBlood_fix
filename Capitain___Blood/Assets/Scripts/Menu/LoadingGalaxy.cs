using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RetroJam.CaptainBlood.GalaxyLib;
using RetroJam.CaptainBlood.Lang;
using TMPro;

namespace RetroJam.CaptainBlood
{
    
    public class LoadingGalaxy : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        bool startGame;
        // Start is called before the first frame update
        void Start()
        {
            Words.InitializeWords();
            Galaxy.GeneratePlanets();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = Galaxy.planets.Count.ToString();

            if(Galaxy.planets.Count == 32256 && !startGame)
            {
                startGame = true;
                Galaxy.Initialize();
                LoadGame(2);
            }

            //if(Input.GetKeyDown(KeyCode.Space)) LoadGame(2);
        }

        public void LoadGame(int _sceneIndex)
        {
            StartCoroutine(LoadAsynchronously(_sceneIndex));
            
        }

        IEnumerator LoadAsynchronously (int _sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneIndex, LoadSceneMode.Additive);

            operation.allowSceneActivation = true;

            while (!operation.isDone)
            {
                Debug.Log(operation.progress);

                yield return null;
            }
        }
    }
}