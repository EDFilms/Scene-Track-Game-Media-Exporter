// Copyright 2018 E*D Films. All Rights Reserved.

/**
 * SceneTrackMultiBoneProxy.cs
 *
 * Helper proxy to handle bone issues.
 * 
 * @author  dotBunny <hello@dotbunny.com>
 * @version 1
 * @since	1.0.0
 */


using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace SceneTrack.Unity
{
#if UNITY_EDITOR
    // Proxy Object, for transform.hasChanged, alerts multiple bones once it has changed
    // Created automatically through SceneTrackObject should not be added through the editor!
    [DisallowMultipleComponent]
    public class SceneTrackMultiBoneProxy : MonoBehaviour
    {
    }
#endif
}
