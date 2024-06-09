using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SearchUtility
{

    public static bool SearchInSector(Vector3 viewerPos, Vector3 viewerDir, Vector3 targetPos, float viewingAngle, float viewingDistance, LayerMask wallsMask)
    {
        Vector3 toTargetDir = targetPos - viewerPos;
        Vector3 start = viewerPos;
        Vector3 end = targetPos;
        float angle = Vector3.Angle(viewerDir, toTargetDir);
        Vector3 toTargetXZ = new Vector3(toTargetDir.x, 0f, toTargetDir.z);
        float distance = toTargetXZ.magnitude;

        if (angle < viewingAngle && distance < viewingDistance)
        {
            if (Physics.Linecast(start, end, wallsMask) == false)
            {
                return true;
            }
        }
        return false;
    }
}
