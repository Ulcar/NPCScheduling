using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.BT.Conditions
{
  public  class TimeCondition:BehaviourTreeNode
    {

        [SerializeField]
        private float startTime, endTime;
        
        
        public override BehaviourStatus Tick()
        {
            return base.Tick();
        }
    }
}
