using System;
namespace Blaash.Gaming.Service.GamePlay
{
    public class GameLevelCompleteRequest
    {
        public int gameSessionID { get; set; }

        public int currentLevel { get; set; }

        public int currentScore { get; set; }
    }
}
