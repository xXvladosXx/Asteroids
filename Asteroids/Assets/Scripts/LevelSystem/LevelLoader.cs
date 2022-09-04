using System;
using System.Threading.Tasks;
using AsteroidsZenject;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace LevelSystem
{
    public class LevelLoader : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public async void LoadScene(int index)
        {
            var scene = SceneManager.LoadSceneAsync(index);
            scene.allowSceneActivation = false;
            
            do
            {
                await Task.Delay(1);
            } while (scene.progress < .9f);

            scene.allowSceneActivation = true;
        }
    }
}