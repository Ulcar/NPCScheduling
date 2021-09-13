using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.BT.Actions.Scene_Actions
{
    public class Wander : NPCActionBehaviour
    {
        [SerializeField]
        List<Vector3> wanderPoints = new List<Vector3>();
        
        public override void Tick(NPCAgentBehaviour objectData)
        {
           // Set NavMesh agent target position to random wander point
        }
    }
}
