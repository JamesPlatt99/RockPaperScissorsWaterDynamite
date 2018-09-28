using BotInterface.Bot;
using BotInterface.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperCoolBotWhatIDidMake
{
    interface IStrat
    {
        Move GetMove(Gamestate gamestate);
        int RoundIndex(Gamestate gamestate);
        bool DynamiteAvailable(Gamestate gamestate);
        bool OpponentHasDynamiteAvailable(Gamestate gamestate);


    }
}
