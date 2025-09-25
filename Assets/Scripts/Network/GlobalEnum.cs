using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

namespace YunSun
{
	public enum CustomerType
	{
		Normal,
		Rich,
		Gourmet,
		Glutton,
	}

	public enum IngredientType
	{

	}

	public enum MenuType
	{

	}

	public enum EmployeeType
	{

	}

	public enum InteriorType
	{

	}

	public enum ShopStoreType
	{
		kShopStoreType_None = 0,
		kShopStoreType_Google,
		kShopStoreType_OneStore,
		kShopStoreType_Apple,
		kShopStoreType_Galaxy,
	};

	public enum OSType 
	{
		kOSType_None = 0,
		kOSType_AOS,
		kOSType_IOS,
		kOSType_WEB,
		kOSType_WINDOWS,
	};

}