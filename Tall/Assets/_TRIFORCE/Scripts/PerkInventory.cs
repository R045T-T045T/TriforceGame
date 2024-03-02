using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkInventory : MonoBehaviour
{
    private int brokenRules = 0;

    [SerializeField] private Text rulesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addRule(Rule instance)
    {
        brokenRules++;
        rulesText.text = "Rules; " + brokenRules;
    }

    public void removeRule(Rule instance) 
    {
        brokenRules--;
        rulesText.text = "Rules; " + brokenRules;
    }
    public void Reset()
    {

    }
}
