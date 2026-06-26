using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    MapData MapData => ServiceProvider.Instance.GetService<MapData>();

    [SerializeField] private ShipManagerView shipManagerViewPrefab;
    [SerializeField] private GameObject laneStartPrefab;
    [SerializeField] private GameObject laneEndPrefab;

    [SerializeField] private Lane[] lanes;
    [SerializeField] private float laneSize = 1;

    [SerializeField] private PlayerView playerView;

    [SerializeField] private AudioClip[] clips;

    private void Awake()
    {
        ServiceProvider.Instance.AddService<MapData>(new MapData(lanes.Length, laneSize));
        ServiceProvider.Instance.AddService<ClipsData>(new ClipsData(clips));

        new PlayerPresenter(new PlayerModel(), playerView);
        for(int i = 0; i < lanes.Length; i++)
        {
            Vector2 pos = new Vector2(0.0f, i * MapData.LaneSize);
            lanes[i].Index = i;
            switch (lanes[i].Type)
            {
                case LaneType.Start:
                    Instantiate(laneStartPrefab).transform.position = pos;
                    break;
                case LaneType.Street:
                    new ShipManagerPresenter(
                        new ShipManagerModel(lanes[i]),
                        Instantiate(shipManagerViewPrefab), 
                        Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x * 2);
                    break;
                case LaneType.End:
                    Instantiate(laneEndPrefab).transform.position = pos;
                    break;
            }
        }
    }
}
