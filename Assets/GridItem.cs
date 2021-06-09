using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridItem : MonoBehaviour, IPointerClickHandler {

	public GridInfo Info;
	public Color ObstacleColor;
	public Color GroundColor;
	public Color StarColor;
	public Color EndColor;
	private Image bg;
	public AStarManager Manager;

	public Text F;
	public Text G;
	public Text H;
	public Text IDT;
	public Transform Arrow;
	public GameObject Cur;

	// Use this for initialization
	void Start () {
		bg = GetComponent<Image>();
	}

	public void Init(int id, Vector2 pos)
    {
		IDT.text = id.ToString();
		transform.localPosition = pos * 80;
		gameObject.name = id.ToString();
	}

	public void SetInfo(GridInfo info)
    {
		Info = info;
		SetColor(info.ItemType);
		F.text = info.F > 0 ? info.F.ToString() : "";
		G.text = info.G > 0 ? info.G.ToString() : "";
		H.text = info.H > 0 ? info.H.ToString() : "";
		if(info.ParentInfo == null)
        {
			Arrow.gameObject.SetActive(false);
        }
        else
        {
			Arrow.gameObject.SetActive(true);
			var x = info.ParentInfo.Pos.x - info.Pos.x;
			var y = info.ParentInfo.Pos.y - info.Pos.y;
			if(x == 0)
            {
				if(y == 1)
                {
					Arrow.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
					Arrow.eulerAngles = new Vector3(0, 0, 180);
				}
            }else if(x == 1)
            {
				if(y == 1)
                {
					Arrow.eulerAngles = new Vector3(0, 0, 315);
				}
				else if(y == 0)
                {
					Arrow.eulerAngles = new Vector3(0, 0, 270);
				}
                else
                {
					Arrow.eulerAngles = new Vector3(0, 0, 225);
				}
            }
            else
            {
				if (y == 1)
				{
					Arrow.eulerAngles = new Vector3(0, 0, 45);
				}
				else if (y == 0)
				{
					Arrow.eulerAngles = new Vector3(0, 0, 90);
				}
				else
				{
					Arrow.eulerAngles = new Vector3(0, 0, 135);
				}
			}

		}
		Cur.SetActive(Manager.CurInfo.ID == info.ID);
	}
	
	public void SetColor(ItemType type)
    {
        switch (type)
        {
            case ItemType.Ground:
				bg.color = GroundColor;
                break;
            case ItemType.Obstacle:
				bg.color = ObstacleColor;
                break;
			case ItemType.Start:
				bg.color = StarColor;
				break;
			case ItemType.End:
				bg.color = EndColor;
				break;
			default:
				bg.color = Color.white;
				break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
		var info = Manager.InfoDict[Info.ID];
        switch (info.ItemType)
        {
            case ItemType.Ground:
				info.ItemType = ItemType.Obstacle;
				break;
            case ItemType.Obstacle:
				info.ItemType = ItemType.Start;
				break;
            case ItemType.Start:
				info.ItemType = ItemType.End;
				break;
            case ItemType.End:
				info.ItemType = ItemType.Ground;
				break;
            default:
                break;
        }
    }
}

