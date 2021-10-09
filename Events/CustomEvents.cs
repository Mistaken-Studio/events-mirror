// -----------------------------------------------------------------------
// <copyright file="CustomEvents.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features;
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
            try
            {
                OnRequestPickItem?.Invoke(ev);
            }
            catch (System.Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
            }
        }

        /// <summary>
        /// Invokes <see cref="OnBroadcast"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeOnBroadcast(ref BroadcastEventArgs ev)
        {
            try
            {
                OnBroadcast?.Invoke(ev);
            }
            catch (System.Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
            }
        }

        /// <summary>
        /// Invokes <see cref="OnRequestPickItem"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeOnFirstTimeJoined(FirstTimeJoinedEventArgs ev)
        {
            try
            {
                OnFirstTimeJoined?.Invoke(ev);
            }
            catch (System.Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
            }
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