using UnityEngine;
using System.Collections;

using Vuforia;

[RequireComponent(typeof(Camera))]
public class CameraVerticalFlip : MonoBehaviour
{
    private int count = 0;
    private bool mFlipReady = false;
    private bool mFlipped = false;
    private bool mFlipVertical = true;
    private bool mFlipHorizontal = false;

    void OnPreCull()
    {
        if (!mFlipped)
        {
            if (mFlipReady)
            {
                Camera cam = GetComponent<Camera>();
                Vector3 flipScale = new Vector3(mFlipHorizontal ? -1 : 1, mFlipVertical ? -1 : 1, 1);
                Matrix4x4 projMat = cam.projectionMatrix * Matrix4x4.Scale(flipScale);
                cam.projectionMatrix = projMat;
                mFlipped = true;
            }

            if (count >= 40)
            {
                mFlipReady = true;
            }
            else
            {
                count++;
            }
        }
    }

    void OnPreRender()
    {
        if ((mFlipVertical && !mFlipHorizontal) ||
            (mFlipHorizontal && !mFlipVertical))
        {
            GL.SetRevertBackfacing(true);
        }
    }

    // Set it to false again because we don't want to affect all other cameras.
    void OnPostRender()
    {
        if ((mFlipVertical && !mFlipHorizontal) ||
            (mFlipHorizontal && !mFlipVertical))
        {
            GL.SetRevertBackfacing(false);
        }
    }
}