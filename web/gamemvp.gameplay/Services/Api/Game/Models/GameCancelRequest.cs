using System;
namespace Blaash.Gaming.Service.GamePlay.Services.Api.Game.Models
{
    /// <summary>
    /// This will be called when Player Clicks on the Close Button
    /// </summary>
    public class GameCancelRequest
    {
        public int gameSessionID { get; set; }

        public int currentLevel { get; set; }

        public int currentScore { get; set; }
    }
}


