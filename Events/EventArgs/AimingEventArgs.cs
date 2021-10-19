// -----------------------------------------------------------------------
// <copyright file="AimingEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features.Items;

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class AimingEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AimingEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public AimingEventArgs(Exiled.API.Features.Player player, bool aimingIn)
        {
            this.Player = player;
            this.Firearm = player.CurrentItem as Exiled.API.Features.Items.Firearm;
            this.AimingIn = aimingIn;
        }

        /// <summary>
        /// Gets player that unloades firearm.
        /// </summary>
        public Exiled.API.Features.Player Player { get; }

        /// <summary>
        /// Gets firearm to aim in or aim out.
        /// </summary>
        public Firearm Firearm { get; }

        /// <summary>
        /// Gets a value indicating whether player is aiming in or out.
        /// </summary>
        public bool AimingIn { get; }
    }
}