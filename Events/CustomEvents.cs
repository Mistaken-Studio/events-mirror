// -----------------------------------------------------------------------
// <copyright file="CustomEvents.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features;
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
        public static event Exiled.Events.Events.CustomEventHandler<PickItemRequestEventArgs> OnRequestPickItem;

        /// <summary>
        /// Event called when broadcast was sent.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<BroadcastEventArgs> OnBroadcast;

        /// <summary>
        /// Event called when player joins first time in a session.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<FirstTimeJoinedEventArgs> OnFirstTimeJoined;

        /// <summary>
        /// Event called when player is changing attachments.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<ChangingAttachmentsEventArgs> OnChangingAttachments;

        /// <summary>
        /// Event called when player is changing attachments.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<UnloadingFirearmEventArgs> OnUnloadingFirearm;

        /// <summary>
        /// Invokes <see cref="OnRequestPickItem"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeOnRequestPickItem(ref PickItemRequestEventArgs ev)
        {
            OnRequestPickItem.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="OnBroadcast"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeOnBroadcast(ref BroadcastEventArgs ev)
        {
            OnBroadcast.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="OnFirstTimeJoined"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeOnFirstTimeJoined(FirstTimeJoinedEventArgs ev)
        {
            OnFirstTimeJoined.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="OnChangingAttachments"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeOnChangingAttachments(ChangingAttachmentsEventArgs ev)
        {
            OnChangingAttachments.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="OnUnloadingFirearm"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeOnUnloadingFirearm(UnloadingFirearmEventArgs ev)
        {
            OnUnloadingFirearm.InvokeSafely(ev);
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