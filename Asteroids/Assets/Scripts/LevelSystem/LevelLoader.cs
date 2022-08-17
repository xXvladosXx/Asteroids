using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelSystem
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _loader;
        [SerializeField] private Image _progressBar;
        
        public static LevelLoader Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public async void LoadScene(int index)
        {
            var scene = SceneManager.LoadSceneAsync(index);
            scene.allowSceneActivation = false;
            
            _loader.SetActive(true);

            do
            {
                await Task.Delay(1);
                _progressBar.fillAmount = scene.progress;
            } while (scene.progress < .9f);

            scene.allowSceneActivation = true;
            _loader.SetActive(false);
        }
    }
}