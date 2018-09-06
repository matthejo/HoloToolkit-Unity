﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.MixedReality.Toolkit.Core.Definitions.Utilities;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Core.Definitions.InputSystem
{
    /// <summary>
    /// Configuration profile settings for setting up and consuming Speech Commands.
    /// </summary>
    [CreateAssetMenu(menuName = "Mixed Reality Toolkit/Mixed Reality Speech Commands Profile", fileName = "MixedRealitySpeechCommandsProfile", order = (int)CreateProfileMenuItemIndices.Speech)]
    public class MixedRealitySpeechCommandsProfile : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Whether the recognizer should be activated on start.")]
        private AutoStartBehavior startBehavior = AutoStartBehavior.AutoStart;

        /// <summary>
        /// The list of Speech Commands users use in your application.
        /// </summary>
        public AutoStartBehavior SpeechRecognizerStartBehavior => startBehavior;

        [SerializeField]
        [Tooltip("Select the minimum confidence level for recognized words")]
        private RecognitionConfidenceLevel recognitionConfidenceLevel = RecognitionConfidenceLevel.Medium;

        /// <summary>
        /// The speech recognizer's minimum confidence level setting that will raise the action.<para/>
        /// 0 == High, 1 == Medium, 2 == Low, 3 == Unknown
        /// </summary>
        public RecognitionConfidenceLevel SpeechRecognitionConfidenceLevel => recognitionConfidenceLevel;

        [SerializeField]
        [Tooltip("The list of Speech Commands users use in your application.")]
        private SpeechCommands[] speechCommands = new SpeechCommands[0];

        /// <summary>
        /// The list of Speech Commands users use in your application.
        /// </summary>
        public SpeechCommands[] SpeechCommands => speechCommands;
    }
}