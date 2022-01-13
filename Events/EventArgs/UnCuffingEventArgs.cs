// -----------------------------------------------------------------------
// <copyright file="UnCuffingEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class UncuffingEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UncuffingEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public UncuffingEventArgs(Exiled.API.Features.Player target, Exiled.API.Features.Player unCuffer, bool isAllowed = true)
        {
            this.Target = target;
            this.UnCuffer = unCuffer;
            this.IsAllowed = isAllowed;
        }

        /// <summary>
        /// Gets player that is being uncuffed.
        /// </summary>
        public Exiled.API.Features.Player Target { get; }

        /// <summary>
        /// Gets player that is un cuffing <see cref="Target"/>.
        /// </summary>
        public Exiled.API.Features.Player UnCuffer { get; }

        /// <summary>
        /// Gets or sets a value indicating whether if uncuffing is allowed.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}