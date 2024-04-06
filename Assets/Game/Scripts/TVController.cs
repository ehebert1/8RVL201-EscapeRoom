using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.Video;

public class TVController : MonoBehaviour
{
    const string projection_screen = "ProjectionScreen";
    const string wall_tv = "WallTV";
    const string rickroll_screen = "RickRollScreen";

    public List<Material> material;

    public void RickRoll()
    {
        var obj = GameObject.FindObjectsOfType(typeof(MonoBehaviour));

        foreach (var item in obj)
        {
            if (item.name.Contains(projection_screen) || item.name.Contains(wall_tv))
            {
                Transform itemGameObj = item.GameObject().transform;

                for (int i = 0; i < itemGameObj.childCount; i++)
                {
                    Transform child = itemGameObj.GetChild(i);

                    if (child.name.Equals(rickroll_screen))
                    {
                        var rickroll_player = child.transform.gameObject.GetComponent<VideoPlayer>();

                        if (!rickroll_player) continue;

                        rickroll_player.Play();

                        if (item.name == projection_screen)
                        {
                            rickroll_player.SetDirectAudioMute(0, true);
                        }

                        child.transform.gameObject.GetComponent<MeshRenderer>().SetMaterials(material);
                    }
                }
            }
        }
    }
}