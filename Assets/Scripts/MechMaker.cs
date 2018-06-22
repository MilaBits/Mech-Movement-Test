using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class MechMaker : MonoBehaviour {
    public List<SpawnPoint> SpawnPoints;

    public Loadout loadoutToSpawn;

    public GameObject baseMech;

    [Button("Test Spawn Mech")]
    public void testSpawnMech() {
        SpawnMech(loadoutToSpawn);
    }


    public void SpawnMech(Loadout loadout) {
        GameObject mech = Instantiate(baseMech);
        mech.transform.position = SpawnPoints[0].transform.position;

        // Set top frame model
        MeshFilter mechTorsoMeshFilter = mech.transform.GetChild(0).GetComponent<MeshFilter>();
        MeshCollider meshTorsoCollider = mech.transform.GetChild(0).GetComponent<MeshCollider>();

        mechTorsoMeshFilter.mesh = loadout.TopFrame.Model;
        meshTorsoCollider.sharedMesh = mechTorsoMeshFilter.sharedMesh;

        //Set bottom frame model
        MeshFilter mechBottomMeshFilter = mech.transform.GetChild(1).GetComponent<MeshFilter>();
        MeshCollider meshBottomCollider = mech.transform.GetChild(1).GetComponent<MeshCollider>();

        mechBottomMeshFilter.mesh = loadout.BottomFrame.Model;
        meshBottomCollider.sharedMesh = mechBottomMeshFilter.sharedMesh;

        // Set top frame health
        mechTorsoMeshFilter.GetComponent<Health>().BaseMaxHealth = loadout.TopFrame.Health;

        // Set bottom frame health
        mechBottomMeshFilter.GetComponent<Health>().BaseMaxHealth = loadout.BottomFrame.Health;

        MechController controller = mech.GetComponent<MechController>();
        controller.Loadout = loadout;
    }
}