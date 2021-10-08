// -----------------------------------------------------------------------
// <copyright file="PickItemRequestEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features.Items;

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class PickItemRequestEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PickItemRequestEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public PickItemRequestEventArgs(Exiled.API.Features.Player player, Pickup pickup)
        {
            this.Player = player;
            this.Pickup = pickup;
            this.IsAllowed = true;
        }

        /// <summary>
        /// Gets player that pickups.
        /// </summary>
        public Exiled.API.Features.Player Player { get; }

        /// <summary>
        /// Gets pickup.
        /// </summary>
        public Pickup Pickup { get; }

        /// <summary>
        /// Gets or sets a value indicating whether if picking is allowed.
        /// </summary>
        public bool IsAllowed { get; set; } = true;
    }
}