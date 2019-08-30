using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("baseNumber", "baseName", "baseLevel", "baseImage", "basePrefab", "unlockCost", "isUnlocked", "isSelected", "upgradable", "upgradeName", "upgradeCost", "upgradeImage", "isUpgraded")]
	public class ES3Type_BaseTemplate : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_BaseTemplate() : base(typeof(BaseTemplate)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (BaseTemplate)obj;
			
			writer.WriteProperty("baseNumber", instance.baseNumber, ES3Type_int.Instance);
			writer.WriteProperty("baseName", instance.baseName, ES3Type_string.Instance);
			writer.WriteProperty("baseLevel", instance.baseLevel, ES3Type_int.Instance);
			writer.WritePropertyByRef("baseImage", instance.baseImage);
			writer.WritePropertyByRef("basePrefab", instance.basePrefab);
			writer.WriteProperty("unlockCost", instance.unlockCost, ES3Type_float.Instance);
			writer.WriteProperty("isUnlocked", instance.isUnlocked, ES3Type_bool.Instance);
			writer.WriteProperty("isSelected", instance.isSelected, ES3Type_bool.Instance);
			writer.WriteProperty("upgradable", instance.upgradable, ES3Type_bool.Instance);
			writer.WriteProperty("upgradeName", instance.upgradeName, ES3Type_string.Instance);
			writer.WriteProperty("upgradeCost", instance.upgradeCost, ES3Type_float.Instance);
			writer.WritePropertyByRef("upgradeImage", instance.upgradeImage);
			writer.WriteProperty("isUpgraded", instance.isUpgraded, ES3Type_bool.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (BaseTemplate)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "baseNumber":
						instance.baseNumber = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "baseName":
						instance.baseName = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "baseLevel":
						instance.baseLevel = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "baseImage":
						instance.baseImage = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
						break;
					case "basePrefab":
						instance.basePrefab = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "unlockCost":
						instance.unlockCost = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "isUnlocked":
						instance.isUnlocked = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "isSelected":
						instance.isSelected = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "upgradable":
						instance.upgradable = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "upgradeName":
						instance.upgradeName = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "upgradeCost":
						instance.upgradeCost = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "upgradeImage":
						instance.upgradeImage = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
						break;
					case "isUpgraded":
						instance.isUpgraded = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new BaseTemplate();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_BaseTemplateArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_BaseTemplateArray() : base(typeof(BaseTemplate[]), ES3Type_BaseTemplate.Instance)
		{
			Instance = this;
		}
	}
}