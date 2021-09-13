using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.IO;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
public class ItemDatabase : EditorWindow
{
    private Sprite m_DefaultItemIcon;



    // this can be any serializeable class, doesn't have to be scriptableObject
    private static List<LootboxData> m_ItemDatabase = new List<LootboxData>();

    private static int itemIndex = 0;

    private List<LootboxData> dynamicData = new List<LootboxData>();

    private VisualElement m_ItemsTab;
    private static VisualTreeAsset m_ItemRowTemplate;
    private ListView m_ItemListView;
    private float m_ItemHeight = 40;
    private ObjectField test;

    private ScrollView m_DetailSection;
    private VisualElement m_LargeDisplayIcon;
    private LootboxData m_activeItem;




    [MenuItem("WUG/Item Database")]
    public static void Init()
    {
        ItemDatabase wnd = GetWindow<ItemDatabase>();
        wnd.titleContent = new GUIContent("Item Database");
        Vector2 size = new Vector2(800, 475);
        wnd.minSize = size;
        wnd.maxSize = size;
    }
    public void CreateGUI()
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>
            ("Assets/WUG/Editor/ItemDatabase.uxml");
        VisualElement rootFromUXML = visualTree.Instantiate();
        rootVisualElement.Add(rootFromUXML);
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>
            ("Assets/WUG/Editor/ItemDatabase.uss");
        rootVisualElement.styleSheets.Add(styleSheet);
        m_DefaultItemIcon = (Sprite)AssetDatabase.LoadAssetAtPath(
            "Assets/WUG/Sprites/UnknownIcon.png", typeof(Sprite));

        m_ItemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>
    ("Assets/WUG/Editor/ItemRowTemplate.uxml");

        LoadAllItems();

        m_ItemsTab = rootVisualElement.Q<VisualElement>("ItemsTab");
        test = rootVisualElement.Q<ObjectField>("IconPicker");
        test.objectType = typeof(Sprite);
        GenerateListView();

        m_DetailSection = rootVisualElement.Q<ScrollView>("ScrollView_Details");
        m_DetailSection.style.visibility = Visibility.Hidden;
        m_LargeDisplayIcon = m_DetailSection.Q<VisualElement>("Icon");
        rootVisualElement.Q<Button>("Btn_AddItem").clicked += AddItem_OnClick;
        rootVisualElement.Q<Button>("Btn_DeleteItem").clicked += DeleteItem_OnClick;
    }

    private void LoadAllItems()
    {
        m_ItemDatabase.Clear();
        string[] allPaths = Directory.GetFiles("Assets/Data", "*.asset",
            SearchOption.AllDirectories);
        foreach (string path in allPaths)
        {
            string cleanedPath = path.Replace("\\", "/");
            m_ItemDatabase.Add((LootboxData)AssetDatabase.LoadAssetAtPath(cleanedPath,
                typeof(LootboxData)));
            
        }
        itemIndex = m_ItemDatabase.Count;
    }

    private void GetRuntimeScriptables() 
    {

    }

    private void AddItem_OnClick()
    {
        //Create an instance of the scriptable object and set the default parameters
        LootboxData newItem = CreateInstance<LootboxData>();
        newItem.name = $"New Item";
        newItem.lootSprite = m_DefaultItemIcon;
        //Create the asset, using the unique ID for the name
        AssetDatabase.CreateAsset(newItem,  $"Assets/Data/NewItem{itemIndex}.asset");
        //Add it to the item list
        m_ItemDatabase.Add(newItem);
        //Refresh the ListView so everything is redrawn again
        m_ItemListView.Refresh();
        m_ItemListView.style.height = m_ItemDatabase.Count * m_ItemHeight;
        itemIndex = m_ItemDatabase.Count;
        EditorWindow.GetWindow<ItemDatabase>().ShowTab();
    }

    private void DeleteItem_OnClick()
    {
        //Get the path of the fie and delete it through AssetDatabase
        string path = AssetDatabase.GetAssetPath(m_activeItem);
        AssetDatabase.DeleteAsset(path);
        //Purge the reference from the list and refresh the ListView
        m_ItemDatabase.Remove(m_activeItem);
        m_ItemListView.Refresh();
        ListView_onSelectionChange(m_ItemListView.selectedItems);
    }

    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => m_ItemRowTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (e, i) =>
        {
            e.Q<VisualElement>("Icon").style.backgroundImage =
                m_ItemDatabase[i] == null ? m_DefaultItemIcon.texture :
                m_ItemDatabase[i].lootSprite.texture;
            e.Q<Label>("Name").text = m_ItemDatabase[i].name;
        };
        m_ItemListView = new ListView(m_ItemDatabase, 35, makeItem, bindItem);
        m_ItemListView.selectionType = SelectionType.Single;
        m_ItemListView.style.height = m_ItemDatabase.Count * m_ItemHeight;
        m_ItemsTab.Add(m_ItemListView);
        m_ItemListView.onSelectionChange += ListView_onSelectionChange;


    }


    private void ListView_onSelectionChange(IEnumerable<object> selectedItems)
    {
        m_activeItem = (LootboxData)selectedItems.First();
        SerializedObject so = new SerializedObject(m_activeItem);
        m_DetailSection.Bind(so);
        if (m_activeItem.lootSprite != null)
        {
            m_LargeDisplayIcon.style.backgroundImage = m_activeItem.lootSprite.texture; 
        }
        m_DetailSection.style.visibility = Visibility.Visible;
    }
}