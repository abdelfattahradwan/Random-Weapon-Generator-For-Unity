using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEditor;

using UnityEngine;

namespace WinterboltGames.RandomWeaponGenerator
{
	public class RandomWeaponGeneratorWindow : EditorWindow
	{
		private Vector2 scrollview;

		private List<WeaponPropertiesTemplate> templates;

		private List<WeaponBase> weaponBases;

		private List<GameObject> stocks;
		private List<GameObject> grips;
		private List<GameObject> magazines;
		private List<GameObject> scopes;
		private List<GameObject> barrels;

		private bool showTemplates;
		private bool showWeaponBodyList;
		private bool showStockList;
		private bool showGripList;
		private bool showMagazineList;
		private bool showScopeList;
		private bool showBarrelList;
		private bool generateGrid;

		private int generatedGridWidth;
		private int generatedGridHeight;

		private float generatedGridHorizontalOffset;
		private float generatedGridVerticalOffset;

		private const string ASSET_REFERENCES_INSTANCE_IDS_FILE_NAME = "rwgwariids.bin";

		[MenuItem("Tools/Random Weapon Generator")]
		public static void Open()
		{
			RandomWeaponGeneratorWindow window = GetWindow<RandomWeaponGeneratorWindow>();

			window.titleContent = new GUIContent("Random Weapon Generator");

			window.templates = new List<WeaponPropertiesTemplate>();

			window.weaponBases = new List<WeaponBase>();

			window.stocks = new List<GameObject>();
			window.grips = new List<GameObject>();
			window.magazines = new List<GameObject>();
			window.scopes = new List<GameObject>();
			window.barrels = new List<GameObject>();
		}

