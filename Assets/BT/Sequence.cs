using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.BT
{
   public class Sequence:BehaviourTreeNode
    {


        private int currentChildIndex;
        public override BehaviourStatus Tick()
        {

            BehaviourStatus status = BehaviourStatus.FAILURE;
            for (int i = currentChildIndex; i < children.Count; i++) 
            {
               status = children[i].Tick();


                switch (status) 
                {
                    case BehaviourStatus.FAILURE:
                        return status;


                    case BehaviourStatus.RUNNING:
                        // save current node pos and return
                        currentChildIndex = i;
                        return status;
                }
            }
            // reset Sequence
            currentChildIndex = 0;

            return status;
        }
    }
}
