using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.GameLogic
{
  public  class NPCAgentHandler:MonoBehaviour
    {

        public List<NpcAgent> agents = new List<NpcAgent>();

        [SerializeField]
        private float tickTime = 1f;

        private float currentTime = 0;

        private void Awake()
        {

            DontDestroyOnLoad(this.gameObject);
        }
        private void Update()
        {


            currentTime += Time.deltaTime;


            if (currentTime > tickTime) 
            {
                currentTime = 0;
                UpdateAgents();
            }
        }

        private void UpdateAgents() 
        {
            foreach (NpcAgent agent in agents)
            {
                agent.Tick();
            }
        }



        private void LoadNPCsInScene() 
        {
            // Loop Through every npc, and spawn prefab if npc's scene is the same as the loaded scene
        }
    }
}