		private void OnGUI()
		{
			scrollview = EditorGUILayout.BeginScrollView(scrollview);

			EditorGUILayout.HelpBox("Templates", MessageType.None);

			if (showTemplates = EditorGUILayout.Foldout(showTemplates, "Templates"))
			{
				DrawList(ref templates);
			}

			EditorGUILayout.HelpBox("Parts", MessageType.None);

			if (showWeaponBodyList = EditorGUILayout.Foldout(showWeaponBodyList, "Bodies"))
			{
				DrawList(ref weaponBases);
			}

			if (showStockList = EditorGUILayout.Foldout(showStockList, "Stocks"))
			{
				DrawList(ref stocks);
			}

			if (showGripList = EditorGUILayout.Foldout(showGripList, "Grips"))
			{
				DrawList(ref grips);
			}

			if (showMagazineList = EditorGUILayout.Foldout(showMagazineList, "Magazines"))
			{
				DrawList(ref magazines);
			}

			if (showScopeList = EditorGUILayout.Foldout(showScopeList, "Scopes"))
			{
				DrawList(ref scopes);
			}

			if (showBarrelList = EditorGUILayout.Foldout(showBarrelList, "Barrels"))
			{
				DrawList(ref barrels);
			}

			EditorGUILayout.HelpBox("Generation", MessageType.None);

			if (generateGrid = EditorGUILayout.Toggle("Generate Grid?", generateGrid))
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
							_ = RuntimeWeaponGenerator
								.GenerateRandomWeapon(templates, weaponBases, stocks, grips, magazines, scopes, barrels)
								.transform.position = new Vector3(x * generatedGridHorizontalOffset, y * generatedGridVerticalOffset, 0f);
						}
					}
				}
			}
			else
			{
				if (GUILayout.Button("Generate Random Weapon"))
				{
					_ = RuntimeWeaponGenerator.GenerateRandomWeapon(templates, weaponBases, stocks, grips, magazines, scopes, barrels);
				}
			}

			EditorGUILayout.HelpBox("Asset References", MessageType.None);

			if (GUILayout.Button("Save Asset References"))
			{
				SaveAssetReferences();
			}

			if (GUILayout.Button("Load Asset References"))
			{
				LoadAssetReferences();
			}

			EditorGUILayout.EndScrollView();
		}

		private void SaveAssetReferences()
		{
			List<int> templatesInstanceIDs = new List<int>();
			List<int> weaponBasesInstanceIDs = new List<int>();
			List<int> stocksInstanceIDs = new List<int>();
			List<int> handlesInstanceIDs = new List<int>();
			List<int> magazinesInstanceIDs = new List<int>();
			List<int> scopesInstanceIDs = new List<int>();
			List<int> barrelsInstanceIDs = new List<int>();

			templates?.ForEach(template =>
			{
				if (template != null)
				{
					templatesInstanceIDs.Add(template.GetInstanceID());
				}
			});

			weaponBases?.ForEach(weaponBase =>
			{
				if (weaponBase != null)
				{
					weaponBasesInstanceIDs.Add(weaponBase.GetInstanceID());
				}
			});

			stocks?.ForEach(stock =>
			{
				if (stock != null)
				{
					stocksInstanceIDs.Add(stock.GetInstanceID());
				}
			});

			grips?.ForEach(handle =>
			{
				if (handle != null)
				{
					handlesInstanceIDs.Add(handle.GetInstanceID());
				}
			});

			magazines?.ForEach(magazine =>
			{
				if (magazine != null)
				{
					magazinesInstanceIDs.Add(magazine.GetInstanceID());
				}
			});

			scopes?.ForEach(scope =>
			{
				if (scope != null)
				{
					scopesInstanceIDs.Add(scope.GetInstanceID());
				}
			});

			barrels?.ForEach(barrel =>
			{
				if (barrel != null)
				{
					barrelsInstanceIDs.Add(barrel.GetInstanceID());
				}
			});

			List<int>[] lists = new List<int>[7]
			{
				templatesInstanceIDs,
				weaponBasesInstanceIDs,
				stocksInstanceIDs,
				handlesInstanceIDs,
				magazinesInstanceIDs,
				scopesInstanceIDs,
				barrelsInstanceIDs,
			};

			using (FileStream fileStream = new FileStream(Path.Combine("Temp", ASSET_REFERENCES_INSTANCE_IDS_FILE_NAME), FileMode.Create))
			{
				new BinaryFormatter().Serialize(fileStream, lists);
			}
		}

		private void LoadAssetReferences()
		{
			templates?.Clear();
			weaponBases?.Clear();
			stocks?.Clear();
			grips?.Clear();
			magazines?.Clear();
			scopes?.Clear();
			barrels?.Clear();

			string filePath = Path.Combine("Temp", ASSET_REFERENCES_INSTANCE_IDS_FILE_NAME);

			if (File.Exists(filePath))
			{
				List<int>[] lists = new List<int>[0];

				using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
				{
					lists = new BinaryFormatter().Deserialize(fileStream) as List<int>[];
				}

				lists[0].ForEach(id => templates?.Add(AssetDatabase.LoadAssetAtPath<WeaponPropertiesTemplate>(AssetDatabase.GetAssetPath(id))));
				lists[1].ForEach(id => weaponBases?.Add(AssetDatabase.LoadAssetAtPath<WeaponBase>(AssetDatabase.GetAssetPath(id))));
				lists[2].ForEach(id => stocks?.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GetAssetPath(id))));
				lists[3].ForEach(id => grips?.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GetAssetPath(id))));
				lists[4].ForEach(id => magazines?.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GetAssetPath(id))));
				lists[5].ForEach(id => scopes?.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GetAssetPath(id))));
				lists[6].ForEach(id => barrels?.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GetAssetPath(id))));
			}
		}

		private void DrawList<T>(ref List<T> list) where T : Object
		{
			Event currentEvent = Event.current;

			Rect dropRect = GUILayoutUtility.GetRect(0.0f, 32.0f, GUILayout.ExpandWidth(true));

			GUI.Box(dropRect, "Drag and drop items here to add to the list", EditorStyles.helpBox);

			if (currentEvent.type == EventType.DragUpdated || currentEvent.type == EventType.DragPerform)
			{
				if (dropRect.Contains(currentEvent.mousePosition))
				{
					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

					if (currentEvent.type == EventType.DragPerform)
					{
						foreach (Object objectReference in DragAndDrop.objectReferences)
						{
							if (objectReference is T typeReference)
							{
								if (list == null)
								{
									list = new List<T>();
								}

								list.Add(typeReference);
							}
						}

						DragAndDrop.AcceptDrag();

						currentEvent.Use();
					}
				}
			}

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
}