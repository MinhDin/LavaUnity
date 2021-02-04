using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PlayerSavePack
{
    public PlayerSaveData SaveData;
    public static int VERSION = 1;

    public static int SAVED_VERSION;
    public PlayerSavePack()
    {
        //version = 1;
        SAVED_VERSION = VERSION;
        SaveData = new PlayerSaveData();
    }

    public PlayerSavePack(BinaryReader binReader)
    {
        LoadFromBinary(binReader);
    }

    public void SaveToBinary(BinaryWriter binWriter)
    {
        binWriter.Write(VERSION);
        SaveData.SaveToBinary(binWriter);
    }

    public void LoadFromBinary(BinaryReader binReader)
    {
        SAVED_VERSION = binReader.ReadInt32();
        SaveData = new PlayerSaveData(binReader);
    }
}

public class PlayerSaveData
{
    public long LastSaveTimeInTick;
    public PlayerSaveData()
    {
       LastSaveTimeInTick = 0;
    }

    public PlayerSaveData(BinaryReader binReader)
    {
        LoadFromBinary(binReader);
    }

    public void SaveToBinary(BinaryWriter binWriter)
    {
        binWriter.Write(LastSaveTimeInTick);
    }

    public void LoadFromBinary(BinaryReader binReader)
    {
       LastSaveTimeInTick = binReader.ReadInt64();
    }
}

public class UISaveData
{
    public int Diamond;

    public UISaveData()
    {

    }

    public UISaveData(BinaryReader binReader)
    {
        LoadFromBinary(binReader);
    }

    public void SaveToBinary(BinaryWriter binWriter)
    {
        binWriter.Write(Diamond);
    }

    public void LoadFromBinary(BinaryReader binReader)
    {
        Diamond = binReader.ReadInt32();
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

