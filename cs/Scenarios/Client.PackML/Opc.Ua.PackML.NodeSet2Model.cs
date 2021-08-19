namespace Client
{
    using System;

    using Opc.Ua;
    using Opc.UaFx;

    public class PackMLAlarmData
    {
        public int ID
        {
            get;
            set;
        }

        public int Value
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public int Category
        {
            get;
            set;
        }

        public DateTime DateTime
        {
            get;
            set;
        }

        public DateTime AckDateTime
        {
            get;
            set;
        }

        public bool Trigger
        {
            get;
            set;
        }
    }

    public class PackMLCountData
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public OpcEngineeringUnitInfo Unit
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }

        public int AccCount
        {
            get;
            set;
        }
    }

    public class PackMLDescriptorData
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public OpcEngineeringUnitInfo Unit
        {
            get;
            set;
        }

        public float Value
        {
            get;
            set;
        }
    }

    public class PackMLIngredientsData
    {
        public int IngredientID
        {
            get;
            set;
        }

        public PackMLDescriptorData[] Parameter
        {
            get;
            set;
        }
    }

    public class PackMLProductData
    {
        public int ProductID
        {
            get;
            set;
        }

        public PackMLDescriptorData[] ProcessVariables
        {
            get;
            set;
        }

        public PackMLIngredientsData[] Ingredients
        {
            get;
            set;
        }
    }

    public class PackMLRemoteInterfaceData
    {
        public int Number
        {
            get;
            set;
        }

        public int ControlCmdNumber
        {
            get;
            set;
        }

        public int CmdValue
        {
            get;
            set;
        }

        public PackMLDescriptorData[] Parameter
        {
            get;
            set;
        }
    }

    public enum ProductionMaintenanceMode
    {
        Invalid = 0,
        Produce = 1,
        Maintenance = 2,
        Manual = 3
    }
}
