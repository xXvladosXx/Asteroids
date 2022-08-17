using System;

namespace Saving
{
    [Serializable]
    public class SaveData
    {
        public float Score { get; set; }
        public float EffectsVolume { get; set; }
        public float MusicVolume { get; set; }
        public bool Fullscreen { get; set; }
    }
}