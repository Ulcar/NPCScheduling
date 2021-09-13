using System.Collections;
using UnityEngine;
using System.Collections.Generic;
namespace Assets
{
    [CreateAssetMenu]
    public class NpcAgent : ScriptableObject
    {

        public string Name;

       public Assets.BT.BehaviourTreeNode BehaviourTree;

        public NPCAgentBehaviour agentBehaviour;

        public Vector3 currentPosition;

        public GameObject npcPrefab;


        public UnityEngine.SceneManagement.Scene currentScene;


        [HideInInspector]
        public List<NPCActionBehaviour> Actions = new List<NPCActionBehaviour>();



        public void Tick() 
        {
            BehaviourTree.Tick();
        }


        public void SpawnNPC() 
        {

        }


        public void OnUnload() { }
    }
}