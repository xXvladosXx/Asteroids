using System;
using AsteroidsZenject.AsteroidZenject;
using AudioSystem;
using Camera;
using Combat.Core;
using Data.Camera;
using Entities;
using Saving;
using Spawners;
using UI.Core;
using UI.GameOver;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Games
{
    public sealed class GameGameplay : MonoBehaviour
    {
        [field: SerializeField] public CameraShakerData CameraShakerData { get; private set; }
    }
}