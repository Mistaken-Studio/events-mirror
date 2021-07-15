// -----------------------------------------------------------------------
// <copyright file="PluginHandler.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Exiled.API.Enums;
using Mistaken.API;

namespace Mistaken.Events
{
    /// <inheritdoc/>
    public class PluginHandler : AutoUpdatablePlugin<Config>
    {
        /// <inheritdoc/>
        public override string Author => "Mistaken Devs";

        /// <inheritdoc/>
        public override string Name => "Mistaken Events";

        /// <inheritdoc/>
        public override string Prefix => "MEVENTS";

        /// <inheritdoc/>
        public override PluginPriority Priority => PluginPriority.High;

        /// <inheritdoc/>
        public override Version RequiredExiledVersion => new Version(2, 11, 0);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            this.harmony = new HarmonyLib.Harmony("com.mistaken.events");
            this.harmony.PatchAll();

            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            this.harmony.UnpatchAll();

            base.OnDisabled();
        }

        private HarmonyLib.Harmony harmony;
    }
}
