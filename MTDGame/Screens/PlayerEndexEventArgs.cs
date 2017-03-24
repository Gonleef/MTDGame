﻿using System;
using Microsoft.Xna.Framework;

namespace MG
{
    /// <summary>
    /// Custom event argument which includes the index of the player who
    /// triggered the event. This is used by the MenuEntry.Selected event.
    /// </summary>
    class PlayerIndexEventArgs : EventArgs
    {
        public PlayerIndexEventArgs(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
        }

        public PlayerIndex PlayerIndex
        {
            get { return playerIndex; }
        }

        PlayerIndex playerIndex;
    }
}