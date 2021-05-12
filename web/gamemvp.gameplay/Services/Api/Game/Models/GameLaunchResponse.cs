using System;
namespace Blaash.Gaming.Service.GamePlay
{
    public class GameLaunchResponse
    {
        public int gameSessionID { get; set; }

        public int currentLevel { get; set; }

        public int currentScore { get; set; }

        public int nextAwardLevel { get; set; }

    }
}
