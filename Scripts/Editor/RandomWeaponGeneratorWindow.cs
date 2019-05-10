using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

public class RandomWeaponGeneratorWindow : EditorWindow
{
	private Vector2 scrollview;

	private List<WeaponPropertiesTemplate> templates;

	private List<WeaponBody> weaponBodies;

	private List<GameObject> stocks;
	private List<GameObject> handles;
	private List<GameObject> magazines;
	private List<GameObject> scopes;
	private List<GameObject> barrels;

	private bool showTemplates;
	private bool showWeaponBodyList;
	private bool showStockList;
	private bool showHandleList;
	private bool showMagazineList;
	private bool showScopeList;
	private bool showBarrelList;
	private bool generateGrid;

	private int generatedGridWidth;
	private int generatedGridHeight;

	private float generatedGridHorizontalOffset;
	private float generatedGridVerticalOffset;

	[MenuItem("Tools/Random Weapon Generator")]
	public static void Open()
	{
		var window = GetWindow<RandomWeaponGeneratorWindow>();

		window.titleContent = new GUIContent("Random Weapon Generator");

		window.templates = new List<WeaponPropertiesTemplate>();

		window.weaponBodies = new List<WeaponBody>();

		window.stocks = new List<GameObject>();
		window.handles = new List<GameObject>();
		window.magazines = new List<GameObject>();
		window.scopes = new List<GameObject>();
		window.barrels = new List<GameObject>();
	}

	private void OnGUI()
	{
		scrollview = EditorGUILayout.BeginScrollView(scrollview);

		showTemplates = EditorGUILayout.Foldout(showTemplates, "Templates");

		if (showTemplates)
		{
			DrawList(ref templates);
		}

		EditorGUILayout.LabelField("-- Parts --");

		showWeaponBodyList = EditorGUILayout.Foldout(showWeaponBodyList, "Bodies");

		if (showWeaponBodyList)
		{
			DrawList(ref weaponBodies);
		}

		showStockList = EditorGUILayout.Foldout(showStockList, "Stocks");

		if (showStockList)
		{
			DrawList(ref stocks);
		}

		showHandleList = EditorGUILayout.Foldout(showHandleList, "Handles");

		if (showHandleList)
		{
			DrawList(ref handles);
		}

		showMagazineList = EditorGUILayout.Foldout(showMagazineList, "Magazines");

		if (showMagazineList)
		{
			DrawList(ref magazines);
		}

		showScopeList = EditorGUILayout.Foldout(showScopeList, "Scopes");

		if (showScopeList)
		{
			DrawList(ref scopes);
		}

		showBarrelList = EditorGUILayout.Foldout(showBarrelList, "Barrels");

		if (showBarrelList)
		{
			DrawList(ref barrels);
		}

		EditorGUILayout.LabelField("-- Generation --");

		generateGrid = EditorGUILayout.Toggle("Generate Grid?", generateGrid);

		if (generateGrid)
		{
			generatedGridWidth = EditorGUILayout.IntField("Grid Width", generatedGridWidth);
			generatedGridHeight = EditorGUILayout.IntField("Grid Height", generatedGridHeight);

			generatedGridHorizontalOffset = EditorGUILayout.FloatField("Horizontal Grid Cell Offset", generatedGridHorizontalOffset);
			generatedGridVerticalOffset = EditorGUILayout.FloatField("Vertical Grid Cell Offset", generatedGridVerticalOffset);

			if (GUILayout.Button("Generate Random Weapon Grid"))
			{
				for (int x = 0; x < generatedGridWidth; x++)
				{
					for (int y = 0; y < generatedGridHeight; y++)
					{
						RuntimeWeaponGenerator.GenerateRandomWeapon(templates.ElementAtOrDefault(Random.Range(0, templates.Count)), weaponBodies, stocks, handles, magazines, scopes, barrels)
							.position = new Vector3(x * generatedGridHorizontalOffset, y * generatedGridVerticalOffset, 0f);
					}
				}
			}
		}
		else
		{
			if (GUILayout.Button("Generate Random Weapon"))
			{
				_ = RuntimeWeaponGenerator.GenerateRandomWeapon(templates.ElementAtOrDefault(Random.Range(0, templates.Count)), weaponBodies, stocks, handles, magazines, scopes, barrels);
			}
		}

		EditorGUILayout.EndScrollView();
	}

	private void DrawList<T>(ref List<T> list) where T : Object
	{
		if (GUILayout.Button("Add"))
		{
			if (list == null)
			{
				list = new List<T>();
			}

			list.Add(null);
		}

		if (list != null && list.Count != 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();

				list[i] = EditorGUILayout.ObjectField(list[i], typeof(T), false) as T;

				if (GUILayout.Button("-", GUILayout.MaxWidth(32f)))
				{
					list.RemoveAt(i);
				}

				EditorGUILayout.EndHorizontal();
			}
		}
		else
		{
			EditorGUILayout.LabelField("Empty...", EditorStyles.centeredGreyMiniLabel);
		}
	}
}