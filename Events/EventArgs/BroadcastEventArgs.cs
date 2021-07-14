// -----------------------------------------------------------------------
// <copyright file="BroadcastEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class BroadcastEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BroadcastEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public BroadcastEventArgs(Broadcast.BroadcastFlags type, string content, string adminName, string[] targets)
        {
            this.Type = type;
            this.Content = content;
            this.AdminName = adminName;
            this.Targets = targets;
        }

        /// <summary>
        /// Gets broadcast type.
        /// </summary>
        public Broadcast.BroadcastFlags Type { get; }

        /// <summary>
        /// Gets broadcast content.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets admin Name.
        /// </summary>
        public string AdminName { get; }

        /// <summary>
        /// Gets broadcast Targets.
        /// </summary>
        public string[] Targets { get; }
    }
}