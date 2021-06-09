using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GridInfo {

	public int ID;
	private int f;
	public int F
    {
        get { return f; }
    }
	private int g;
	public int G
    {
        get { return g; }
        set { 
			g = value;
			f = g + h;
		}
    }
	private int h;
	public int H
    {
        get { return h; }
        set { 
			h = value;
			f = g + h;
		}
    }
	public GridInfo ParentInfo;
	public Vector2 Pos;
	public ItemType ItemType;
	public bool Open;
	public bool Close;

	public void Reset()
    {
		G = 0;
		H = 0;
		ParentInfo = null;
		Open = false;
		Close = false;

	}
}

public enum ItemType
{
	Ground,
	Obstacle,
	Start,
	End,
}