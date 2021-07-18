// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using Mistaken.Updater.Config;

namespace Mistaken.Events
{
    /// <inheritdoc/>
    public class Config : IAutoUpdatableConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether debugs should be displayed.
        /// </summary>
        [Description("If true then debugs will be displayed")]
        public bool VerbouseOutput { get; set; }

        /// <inheritdoc/>
        public AutoUpdateConfig AutoUpdateConfig { get; set; }
    }
}
