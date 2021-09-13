using System.Collections;
using UnityEngine;

namespace Assets
{
    public abstract class NPCActionBehaviour : ScriptableObject
    {

        Assets.BT.SceneAction linkedAction;

        public abstract void Tick(NPCAgentBehaviour objectData);
    }
}