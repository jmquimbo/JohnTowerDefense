using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;

    public GameObject ui;
    public Text upgradeCost;
    public Text sellCost;

    public Button upgradeButton;
    public Button sellButton;

    public int sellingcost;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        ui.SetActive(true);


        if (target.isUpgraded)
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
            sellingcost = (target.turretBlueprint.upgradeCost+target.turretBlueprint.cost) / 2;
        }
        else
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
            sellingcost = target.turretBlueprint.cost / 2;
        }
        
        sellCost.text = "$" + sellingcost;
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();   
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
