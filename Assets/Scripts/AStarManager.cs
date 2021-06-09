using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarManager : MonoBehaviour {

	[Header("地图大小")]
	public Vector2 Size;
	public Dictionary<int, GridInfo> InfoDict;
	public GridInfo CurInfo;
	public GridInfo StartInfo;
	public GridInfo EndInfo;

	public List<int> OpenIds;
	public List<int> CloseIds;

	public bool Check = false;
	public bool Next = false;

	void Awake()
    {
		InfoDict = new Dictionary<int, GridInfo>();
		for (int i = 0; i < Size.x; i++)
		{
			for (int j = 0; j < Size.y; j++)
			{
				var info = new GridInfo();
				info.Reset();
				info.Pos = new Vector2(j, i);
				info.ID = (int)(i * Size.y) + j;
				InfoDict[info.ID] = info;
			}
		}
	}

	// Use this for initialization
	void Start() {
		
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Check)
        {
			Check = !Check;
			Reset();
		}
        if (Next)
        {
			Next = !Next;
			GotoNext();

		}
	}

	void Reset()
    {
        foreach (var info in InfoDict.Values)
        {
			info.Reset();
			if(info.ItemType == ItemType.Start)
            {
				StartInfo = info;
            }else if(info.ItemType == ItemType.End)
            {
				EndInfo = info;
            }
        }
		CurInfo = StartInfo;
		OpenIds.Clear();
		CloseIds.Clear();

	}

	void GotoNext()
    {
        if (CurInfo == null)
        {
			return;
        }

		CurInfo.Close = true;
        for (int i = -1; i <= 1; i++)
        {
			for (int j = -1; j <= 1; j++)
			{
				if(i == 0 && j == 0)
                {
					continue;
                }
				var x = CurInfo.Pos.y + i;
				var y = CurInfo.Pos.x + j;
				if(x < 0 || y < 0)
                {
					continue;
                }
				var id = (int)(x * Size.y + y);
				print(id);
				GridInfo info;
				if(InfoDict.TryGetValue(id, out info))
                {
					if(info.ItemType == ItemType.Obstacle)
                    {
						continue;
                    }
                    if (info.Close)
                    {
						continue;
                    }
                    if (info.Open)
                    {
						if(info.G < CurInfo.G + GetG(CurInfo, info))
                        {
							continue;
                        }
                    }
					if(info.ItemType == ItemType.End)
                    {
						print("到达终点");
						CurInfo = null;
						return;
                    }
					info.Open = true;
					info.ParentInfo = CurInfo;
					info.G = CurInfo.G + GetG(CurInfo, info);
					info.H = GetH(EndInfo, info);
				}
			}
		}

		var min_f = int.MaxValue;
		GridInfo min_info = null;
		foreach (var info in InfoDict.Values)
        {
			if (!info.Close && info.Open && info.F < min_f)
			{
				min_f = info.F;
				min_info = info;
			}
		}
		if(min_info == null)
        {
			print("没有路径");
			CurInfo = null;
			return;
        }
		CurInfo = min_info;
	}
	int GetG(GridInfo info, GridInfo _info)
    {
		if(info.Pos.x == _info.Pos.x || info.Pos.y == _info.Pos.y)
        {
			return 10;
        }
        else
        {
			return 14;
        }
    }

	int GetH(GridInfo info, GridInfo _info)
    {
		return (int)Mathf.Abs(info.Pos.x - _info.Pos.x) * 10 + (int)Mathf.Abs(info.Pos.y - _info.Pos.y) * 10;
    }
}
