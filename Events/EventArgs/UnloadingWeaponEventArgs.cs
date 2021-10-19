// -----------------------------------------------------------------------
// <copyright file="UnloadingWeaponEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features.Items;

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class UnloadingWeaponEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnloadingWeaponEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public UnloadingWeaponEventArgs(Exiled.API.Features.Player player, bool isAllowed = true)
        {
            this.Player = player;
            this.Firearm = player.CurrentItem as Firearm;
            this.IsAllowed = isAllowed;
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