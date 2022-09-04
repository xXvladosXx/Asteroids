using System;
using UnityEngine;
using Utilities;

namespace Saving
{
    public class SaveSystem
    {
        public float CurrentScore { get; private set; }

        private const string BEST_SCORE = "BestScore";
        private const string HAS_BEST_SCORE = "HasBestScore";
        private const string EFFECTS_VOLUME = "EffectsVolume";
        private const string MUSIC_VOLUME = "MusicVolume";
        private const string IS_FULLSCREEN = "Fullscreen";
        private const string LANGUAGE = "Language";

        public event Action OnBestScoreChanged;
        
        public void TryToRegisterBestResult(SaveData saveData)
        {
            CurrentScore = saveData.Score;

            if (PlayerPrefs.GetInt(HAS_BEST_SCORE, 0) == 0)
            {
                PlayerPrefs.SetFloat(BEST_SCORE, CurrentScore);
                PlayerPrefs.SetInt(HAS_BEST_SCORE, 1);
                OnBestScoreChanged?.Invoke();
                
                return;
            }

            if (PlayerPrefs.GetFloat(BEST_SCORE) > saveData.Score)
            {
                PlayerPrefs.SetFloat(BEST_SCORE, CurrentScore);
                OnBestScoreChanged?.Invoke();
            }
        }

        public string GetBestScore()
        {
            if (PlayerPrefs.GetInt(HAS_BEST_SCORE, 0) == 0)
                return "Whoops!\n You don`t have a result.";

            return TextFormatter.FormatToTwoDecimalAfterPoint(PlayerPrefs.GetFloat(BEST_SCORE));
        }

        public void SetLanguage(int id)
        {
            PlayerPrefs.SetInt(LANGUAGE, id);
        }
        
        public void SaveSettings(SaveData settingsSaveData)
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME, settingsSaveData.MusicVolume);
            PlayerPrefs.SetFloat(EFFECTS_VOLUME, settingsSaveData.EffectsVolume);
            PlayerPrefs.SetInt(IS_FULLSCREEN, Convert.ToInt32(settingsSaveData.Fullscreen));
        }
        
        public SaveData LoadSettings()
        {
            var settingsSaveData = new SaveData
            {
                EffectsVolume = PlayerPrefs.GetFloat(EFFECTS_VOLUME, 0),
                MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0),
                Fullscreen = Convert.ToBoolean(PlayerPrefs.GetInt(IS_FULLSCREEN, 0)),
                Language = PlayerPrefs.GetInt(LANGUAGE, 0),
            };
            
            return settingsSaveData;
        }
    }
}