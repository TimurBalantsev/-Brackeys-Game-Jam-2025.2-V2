using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private Image itemicon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemAmountText;

    [SerializeField] private GameObject popRewardContainer;
    [SerializeField] private GameObject popConsequenceContainer;

    [SerializeField] private TextMeshProUGUI popRewardText;
    [SerializeField] private TextMeshProUGUI popConsequenceText;

    [SerializeField] private Image typeIcon;

    public void SetQuest(Quest quest)
    {
        itemicon.sprite = quest.Item.icon;
        itemNameText.text = quest.Item.name;
        itemAmountText.text = "x" + quest.Amount.ToString();

        if (quest.PopReward > 0)
        {
            popRewardText.text = "+" + quest.PopReward.ToString();
        }
        else
        {
            popRewardContainer.SetActive(false);
        }

        if (quest.PopConsequence > 0)
        {
            popConsequenceText.text = "-" + quest.PopConsequence.ToString();
        }
        else
        {
            popConsequenceContainer.SetActive(false);
        }

        typeIcon.color = ItemTypeHelper.Instance.GetItemTypeColor(quest.Item.itemType);
        typeIcon.sprite = ItemTypeHelper.Instance.GetItemTypeSprite(quest.Item.itemType);
    }
}
