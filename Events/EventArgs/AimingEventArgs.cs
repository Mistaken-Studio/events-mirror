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
        public AimingEventArgs(Exiled.API.Features.Player player, Firearm firearm, bool aiming)
        {
            this.Player = player;
            this.Firearm = firearm;
            this.Aiming = aiming;
            this.IsAllowed = true;
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
        /// Gets a value indicating whether player is aiming or not.
        /// </summary>
        public bool Aiming { get; }

        /// <summary>
        /// Gets or sets a value indicating whether if unloading is allowed.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}