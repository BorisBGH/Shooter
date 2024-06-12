using System.Collections;
using System.Collections.Generic;
using System.Linq;
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




    public static List<EnemyStateMachine> SearchEnemiesInSector(Vector3 viewerPos, Vector3 viewerDir, List<EnemyStateMachine> targets, float viewingAngle, float viewingDistance, LayerMask wallsMask)
    {
        List<EnemyStateMachine> foundEnemies = new List<EnemyStateMachine>();

        foreach (var target in targets)
        {

            Vector3 toTargetDir = target.transform.position - viewerPos;
            Vector3 start = viewerPos;
            Vector3 end = target.transform.position;
            float angle = Vector3.Angle(viewerDir, toTargetDir);
            Vector3 toTargetXZ = new Vector3(toTargetDir.x, 0f, toTargetDir.z);
            float distance = toTargetXZ.magnitude;

            if (angle < viewingAngle && distance < viewingDistance && !foundEnemies.Contains(target))
            {
                if (Physics.Linecast(start, end, wallsMask) == false)
                {
                    foundEnemies.Add(target);
                }
            }
            //else
            //{
            //    if(foundEnemies.Contains(target))
            //    {
            //        foundEnemies.Remove(target);
            //    }
            //}
        }
        return foundEnemies;


    }

    public static List<AllyBotStateMachine> SearchAlliesInSector(Vector3 viewerPos, Vector3 viewerDir, List<AllyBotStateMachine> targets, float viewingAngle, float viewingDistance, LayerMask wallsMask)
    {
        List<AllyBotStateMachine> foundAllies = new List<AllyBotStateMachine>();

        foreach (var target in targets)
        {

            Vector3 toTargetDir = target.transform.position - viewerPos;
            Vector3 start = viewerPos;
            Vector3 end = target.transform.position;
            float angle = Vector3.Angle(viewerDir, toTargetDir);
            Vector3 toTargetXZ = new Vector3(toTargetDir.x, 0f, toTargetDir.z);
            float distance = toTargetXZ.magnitude;

            if (angle < viewingAngle && distance < viewingDistance && !foundAllies.Contains(target))
            {
                if (Physics.Linecast(start, end, wallsMask) == false)
                {
                    foundAllies.Add(target);
                }
            }

        }
        return foundAllies;


    }

    public static Transform FindClosestTarget(Vector3 viewer, List<Transform> targets)
    {
        Transform closestTarget = targets.OrderBy(target => (Vector3.Distance(viewer, target.transform.position))).FirstOrDefault();
        return closestTarget;
    }
}



