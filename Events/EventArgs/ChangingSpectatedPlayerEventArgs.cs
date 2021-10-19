// -----------------------------------------------------------------------
// <copyright file="ChangingSpectatedPlayerEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features;

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class ChangingSpectatedPlayerEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangingSpectatedPlayerEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public ChangingSpectatedPlayerEventArgs(Player spectator, Player oldPlayer, Player newPlayer)
        {
            this.Spectator = spectator;
            this.OldPlayer = oldPlayer;
            this.NewPlayer = newPlayer;
        }

        /// <summary>
        /// Gets player that is changing spectated player.
        /// </summary>
        public Player Spectator { get; }

        /// <summary>
        /// Gets player that was spectated.
        /// </summary>
        public Player OldPlayer { get; }

        /// <summary>
        /// Gets player that is spectated.
        /// </summary>
        public Player NewPlayer { get; }
    }
}