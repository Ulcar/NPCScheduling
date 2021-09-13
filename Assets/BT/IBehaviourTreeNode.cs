using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.BT
{
    public abstract class BehaviourTreeNode : ScriptableObject
    {

        public BehaviourTreeNode parent;

        public List<BehaviourTreeNode> children = new List<BehaviourTreeNode>();

        public NpcAgent parentAgent;
        

        public virtual BehaviourStatus Tick() => BehaviourStatus.FAILURE;


        public void SetNPCAgent(NpcAgent agent) 
        {
            parentAgent = agent;

            foreach (BehaviourTreeNode child in children) 
            {
                child.SetNPCAgent(agent);
            }
        }
    }
}
