// -----------------------------------------------------------------------
// <copyright file="FirstTimeJoinedEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features;

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class FirstTimeJoinedEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstTimeJoinedEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public FirstTimeJoinedEventArgs(Player player)
        {
            this.Player = player;
        }

        /// <summary>
        /// Gets player that joins.
        /// </summary>
        public Player Player { get; }
    }
}