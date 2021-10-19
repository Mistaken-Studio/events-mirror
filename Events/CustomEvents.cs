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
        public static event Exiled.Events.Events.CustomEventHandler<UnloadingWeaponEventArgs> UnloadingWeapon;

        /// <summary>
        /// Event called when player is sending RA command.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<SendingCommandEventArgs> SendingCommand;

        /// <summary>
        /// Event called when player is aiming-in or aiming-out.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<AimingEventArgs> Aiming;

        /// <summary>
        /// Event called when player is changing spectated player.
        /// </summary>
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
        /// Invokes <see cref="UnloadingWeapon"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeUnloadingWeapon(UnloadingWeaponEventArgs ev)
        {
            UnloadingWeapon.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="SendingCommand"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeSendingCommand(SendingCommandEventArgs ev)
        {
            SendingCommand.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="Aiming"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeAiming(AimingEventArgs ev)
        {
            Aiming.InvokeSafely(ev);
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
        public static class SCP079
        {
            /// <summary>
            /// Gets a value indicating whether is recontainment in proggres.
            /// </summary>
            public static bool IsBeingRecontained => Recontainer._prevEngaged >= 3;

            /// <summary>
            /// Gets a value indicating whether recontainment has finished.
            /// </summary>
            public static bool IsRecontained => Recontainer._alreadyRecontained;

            private static int roundId = -1;

            private static Recontainer079 recontainer;

            private static Recontainer079 Recontainer
            {
                get
                {
                    if (RoundPlus.RoundId != roundId)
                    {
                        recontainer = GameObject.FindObjectOfType<Recontainer079>();
                        roundId = RoundPlus.RoundId;
                    }

                    return recontainer;
                }
            }
        }
    }
}