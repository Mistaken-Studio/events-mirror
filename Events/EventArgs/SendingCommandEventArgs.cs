// -----------------------------------------------------------------------
// <copyright file="SendingCommandEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class SendingCommandEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendingCommandEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public SendingCommandEventArgs(Exiled.API.Features.Player admin, string query)
        {
            this.Admin = admin;
            this.Arguments = query.Split(' ');
            this.Command = this.Arguments[0];
            this.Arguments = this.Arguments.Skip(1).ToArray();
            this.IsAllowed = true;
        }

        /// <summary>
        /// Gets player that unloades firearm.
        /// </summary>
        public Exiled.API.Features.Player Admin { get; }

        /// <summary>
        /// Gets sent command.
        /// </summary>
        public string Command { get; }

        /// <summary>
        /// Gets sent command's arguments.
        /// </summary>
        public string[] Arguments { get; }

        /// <summary>
        /// Gets or sets a value indicating whether if unloading is allowed.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}