using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("playerName", "playerPrefab", "number", "playerType", "speed", "projectile", "playerTemplateImage", "unlockCost", "isUnlocked", "isSelected")]
	public class ES3Type_PlayerTemplate : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_PlayerTemplate() : base(typeof(PlayerTemplate)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (PlayerTemplate)obj;
			
			writer.WriteProperty("playerName", instance.playerName, ES3Type_string.Instance);
			writer.WritePropertyByRef("playerPrefab", instance.playerPrefab);
			writer.WriteProperty("number", instance.number, ES3Type_int.Instance);
			writer.WriteProperty("playerType", instance.playerType, ES3Type_string.Instance);
			writer.WriteProperty("speed", instance.speed, ES3Type_string.Instance);
			writer.WriteProperty("projectile", instance.projectile, ES3Type_string.Instance);
			writer.WritePropertyByRef("playerTemplateImage", instance.playerTemplateImage);
			writer.WriteProperty("unlockCost", instance.unlockCost, ES3Type_float.Instance);
			writer.WriteProperty("isUnlocked", instance.isUnlocked, ES3Type_bool.Instance);
			writer.WriteProperty("isSelected", instance.isSelected, ES3Type_bool.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayerTemplate)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "playerName":
						instance.playerName = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "playerPrefab":
						instance.playerPrefab = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "number":
						instance.number = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "playerType":
						instance.playerType = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "speed":
						instance.speed = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "projectile":
						instance.projectile = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "playerTemplateImage":
						instance.playerTemplateImage = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
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
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new PlayerTemplate();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_PlayerTemplateArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_PlayerTemplateArray() : base(typeof(PlayerTemplate[]), ES3Type_PlayerTemplate.Instance)
		{
			Instance = this;
		}
	}
}