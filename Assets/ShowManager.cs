using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShowManager : MonoBehaviour {

	public AStarManager Manager;
	public Transform Parent;
	public GridItem Item;
	public Dictionary<int, GridItem> GridItems;

	// Use this for initialization
	void Start () {
		GridItems = new Dictionary<int, GridItem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Item == null)
        {
			return;
        }
        foreach (var info in Manager.InfoDict.Values)
        {
			GridItem item;
			if(GridItems.TryGetValue(info.ID, out item))
            {
				item.SetInfo(info);
			}
            else
            {
				item = Instantiate(Item);
				item.transform.SetParent(Parent);
				item.Manager = Manager;
				item.Init(info.ID, info.Pos);
				GridItems.Add(info.ID, item);
            }
        }
	}
}
