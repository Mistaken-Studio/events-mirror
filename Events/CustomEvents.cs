// -----------------------------------------------------------------------
// <copyright file="CustomEvents.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.Events.Extensions;
using Mistaken.API;
using Mistaken.Events.EventArgs;
using UnityEngine;

namespace Mistaken.Events.Handlers
{
    /// <summary>
    /// Custom Events.
    /// </summary>
    public static class CustomEvents
    {
        /// <summary>
        /// Event called when requesting Item Pickup.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<PickItemRequestEventArgs> RequestPickItem;

        /// <inheritdoc cref="RequestPickItem"/>
        [System.Obsolete("Use RequestPickItem", true)]
        public static event Exiled.Events.Events.CustomEventHandler<PickItemRequestEventArgs> OnRequestPickItem
        {
            add => RequestPickItem += value;
            remove => RequestPickItem -= value;
        }

        /// <summary>
        /// Event called when player joins first time in a session.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<FirstTimeJoinedEventArgs> FirstTimeJoined;

        /// <inheritdoc cref="FirstTimeJoined"/>
        [System.Obsolete("Use FirstTimeJoined", true)]
        public static event Exiled.Events.Events.CustomEventHandler<FirstTimeJoinedEventArgs> OnFirstTimeJoined
        {
            add => FirstTimeJoined += value;
            remove => FirstTimeJoined -= value;
        }

        /// <summary>
        /// Event called when player is changing attachments.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<ChangingAttachmentsEventArgs> ChangingAttachments;

        /// <summary>
        /// Event called when player is unloading weapon.
        /// </summary>
        [System.Obsolete("Use Exiled.Events.Handlers.Player.UnloadingWeapon", true)]
        public static event Exiled.Events.Events.CustomEventHandler<Exiled.Events.EventArgs.UnloadingWeaponEventArgs> UnloadingWeapon
        {
            add => Exiled.Events.Handlers.Player.UnloadingWeapon += value;
            remove => Exiled.Events.Handlers.Player.UnloadingWeapon -= value;
        }

        /// <summary>
        /// Event called when player is sending RA command.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<SendingCommandEventArgs> SendingCommand;

        /// <summary>
        /// Event called when player is aiming-in or aiming-out.
        /// </summary>
        [System.Obsolete("Use Exiled.Events.Handlers.Player.AimingDownSight", true)]
        public static event Exiled.Events.Events.CustomEventHandler<Exiled.Events.EventArgs.AimingDownSightEventArgs> Aiming
        {
            add => Exiled.Events.Handlers.Player.AimingDownSight += value;
            remove => Exiled.Events.Handlers.Player.AimingDownSight -= value;
        }

        /// <summary>
        /// Event called when player is changing spectated player.
        /// </summary>
        [System.Obsolete("Use Exiled.Events.Handlers.Player.UnloadingWeapon (Will be in next release after 3.3.1)")]
        public static event Exiled.Events.Events.CustomEventHandler<ChangingSpectatedPlayerEventArgs> ChangingSpectatedPlayer;

        /// <summary>
        /// Invokes <see cref="RequestPickItem"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeRequestPickItem(PickItemRequestEventArgs ev)
        {
            RequestPickItem.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="FirstTimeJoined"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeFirstTimeJoined(FirstTimeJoinedEventArgs ev)
        {
            FirstTimeJoined.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="ChangingAttachments"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeChangingAttachments(ChangingAttachmentsEventArgs ev)
        {
            ChangingAttachments.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="SendingCommand"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeSendingCommand(SendingCommandEventArgs ev)
        {
            SendingCommand.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="ChangingSpectatedPlayer"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeChangingSpectatedPlayer(ChangingSpectatedPlayerEventArgs ev)
        {
            ChangingSpectatedPlayer.InvokeSafely(ev);
        }

        /// <summary>
        /// Data about SCP079.
        /// </summary>
        [System.Obsolete("Use MapPlus", true)]
        public static class SCP079
        {
            /// <summary>
            /// Gets a value indicating whether is recontainment in proggres.
            /// </summary>
            [System.Obsolete("Use MapPlus.IsSCP079ReadyForRecontainment", true)]
            public static bool IsBeingRecontained => MapPlus.IsSCP079ReadyForRecontainment;

            /// <summary>
            /// Gets a value indicating whether recontainment has finished.
            /// </summary>
            [System.Obsolete("Use MapPlus.IsSCP079Recontained", true)]
            public static bool IsRecontained => MapPlus.IsSCP079Recontained;
        }
    }
}