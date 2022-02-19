using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commons;
using BulletServices;
using TankSO;


namespace TankServices
{
    public class TankService : GenericSingleton<TankService>
    {
        public TankScriptableObjectList tankList;
        public TankScriptableObject tankScriptable { get; private set; }
        private List<TankController> tanks = new List<TankController>();
        private Coroutine respawn;

        private void Start()
        {
            CreateTank();
        }
        public void CreateTank()
        {
            int rand = Random.Range(0, tankList.tanks.Length);
            tankScriptable = tankList.tanks[rand];

            TankModel tankModel = new TankModel(tankScriptable, tankList);
            TankController controller = new TankController(tankModel, tankScriptable.tankView);
            tanks.Add(controller);
        }

        private IEnumerator RespawnTank()
        {
            yield return new WaitForSeconds(4f);
            CreateTank();
            if (respawn != null)
            {
                StopCoroutine(respawn);
                respawn = null;
            }
        }
    }
}