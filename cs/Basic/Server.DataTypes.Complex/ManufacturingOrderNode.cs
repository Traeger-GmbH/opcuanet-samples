// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace DataTypes.Complex
{
    using Opc.UaFx;

    public class ManufacturingOrderNode : OpcDataVariableNode<ManufacturingOrder>
    {
        #region ---------- Public constructors ----------

        public ManufacturingOrderNode(SampleNodeManager nodeManager, IOpcNode parent, OpcName name, ManufacturingOrder value)
            : base(parent, name, value)
        {
            this.Article = this.CreateFieldNode(
                    nameof(ManufacturingOrder.Article),
                    () => this.Value.Article,
                    (value) => this.Value.Article = value);

            this.Order = this.CreateFieldNode(
                    nameof(ManufacturingOrder.Order),
                    () => this.Value.Order,
                    (value) => this.Value.Order = value);

            this.Jobs = this.CreateFieldNode(
                    nodeManager,
                    nameof(ManufacturingOrder.Jobs),
                    () => this.Value.Jobs,
                    (value) => this.Value.Jobs = value,
                    (parent, name, value) => new MachineJobNode(parent, name, value));
        }

        #endregion

        #region ---------- Public properties ----------

        public OpcDataVariableNode<string> Article
        {
            get;
        }

        public OpcDataVariableNode<string> Order
        {
            get;
        }

        public OpcDataVariableNode<MachineJob[]> Jobs
        {
            get;
        }

        #endregion

        #region ---------- Public methods ----------

        protected override OpcVariableValue<object> WriteVariableValueCore(OpcWriteVariableValueContext context, OpcVariableValue<object> value)
        {
            if (value.Value is ManufacturingOrder typedValue) {
                this.Article.Value = typedValue.Article;
                this.Order.Value = typedValue.Order;
                this.Jobs.Value = typedValue.Jobs;
            }

            return base.WriteVariableValueCore(context, value);
        }

        #endregion
    }
}
