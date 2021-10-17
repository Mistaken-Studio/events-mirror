// -----------------------------------------------------------------------
// <copyright file="ChangingAttachmentsEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features.Items;

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class ChangingAttachmentsEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangingAttachmentsEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public ChangingAttachmentsEventArgs(Exiled.API.Features.Player player, Firearm firearm, uint newAttachmentsCode)
        {
            this.Player = player;
            this.Firearm = firearm;
            this.NewAttachmentsCode = newAttachmentsCode;
            this.IsAllowed = true;
        }

        /// <summary>
        /// Gets player that changes attachments.
        /// </summary>
        public Exiled.API.Features.Player Player { get; }

        /// <summary>
        /// Gets firearm to change.
        /// </summary>
        public Firearm Firearm { get; }

        /// <summary>
        /// Gets or sets new attachments code.
        /// </summary>
        public uint NewAttachmentsCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if changing attachments is allowed.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}