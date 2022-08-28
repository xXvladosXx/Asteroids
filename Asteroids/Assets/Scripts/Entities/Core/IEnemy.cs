using System;
using EnemiesZenject;

namespace Entities.Core
{
    public interface IEnemy
    {
        PlayerEntity PlayerEntity { get; set; }
    }
}