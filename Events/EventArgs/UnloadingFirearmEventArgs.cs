// -----------------------------------------------------------------------
// <copyright file="UnloadingFirearmEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features.Items;

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class UnloadingFirearmEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnloadingFirearmEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public UnloadingFirearmEventArgs(Exiled.API.Features.Player player, Firearm firearm)
        {
            this.Player = player;
            this.Firearm = firearm;
            this.IsAllowed = true;
        }

        /// <summary>
        /// Gets player that unloades firearm.
        /// </summary>
        public Exiled.API.Features.Player Player { get; }

        /// <summary>
        /// Gets firearm to unload.
        /// </summary>
        public Firearm Firearm { get; }

        /// <summary>
        /// Gets or sets a value indicating whether if unloading is allowed.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}