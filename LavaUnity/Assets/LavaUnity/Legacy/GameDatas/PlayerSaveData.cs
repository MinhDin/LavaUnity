using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerSaveData
{
    public List<StringIntSave> Items;
    public List<StringIntSave> Collections;
    public List<StringIntSave> TrashCollections;
    public List<StringDoubleLongIntSave> Islands;//cleanergy, last trash time, level trash

    //Misc
    public long LastSaveTimeInTick;

    //resources
    public double CoinAmount;
    public float PerlAmount;

	//Grow	
	public int DamageLevel;
	public int MonsterLevel;
    //public int TrashCanLevel;

    //public long TrashCanLastSavedTime;
    //public long TrashCanLastClaimTime;

    public PlayerSaveData()
    {
        Items = new List<StringIntSave>();
        Collections = new List<StringIntSave>(2);
        TrashCollections = new List<StringIntSave>(2);

        Islands = new List<StringDoubleLongIntSave>(){new StringDoubleLongIntSave(Global.FirstIsland, 0, DateTime.Now.Ticks, 1)};
        CoinAmount = 0;
		DamageLevel = 1;
		MonsterLevel = 1;
        PerlAmount = 0;
        //TrashCanLevel = 1;
        LastSaveTimeInTick = System.DateTime.Now.Ticks;
    }
}

[System.Serializable]
public class StringValueSave<T>
{
    public string Name;
    public ClassedValue<T> CValue;
    public T Value
    {
        get
        {
            return CValue.Value;
        }
        set
        {
            CValue.Value = value;
        }
    }

    public StringValueSave(string item, T value)
    {
        Name = item;
        CValue = new ClassedValue<T>(value);
    }

    public StringValueSave(string item, ClassedValue<T> cValue)
    {
        Name = item;
        CValue = cValue;
    }
}

[System.Serializable]
public class StringIntSave
{
    public string Name;
    public ClassedInt CValue;
    public int Value
    {
        get
        {
            return CValue.Value;
        }
        set
        {
            CValue.Value = value;
        }
    }

    public StringIntSave(string item, int value)
    {
        Name = item;
        CValue = new ClassedInt(value);
    }

    public StringIntSave(string item, ClassedInt cValue)
    {
        Name = item;
        CValue = cValue;
    }
}

[System.Serializable]
public class StringDoubleSave
{
    public string Name;
    public ClassedDouble CValue;
    public double Value
    {
        get
        {
            return CValue.Value;
        }
        set
        {
            CValue.Value = value;
        }
    }

    public StringDoubleSave(string item, double value)
    {
        Name = item;
        CValue = new ClassedDouble(value);
    }

    public StringDoubleSave(string item, ClassedDouble cValue)
    {
        Name = item;
        CValue = cValue;
    }
}

[System.Serializable]
public class StringDoubleLongIntSave
{
    public string Name;
    public ClassedDouble dValue;
    public ClassedLong lValue;
    public ClassedInt iValue;

    public double DValue
    {
        get
        {
            return dValue.Value;
        }
        set
        {
            dValue.Value = value;
        }
    }

    public long LValue
    {
        get
        {
            return lValue.Value;
        }
        set
        {
            lValue.Value = value;
        }
    }
    
    public int IValue
    {
        get
        {
            return iValue.Value;
        }
        set
        {
            iValue.Value = value;
        }
    }
    public StringDoubleLongIntSave(string item, double dValue, long lValue, int iValue)
    {
        Name = item;
        this.dValue = new ClassedDouble(dValue);
        this.lValue = new ClassedLong(lValue);
        this.iValue = new ClassedInt(iValue);
    }

    public StringDoubleLongIntSave(string item, ClassedDouble dValue, ClassedLong lValue, ClassedInt iValue)
    {
        Name = item;
        this.dValue = dValue;
        this.lValue = lValue;
        this.iValue = iValue;
    }
}

[System.Serializable]
public class ClassedValue<T>
{
    public T Value;

    public ClassedValue(T value)
    {
        Value = value;
    }
}

[System.Serializable]
public class ClassedInt : ClassedValue<int>
{
    public ClassedInt(int value)
        : base(value)
    {

    }
}

[System.Serializable]
public class ClassedDouble : ClassedValue<double>
{
    public ClassedDouble(double value)
        : base(value)
    {

    }
}

[System.Serializable]
public class ClassedLong : ClassedValue<long>
{
    public ClassedLong(long value)
        : base(value)
    {

    }
}

