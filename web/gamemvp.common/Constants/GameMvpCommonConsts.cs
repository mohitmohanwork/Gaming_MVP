using System;
using System.Collections.Generic;
using System.Text;

namespace Blaash.Gaming.Service.Common
{
    
    public static class GameMvpCommonConsts
    {
        public static class CommonKeys
        {
            public const string EVENT_NAME = "event_name";
            public const string IS_PROCESSED = "is_processed";
            public const string USER_ID = "user_id";
            public const string FIRST_NAME = "first_name";
            public const string LAST_NAME = "last_name";
            public const string MIDDLE_NAME = "middle_name";
            public const string USER_TYPE = "app_token";
            public const string PLAYER_USER_ROLE = "mvp_player";
        }
       
        public static class Collections
        {
            public const string EVENT = "event_track";
            public const string TENANT = "tenant";
            public const string TENANT_CLIENT = "tenant_client";
            public const string GAME = "game";
            public const string TENANT_GAMES = "tenant_games";

            public const string JOURNEY = "journey";
            public const string JOURNEY_DETAILS = "journey_details";
            public const string REWARD = "reward";
            public const string ENGAGEMENT = "engagement";
            public const string ENGAGEMENT_JOURNEY = "engagement_journey";

        }
        public static class EventTypes
        {
            
            public const string PLAYER_LOGIN = "player_login";
            public const string VIEW_PRODUCT = "view_product";
        }

    }
}
