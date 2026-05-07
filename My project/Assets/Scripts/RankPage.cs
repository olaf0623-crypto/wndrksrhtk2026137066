using UnityEngine;
using System.Linq;
using TMPro; 

public class RankPage : MonoBehaviour
{

    [SerializeField] Transform contentRoot;

    [SerializeField] GameObject rowPrefab;

    StageResultList allData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        allData = StageResultSaver.LoadRank();
        RefreshRankList();
    }

    // Update is called once per frame
    void RefreshRankList()
    {
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        var sortedData = allData.results.Where(r => r.stage == 1).OrderByDescending(x => x.score).ToList();

        for(int i = 0; i < sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData[i].playerName} - {sortedData[i].score}";
        }
    }
}
