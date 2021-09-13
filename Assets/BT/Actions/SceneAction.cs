using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.BT
{
    
    
    // Running Action that has to run in a Scene. Returns Failure if current NPC is not in scene?
    public class SceneAction:BehaviourTreeNode
    {

        NPCActionBehaviour behaviourInstance;



        public BehaviourStatus currentStatus;


        public override BehaviourStatus Tick()
        {


            // set action to
            
            return currentStatus;
        }
    }
}
