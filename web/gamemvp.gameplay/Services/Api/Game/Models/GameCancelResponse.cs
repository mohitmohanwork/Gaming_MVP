using System;
namespace Blaash.Gaming.Service.GamePlay
{
    public class GameCancelResponse
    {
        public int gameSessionID { get; set; }

        public bool isCancelled { get; set; }
    }
}
