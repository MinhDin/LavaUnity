using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Factory
//Place to get object. Add all pool object here.
public class Factory
{
	//APool itemPool;

	public Factory()
	{
		
	}

	public void Init(Game game)
	{
		//itemPool = new APool(game.ItemPrefab, 60);
	}
	
	//public Item GetItem()
	//{
	//	return (Item)itemPool.GetObject();
	//}

	//public void ReturnItem(Item item)
	//{
	//	itemPool.ReturnObject(item);
	//}
}
