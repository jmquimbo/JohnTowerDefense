 using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint arrowTurret;
    public TurretBlueprint cannonTurret;
    public TurretBlueprint slowTurret;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    
    public void SelectArrowTurret ()
    {
        buildManager.SelectTurretToBuild(arrowTurret);
    }

    public void SelectCannonTurret ()
    {
        buildManager.SelectTurretToBuild(cannonTurret);
    }

    public void SelectSlowTurret()
    {
        buildManager.SelectTurretToBuild(slowTurret);
    }
}
