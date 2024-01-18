using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;
    public int sellingcost;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        sellingcost = 0;

    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        /*if (buildManager.GetTurretToBuild() == null)
            return;*/

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

   

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        /*if (buildManager.GetTurretToBuild() == null)
           return;*/

      
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("no golds");
            return;
        }
        PlayerStats.Money -= blueprint.cost;
        sellingcost = (blueprint.cost / 2);

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        SpecialNode specialNode = GetComponentInChildren<SpecialNode>();
        if (specialNode != null)
        {
            specialNode.BoostTurret();
        }
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("no golds");
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;
        sellingcost = ((turretBlueprint.upgradeCost + turretBlueprint.cost) / 2);

        //get rid of old turret
        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;



        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        turretBlueprint.sellingcost = sellingcost;
        PlayerStats.Money += turretBlueprint.getSellAmount();
        
        

        Destroy(turret);

        //effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        turretBlueprint = null;
        isUpgraded = false;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
