// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Mistaken.Updater.Config;

namespace Mistaken.Events
{
    /// <inheritdoc/>
    public class Config : IAutoUpdatableConfig
    {
        public bool VerbouseOutput { get; set; }

        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc/>
        public AutoUpdateConfig AutoUpdateConfig { get; set; }
    }
}
