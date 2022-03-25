// -----------------------------------------------------------------------
// <copyright file="PluginHandler.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using Exiled.API.Enums;
using Exiled.API.Features;
using Mistaken.Events.Patches;

namespace Mistaken.Events
{
    /// <inheritdoc/>
    internal class PluginHandler : Plugin<Config>
    {
        /// <inheritdoc/>
        public override string Author => "Mistaken Devs";

        /// <inheritdoc/>
        public override string Name => "CustomEvents";

        /// <inheritdoc/>
        public override string Prefix => "MCEVENTS";

        /// <inheritdoc/>
        public override PluginPriority Priority => PluginPriority.High;

        /// <inheritdoc/>
        public override Version RequiredExiledVersion => new Version(5, 0, 0);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            this.harmony = new HarmonyLib.Harmony("com.mistaken.events");
            this.harmony.Patch(
                typeof(Door).GetMethod("RegisterDoorTypesOnLevelLoad", BindingFlags.NonPublic | BindingFlags.Static),
                postfix: new HarmonyLib.HarmonyMethod(typeof(RegisterDoorTypesOnLevelLoadPatch), nameof(RegisterDoorTypesOnLevelLoadPatch.Postfix)));
            this.harmony.PatchAll();

            new EventsHandler(this);

            API.Diagnostics.Module.OnEnable(this);

            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            this.harmony.UnpatchAll();

            API.Diagnostics.Module.OnDisable(this);

            base.OnDisabled();
        }

        private HarmonyLib.Harmony harmony;
    }
}
