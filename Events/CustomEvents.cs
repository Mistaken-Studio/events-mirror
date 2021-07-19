// -----------------------------------------------------------------------
// <copyright file="CustomEvents.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Mistaken.Events.EventArgs;

namespace Mistaken.Events.Handlers
{
    /// <summary>
    /// Custom Events.
    /// </summary>
    public static class CustomEvents
    {
        /// <summary>
        /// Event called when requesting Item Pickup
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<PickItemRequestEventArgs> OnRequestPickItem;

        /// <summary>
        /// Event called when broadcast was sent
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<BroadcastEventArgs> OnBroadcast;

        /// <summary>
        /// Event called when player joins first time in row
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<FirstTimeJoinedEventArgs> OnFirstTimeJoined;

        /// <summary>
        /// Invokes <see cref="OnRequestPickItem"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeOnRequestPickItem(ref PickItemRequestEventArgs ev)
        {
            OnRequestPickItem?.Invoke(ev);
        }

        /// <summary>
        /// Invokes <see cref="OnBroadcast"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeOnBroadcast(ref BroadcastEventArgs ev)
        {
            OnBroadcast?.Invoke(ev);
        }

        /// <summary>
        /// Invokes <see cref="OnRequestPickItem"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeOnFirstTimeJoined(FirstTimeJoinedEventArgs ev)
        {
            OnFirstTimeJoined?.Invoke(ev);
        }

        /// <summary>
        /// Data about SCP079.
        /// </summary>
        public static class SCP079
        {
            /// <summary>
            /// Gets time left to end of recontainment or -1 if not in proggres.
            /// </summary>
            public static int TimeToRecontainment => Patches.SCP079RecontainPatch.SecondsLeft;

            /// <summary>
            /// Gets a value indicating whether is recontainment in proggres.
            /// </summary>
            public static bool IsBeingRecontained => Patches.SCP079RecontainPatch.Recontaining;

            /// <summary>
            /// Gets a value indicating whether recontainment is paused.
            /// </summary>
            public static bool IsRecontainmentPaused => Patches.SCP079RecontainPatch.Waiting;
        }
    }
}