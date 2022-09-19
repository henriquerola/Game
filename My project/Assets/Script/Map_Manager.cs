using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map_Manager : MonoBehaviour
{
    public GameObject selectedtiles_prefab; // selecttiles prefab
    public GameObject SelectedTilesContainer; // um lugar pra colocar todos os select tiles

    [SerializeField]
    private Tilemap map; // escolher qual map

    [SerializeField]
    private List<Tile_Data> datalist; // informação das tiles

    private Dictionary<TileBase,Tile_Data> datafromtiles; // dicionario com as informaçoes das tile e sua posicao

    // coloca as informações das tiles no dict datafromtiles
    private void Awake() { 
        datafromtiles = new Dictionary<TileBase,Tile_Data>();

        foreach (var data in datalist) 
        {
            foreach (var tile in data.tiles) 
            {
                datafromtiles.Add(tile,data);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var groundtile = gameObject.GetComponentInChildren<Tilemap>(); // conectado na grid, logo in children vai pegar ground como var
        
        BoundsInt bounds = groundtile.cellBounds; // pega o limite do mapa

        // selected tiles placement
        for (int y = bounds.min.y; y < bounds.max.y; y++) { // loop pelo ground (da pra fazer ele passar por outros grid como elevation)
            for (int x = bounds.min.x; x < bounds.max.x; x++) {
                // loc do tile
                var tilelocation = new Vector3Int(x,y,0);
                // ve se tem um tile na localizacao (da pra fazer buracos com isso) 
                if(groundtile.HasTile(tilelocation)) {
                    var selecttile = Instantiate(selectedtiles_prefab, SelectedTilesContainer.transform); // coloca o selecttiles em cada tile do ground
                    var cellpos = groundtile.GetCellCenterWorld(tilelocation); //localizacao como cell e nao pos
                    
                    selecttile.transform.position = new Vector3(cellpos.x,cellpos.y,cellpos.z+1); // coloca na posicao correta com o z+1 pra aparecer na frente
                }

            }
        }
    }

    private void Update()
    {
        var groundtile = gameObject.GetComponentInChildren<Tilemap>();
        BoundsInt bounds = groundtile.cellBounds;
        
        // verifica se tem um tile onde vc clicou e pode pegar informações da tile ( apcost type etc)
        if(Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // posisaco 2d do mouse
            Vector3Int gridposition = map.WorldToCell(mousePosition); // posicao grid do mouse

            TileBase clickedtile = map.GetTile(gridposition); // tile clicada

            if(groundtile.HasTile(gridposition)) { // verifica se tem um tile no local do mouse
                float apcost = datafromtiles[clickedtile].APcost; // acessa as variaveis de uma tile
                // Debug.Log("pra andar custa " + apcost); 
            }
        }
        // identificar unidade em cima da tile ( INCOMPLETO )
        for (int y = bounds.min.y; y < bounds.max.y; y++) { // loop pelo ground (da pra fazer ele passar por outros grid como elevation)
            for (int x = bounds.min.x; x < bounds.max.x; x++) {
                // loc do tile
                var tilelocation = new Vector3Int(x,y,0);
                // ve se tem um tile na localizacao (da pra fazer buracos com isso) 
                if(groundtile.HasTile(tilelocation)) {
                    var cellpos = groundtile.GetCellCenterWorld(tilelocation);
                    RaycastHit2D[] hits = Physics2D.RaycastAll(cellpos, Vector2.zero);

                    for(int i = 0; i < hits.Length; i++){
                        if(hits[i].collider.gameObject.name == "Unit(Clone)") 
                        {
                            Tile_Data a = GetTileData(tilelocation); 
                            if(a != null) 
                            {
                                a.Hasunit = true;
                                Debug.Log("1");      
                            }
                            else
                            {
                                Destroy(hits[i].collider.gameObject);
                            }                   
                        }
                    }
                }
            }
            
        }
    }

    public Tile_Data GetTileData(Vector3Int tilelocation) 
    {
        TileBase tile = map.GetTile(tilelocation);

        if (tile == null) 
            return null;
        else
            return datafromtiles[tile];

    }
}
