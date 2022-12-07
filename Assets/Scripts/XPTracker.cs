using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class XPTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CurrentLevelText;
    [SerializeField] TextMeshProUGUI CurrentXPText;
    [SerializeField] TextMeshProUGUI XPToNextLevelText;

    [SerializeField] BaseXPTransfer XPTransferType;

    [SerializeField] UnityEvent<int, int> OnLevelChanged = new UnityEvent<int, int>();
    BaseXPTransfer XPTransfer;

    private void Awake()
    {
        XPTransfer = ScriptableObject.Instantiate(XPTransferType);
    }

    public void AddXP(int amount)
    {
        int previousLevel = XPTransfer.CurrentLevel;
        if (XPTransfer.AddXP(amount))
        {
            OnLevelChanged.Invoke(previousLevel, XPTransfer.CurrentLevel);
        }

        RefreshDisplays();
    }

    public void SetLevel(int level)
    {
        int previousLevel = XPTransfer.CurrentLevel;
        XPTransfer.SetLevel(level);

        if (previousLevel != XPTransfer.CurrentLevel)
        {
            OnLevelChanged.Invoke(previousLevel, XPTransfer.CurrentLevel);
        }

        RefreshDisplays();
    }

    void Start()
    {
        RefreshDisplays();

        OnLevelChanged.Invoke(0, XPTransfer.CurrentLevel);
    }

    void Update()
    {
        
    }

    void RefreshDisplays()
    {
        CurrentLevelText.text = $"Current Level: {XPTransfer.CurrentLevel}";
        CurrentXPText.text = $"Current XP: {XPTransfer.CurrentXP}";
        if (!XPTransfer.AtLevelCap)
            XPToNextLevelText.text = $"XP To Next Level: {XPTransfer.XPRequiredForNextLevel}";
        else
            XPToNextLevelText.text = $"XP To Next Level: At Max";
    }
}
